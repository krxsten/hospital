using Hospital.Core.Contracts;
using Hospital.Core.DTOs;
using Hospital.Core.Services;
using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace Hospital.Tests.Services
{
    [TestFixture]
    public class NurseServiceTests
    {
        private HospitalDbContext context;
        private Mock<IImageService> imageServiceMock;
        private NurseService service;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<HospitalDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            context = new HospitalDbContext(options);
            imageServiceMock = new Mock<IImageService>();
            service = new NurseService(context, imageServiceMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        private async Task<(User user, Specialization specialization, Shift shift, Nurse nurse)> SeedNurseAsync(
            string firstName = "Maria",
            string lastName = "Petrova",
            string specializationName = "Cardiology",
            string shiftType = "Morning")
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                UserName = $"{firstName}_{lastName}",
                Email = $"{firstName.ToLower()}{Guid.NewGuid():N}@test.com"
            };

            var specialization = new Specialization
            {
                ID = Guid.NewGuid(),
                SpecializationName = specializationName,
                ImageURL = "specImage",
                PublicID = "specPublicId"
            };

            var shift = new Shift
            {
                ID = Guid.NewGuid(),
                Type = shiftType,
                StartTime = new TimeOnly(8, 0),
                EndTime = new TimeOnly(16, 0)
            };

            var nurse = new Nurse
            {
                ID = Guid.NewGuid(),
                UserId = user.Id,
                User = user,
                SpecializationId = specialization.ID,
                Specialization = specialization,
                ShiftId = shift.ID,
                Shift = shift,
                IsAccepted = true,
                ImageURL = "/images/nurse.jpg",
                PublicID = "nurse_public_id"
            };

            context.Users.Add(user);
            context.Specializations.Add(specialization);
            context.Shifts.Add(shift);
            context.Nurses.Add(nurse);

            await context.SaveChangesAsync();

            return (user, specialization, shift, nurse);
        }
        [Test]
        public async Task CreateAsync_WhenNurseNameIsEmpty_SetsBothNamesEmpty()
        {
            var specialization = new Specialization
            {
                ID = Guid.NewGuid(),
                SpecializationName = "Neurology",
                ImageURL = "image",
                PublicID = "public"
            };

            var shift = new Shift
            {
                ID = Guid.NewGuid(),
                Type = "Morning",
                StartTime = new TimeOnly(8, 0),
                EndTime = new TimeOnly(16, 0)
            };

            context.Specializations.Add(specialization);
            context.Shifts.Add(shift);
            await context.SaveChangesAsync();

            var fileMock = new Mock<IFormFile>();

            imageServiceMock
                .Setup(x => x.UploadImageAsync(fileMock.Object))
                .ReturnsAsync(("/images/uploaded.jpg", "uploaded_public_id"));

            var dto = new NurseCreateDTO
            {
                NurseName = "",
                SpecializationID = specialization.ID,
                ShiftID = shift.ID,
                ImageFile = fileMock.Object
            };

            await service.CreateAsync(dto);

            var nurse = await context.Nurses
                .Include(x => x.User)
                .FirstOrDefaultAsync();

            Assert.That(nurse, Is.Not.Null);
            Assert.That(nurse!.User.FirstName, Is.EqualTo(""));
            Assert.That(nurse.User.LastName, Is.EqualTo(""));
            Assert.That(nurse.User.UserName, Is.EqualTo("_"));
        }
        [Test]
        public async Task UpdateAsync_WhenNurseNameIsEmpty_SetsBothNamesEmpty()
        {
            var seed = await SeedNurseAsync();

            var model = new NurseEditDTO
            {
                ID = seed.nurse.ID,
                NurseName = "",
                SpecializationId = seed.specialization.ID,
                ShiftId = seed.shift.ID,
                IsAccepted = true
            };

            await service.UpdateAsync(model);

            var updated = await context.Nurses
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.ID == seed.nurse.ID);

            Assert.That(updated!.User.FirstName, Is.EqualTo(""));
            Assert.That(updated.User.LastName, Is.EqualTo(""));
        }
        [Test]
        public async Task GetAllAsync_ReturnsAllNurses()
        {
            await SeedNurseAsync("Maria", "Petrova", "Cardiology", "Morning");
            await SeedNurseAsync("Elena", "Ivanova", "Neurology", "Night");

            var result = await service.GetAllAsync();

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Any(x => x.UserName == "Maria Petrova"), Is.True);
            Assert.That(result.Any(x => x.UserName == "Elena Ivanova"), Is.True);
            Assert.That(result.Any(x => x.SpecializationName == "Cardiology"), Is.True);
            Assert.That(result.Any(x => x.ShiftName == "Night"), Is.True);
        }

        [Test]
        public async Task GetByIdAsync_WhenNurseExists_ReturnsNurse()
        {
            var seed = await SeedNurseAsync();

            var result = await service.GetByIdAsync(seed.nurse.ID);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.ID, Is.EqualTo(seed.nurse.ID));
            Assert.That(result.UserName, Is.EqualTo("Maria Petrova"));
            Assert.That(result.SpecializationName, Is.EqualTo("Cardiology"));
            Assert.That(result.ShiftName, Is.EqualTo("Morning"));
        }

        [Test]
        public async Task GetByIdAsync_WhenNurseMissing_ReturnsNull()
        {
            var result = await service.GetByIdAsync(Guid.NewGuid());

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task CreateAsync_WhenValidData_AddsNurseAndUser()
        {
            var specialization = new Specialization
            {
                ID = Guid.NewGuid(),
                SpecializationName = "Surgery",
                ImageURL = "image",
                PublicID = "public"
            };

            var shift = new Shift
            {
                ID = Guid.NewGuid(),
                Type = "Night",
                StartTime = new TimeOnly(20, 0),
                EndTime = new TimeOnly(6, 0)
            };

            context.Specializations.Add(specialization);
            context.Shifts.Add(shift);
            await context.SaveChangesAsync();

            var fileMock = new Mock<IFormFile>();

            imageServiceMock
                .Setup(x => x.UploadImageAsync(fileMock.Object))
                .ReturnsAsync(("/images/uploaded.jpg", "uploaded_public_id"));

            var model = new NurseCreateDTO
            {
                NurseName = "Anna Dimitrova",
                SpecializationID = specialization.ID,
                ShiftID = shift.ID,
                ImageFile = fileMock.Object
            };

            await service.CreateAsync(model);

            var nurse = await context.Nurses.Include(x => x.User).FirstOrDefaultAsync();

            Assert.That(nurse, Is.Not.Null);
            Assert.That(nurse!.User.FirstName, Is.EqualTo("Anna"));
            Assert.That(nurse.User.LastName, Is.EqualTo("Dimitrova"));
            Assert.That(nurse.User.UserName, Is.EqualTo("Anna_Dimitrova"));
            Assert.That(nurse.SpecializationId, Is.EqualTo(specialization.ID));
            Assert.That(nurse.ShiftId, Is.EqualTo(shift.ID));
            Assert.That(nurse.IsAccepted, Is.True);
            Assert.That(nurse.ImageURL, Is.EqualTo("/images/uploaded.jpg"));
            Assert.That(nurse.PublicID, Is.EqualTo("uploaded_public_id"));

            imageServiceMock.Verify(x => x.UploadImageAsync(fileMock.Object), Times.Once);
        }

        [Test]
        public void CreateAsync_WhenImageIsNull_ThrowsException()
        {
            var model = new NurseCreateDTO
            {
                NurseName = "Anna Dimitrova",
                SpecializationID = Guid.NewGuid(),
                ShiftID = Guid.NewGuid(),
                ImageFile = null
            };

            var ex = Assert.ThrowsAsync<Exception>(async () => await service.CreateAsync(model));

            Assert.That(ex!.Message, Is.EqualTo("Image is required"));
        }

        [Test]
        public async Task UpdateAsync_WhenNurseExistsAndNoNewImage_UpdatesDataWithoutChangingImage()
        {
            var seed = await SeedNurseAsync();

            var newSpecialization = new Specialization
            {
                ID = Guid.NewGuid(),
                SpecializationName = "Neurology",
                ImageURL = "newSpecImage",
                PublicID = "newSpecPublicId"
            };

            var newShift = new Shift
            {
                ID = Guid.NewGuid(),
                Type = "Night",
                StartTime = new TimeOnly(18, 0),
                EndTime = new TimeOnly(2, 0)
            };

            context.Specializations.Add(newSpecialization);
            context.Shifts.Add(newShift);
            await context.SaveChangesAsync();

            var model = new NurseEditDTO
            {
                ID = seed.nurse.ID,
                NurseName = "Elena Georgieva",
                SpecializationId = newSpecialization.ID,
                ShiftId = newShift.ID,
                IsAccepted = false,
                NewImageFile = null
            };

            await service.UpdateAsync(model);

            var updated = await context.Nurses.Include(x => x.User).FirstOrDefaultAsync(x => x.ID == seed.nurse.ID);

            Assert.That(updated, Is.Not.Null);
            Assert.That(updated!.User.FirstName, Is.EqualTo("Elena"));
            Assert.That(updated.User.LastName, Is.EqualTo("Georgieva"));
            Assert.That(updated.SpecializationId, Is.EqualTo(newSpecialization.ID));
            Assert.That(updated.ShiftId, Is.EqualTo(newShift.ID));
            Assert.That(updated.IsAccepted, Is.True);
            Assert.That(updated.ImageURL, Is.EqualTo("/images/nurse.jpg"));
            Assert.That(updated.PublicID, Is.EqualTo("nurse_public_id"));
        }

        [Test]
        public async Task UpdateAsync_WhenNurseExistsAndNewImage_UpdatesDataAndImage()
        {
            var seed = await SeedNurseAsync();

            var newSpecialization = new Specialization
            {
                ID = Guid.NewGuid(),
                SpecializationName = "Pediatrics",
                ImageURL = "specImg",
                PublicID = "specPid"
            };

            var newShift = new Shift
            {
                ID = Guid.NewGuid(),
                Type = "Afternoon",
                StartTime = new TimeOnly(12, 0),
                EndTime = new TimeOnly(20, 0)
            };

            context.Specializations.Add(newSpecialization);
            context.Shifts.Add(newShift);
            await context.SaveChangesAsync();

            var fileMock = new Mock<IFormFile>();

            imageServiceMock
                .Setup(x => x.UploadImageAsync(fileMock.Object))
                .ReturnsAsync(("/images/new-nurse.jpg", "new_public_id"));

            var model = new NurseEditDTO
            {
                ID = seed.nurse.ID,
                NurseName = "Silvia Koleva",
                SpecializationId = newSpecialization.ID,
                ShiftId = newShift.ID,
                IsAccepted = false,
                NewImageFile = fileMock.Object
            };

            await service.UpdateAsync(model);

            var updated = await context.Nurses.Include(x => x.User).FirstOrDefaultAsync(x => x.ID == seed.nurse.ID);

            Assert.That(updated, Is.Not.Null);
            Assert.That(updated!.User.FirstName, Is.EqualTo("Silvia"));
            Assert.That(updated.User.LastName, Is.EqualTo("Koleva"));
            Assert.That(updated.SpecializationId, Is.EqualTo(newSpecialization.ID));
            Assert.That(updated.ShiftId, Is.EqualTo(newShift.ID));
            Assert.That(updated.IsAccepted, Is.True);
            Assert.That(updated.ImageURL, Is.EqualTo("/images/new-nurse.jpg"));
            Assert.That(updated.PublicID, Is.EqualTo("new_public_id"));

            imageServiceMock.Verify(x => x.UploadImageAsync(fileMock.Object), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_WhenNurseMissing_DoesNothing()
        {
            var model = new NurseEditDTO
            {
                ID = Guid.NewGuid(),
                NurseName = "Missing Nurse",
                SpecializationId = Guid.NewGuid(),
                ShiftId = Guid.NewGuid(),
                IsAccepted = false
            };

            await service.UpdateAsync(model);

            Assert.That(await context.Nurses.CountAsync(), Is.EqualTo(0));
        }

        [Test]
        public async Task DeleteAsync_WhenNurseExists_RemovesNurse()
        {
            var seed = await SeedNurseAsync();

            await service.DeleteAsync(seed.nurse.ID);

            var nurse = await context.Nurses.FindAsync(seed.nurse.ID);
            Assert.That(nurse, Is.Null);
        }

        [Test]
        public async Task DeleteAsync_WhenNurseMissing_DoesNothing()
        {
            await service.DeleteAsync(Guid.NewGuid());

            Assert.That(await context.Nurses.CountAsync(), Is.EqualTo(0));
        }

        [Test]
        public async Task FilterBySpecialization_WhenSearchIsNull_ReturnsEmptyList()
        {
            var result = await service.FilterBySpecialization(null!);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task FilterBySpecialization_WhenSearchIsWhitespace_ReturnsEmptyList()
        {
            var result = await service.FilterBySpecialization("   ");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task FilterBySpecialization_WhenMatchesExist_ReturnsMatchingNurses()
        {
            await SeedNurseAsync("Maria", "Petrova", "Cardiology", "Morning");
            await SeedNurseAsync("Elena", "Ivanova", "Neurology", "Night");
            await SeedNurseAsync("Anna", "Koleva", "Cardio Surgery", "Afternoon");

            var result = await service.FilterBySpecialization("Cardio");

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Any(x => x.SpecializationName == "Cardiology"), Is.True);
            Assert.That(result.Any(x => x.SpecializationName == "Cardio Surgery"), Is.True);
            Assert.That(result.Any(x => x.SpecializationName == "Neurology"), Is.False);
        }

        [Test]
        public async Task FilterBySpecialization_WhenNoMatchesExist_ReturnsEmptyList()
        {
            await SeedNurseAsync("Maria", "Petrova", "Cardiology", "Morning");

            var result = await service.FilterBySpecialization("Oncology");

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task SortByFirstName_ReturnsNursesOrderedByFirstName()
        {
            await SeedNurseAsync("Zornitsa", "Petrova", "Cardiology", "Morning");
            await SeedNurseAsync("Anna", "Ivanova", "Neurology", "Night");
            await SeedNurseAsync("Borislava", "Koleva", "Surgery", "Afternoon");

            var result = await service.SortByFirstName();

            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result[0].UserName, Is.EqualTo("Anna Ivanova"));
            Assert.That(result[1].UserName, Is.EqualTo("Borislava Koleva"));
            Assert.That(result[2].UserName, Is.EqualTo("Zornitsa Petrova"));
        }
    }
}