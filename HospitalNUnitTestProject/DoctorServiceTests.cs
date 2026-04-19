using Hospital.Core.Contracts;
using Hospital.Core.DTOs;
using Hospital.Core.Services;
using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.Entities;
using Hospital.Tests.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace Hospital.Tests.Services
{
    [TestFixture]
    public class DoctorServiceTests
    {
        private HospitalDbContext context;
        private Mock<IImageService> imageServiceMock;
        private Mock<UserManager<User>> userManagerMock;
        private IDoctorService service;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<HospitalDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            context = new HospitalDbContext(options);
            imageServiceMock = new Mock<IImageService>();
            userManagerMock = TestHelpers.CreateUserManagerMock<User>();
            service = new DoctorService(context, imageServiceMock.Object, userManagerMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        private async Task<(User user, Specialization specialization, Shift shift, Doctor doctor)> SeedDoctorAsync(
            string firstName = "Ivan",
            string lastName = "Petrov",
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

            var doctor = new Doctor
            {
                ID = Guid.NewGuid(),
                UserId = user.Id,
                User = user,
                SpecializationId = specialization.ID,
                Specialization = specialization,
                ShiftId = shift.ID,
                Shift = shift,
                IsAccepted = true,
                ImageURL = "/images/doctor.jpg",
                CloudinaryID = "doctor_public_id"
            };

            context.Users.Add(user);
            context.Specializations.Add(specialization);
            context.Shifts.Add(shift);
            context.Doctors.Add(doctor);

            await context.SaveChangesAsync();

            return (user, specialization, shift, doctor);
        }

        [Test]
        public async Task CreateAsync_WhenValidData_AddsDoctorAndUser()
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

            var dto = new DoctorCreateDto
            {
                DoctorName = "Georgi Dimitrov",
                SpecializationID = specialization.ID,
                ShiftID = shift.ID,
                ImageFile = fileMock.Object
            };

            await service.CreateAsync(dto);

            var doctor = await context.Doctors.Include(x => x.User).FirstOrDefaultAsync();

            Assert.That(doctor, Is.Not.Null);
            Assert.That(doctor!.User.FirstName, Is.EqualTo("Georgi"));
            Assert.That(doctor.User.LastName, Is.EqualTo("Dimitrov"));
            Assert.That(doctor.User.UserName, Is.EqualTo("Georgi_Dimitrov"));
            Assert.That(doctor.SpecializationId, Is.EqualTo(specialization.ID));
            Assert.That(doctor.ShiftId, Is.EqualTo(shift.ID));
            Assert.That(doctor.IsAccepted, Is.True);
            Assert.That(doctor.ImageURL, Is.EqualTo("/images/uploaded.jpg"));
            Assert.That(doctor.CloudinaryID, Is.EqualTo("uploaded_public_id"));

            imageServiceMock.Verify(x => x.UploadImageAsync(fileMock.Object), Times.Once);
        }

        [Test]
        public void CreateAsync_WhenImageIsNull_ThrowsException()
        {
            var dto = new DoctorCreateDto
            {
                DoctorName = "Georgi Dimitrov",
                SpecializationID = Guid.NewGuid(),
                ShiftID = Guid.NewGuid(),
                ImageFile = null
            };

            var ex = Assert.ThrowsAsync<Exception>(async () => await service.CreateAsync(dto));

            Assert.That(ex!.Message, Is.EqualTo("Image is required"));
        }

        [Test]
        public async Task DeleteAsync_WhenDoctorExists_RemovesDoctor()
        {
            var seed = await SeedDoctorAsync();

            await service.DeleteAsync(seed.doctor.ID);

            var doctor = await context.Doctors.FindAsync(seed.doctor.ID);
            Assert.That(doctor, Is.Null);
        }
        [Test]
        public async Task UpdateAsync_WhenDoctorNameHasOnlyFirstName_SetsLastNameEmpty()
        {
            var seed = await SeedDoctorAsync();

            var model = new DoctorEditDTO
            {
                ID = seed.doctor.ID,
                DoctorName = "Ivan",
                SpecializationId = seed.specialization.ID,
                ShiftId = seed.shift.ID,
                IsAccepted = true
            };

            await service.UpdateAsync(model);

            var updated = await context.Doctors
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.ID == seed.doctor.ID);

            Assert.That(updated!.User.FirstName, Is.EqualTo("Ivan"));
            Assert.That(updated.User.LastName, Is.EqualTo(""));
        }
        [Test]
        public async Task UpdateAsync_WhenDoctorNameIsEmpty_SetsBothNamesEmpty()
        {
            var seed = await SeedDoctorAsync();

            var model = new DoctorEditDTO
            {
                ID = seed.doctor.ID,
                DoctorName = "",
                SpecializationId = seed.specialization.ID,
                ShiftId = seed.shift.ID,
                IsAccepted = true
            };

            await service.UpdateAsync(model);

            var updated = await context.Doctors
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.ID == seed.doctor.ID);

            Assert.That(updated!.User.FirstName, Is.EqualTo(""));
            Assert.That(updated.User.LastName, Is.EqualTo(""));
        }
        [Test]
        public async Task CreateAsync_WhenDoctorNameIsEmpty_SetsBothNamesEmpty()
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

            var dto = new DoctorCreateDto
            {
                DoctorName = "",
                SpecializationID = specialization.ID,
                ShiftID = shift.ID,
                ImageFile = fileMock.Object
            };

            await service.CreateAsync(dto);

            var doctor = await context.Doctors
                .Include(x => x.User)
                .FirstOrDefaultAsync();

            Assert.That(doctor, Is.Not.Null);
            Assert.That(doctor!.User.FirstName, Is.EqualTo(""));
            Assert.That(doctor.User.LastName, Is.EqualTo(""));
            Assert.That(doctor.User.UserName, Is.EqualTo("_"));
        }

        [Test]
        public async Task DeleteAsync_WhenDoctorMissing_DoesNothing()
        {
            await service.DeleteAsync(Guid.NewGuid());

            Assert.That(await context.Doctors.CountAsync(), Is.EqualTo(0));
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllDoctors()
        {
            await SeedDoctorAsync("Ivan", "Petrov", "Cardiology", "Morning");
            await SeedDoctorAsync("Maria", "Ivanova", "Neurology", "Night");

            var result = await service.GetAllAsync();

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Any(x => x.UserName == "Ivan Petrov"), Is.True);
            Assert.That(result.Any(x => x.UserName == "Maria Ivanova"), Is.True);
            Assert.That(result.Any(x => x.SpecializationName == "Cardiology"), Is.True);
            Assert.That(result.Any(x => x.ShiftName == "Night"), Is.True);
        }

        [Test]
        public async Task GetByIdAsync_WhenDoctorExists_ReturnsDoctor()
        {
            var seed = await SeedDoctorAsync();

            var result = await service.GetByIdAsync(seed.doctor.ID);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.ID, Is.EqualTo(seed.doctor.ID));
            Assert.That(result.UserName, Is.EqualTo("Ivan Petrov"));
            Assert.That(result.SpecializationName, Is.EqualTo("Cardiology"));
            Assert.That(result.ShiftName, Is.EqualTo("Morning"));
        }

        [Test]
        public async Task GetByIdAsync_WhenDoctorMissing_ReturnsNull()
        {
            var result = await service.GetByIdAsync(Guid.NewGuid());

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task UpdateAsync_WhenDoctorExistsAndNoNewImage_UpdatesDataWithoutChangingImage()
        {
            var seed = await SeedDoctorAsync();

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

            var dto = new DoctorEditDTO
            {
                ID = seed.doctor.ID,
                DoctorName = "Georgi Dimitrov",
                SpecializationId = newSpecialization.ID,
                ShiftId = newShift.ID,
                IsAccepted = false,
                NewImageFile = null
            };

            await service.UpdateAsync(dto);

            var updated = await context.Doctors.Include(x => x.User).FirstOrDefaultAsync(x => x.ID == seed.doctor.ID);

            Assert.That(updated, Is.Not.Null);
            Assert.That(updated!.User.FirstName, Is.EqualTo("Georgi"));
            Assert.That(updated.User.LastName, Is.EqualTo("Dimitrov"));
            Assert.That(updated.SpecializationId, Is.EqualTo(newSpecialization.ID));
            Assert.That(updated.ShiftId, Is.EqualTo(newShift.ID));
            Assert.That(updated.IsAccepted, Is.False);
            Assert.That(updated.ImageURL, Is.EqualTo("/images/doctor.jpg"));
            Assert.That(updated.CloudinaryID, Is.EqualTo("doctor_public_id"));
        }

        [Test]
        public async Task UpdateAsync_WhenDoctorExistsAndNewImage_UpdatesDataAndImage()
        {
            var seed = await SeedDoctorAsync();

            var newSpecialization = new Specialization
            {
                ID = Guid.NewGuid(),
                SpecializationName = "Dermatology",
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
                .ReturnsAsync(("/images/new-doctor.jpg", "new_public_id"));

            var dto = new DoctorEditDTO
            {
                ID = seed.doctor.ID,
                DoctorName = "Maria Koleva",
                SpecializationId = newSpecialization.ID,
                ShiftId = newShift.ID,
                IsAccepted = true,
                NewImageFile = fileMock.Object
            };

            await service.UpdateAsync(dto);

            var updated = await context.Doctors.Include(x => x.User).FirstOrDefaultAsync(x => x.ID == seed.doctor.ID);

            Assert.That(updated, Is.Not.Null);
            Assert.That(updated!.User.FirstName, Is.EqualTo("Maria"));
            Assert.That(updated.User.LastName, Is.EqualTo("Koleva"));
            Assert.That(updated.SpecializationId, Is.EqualTo(newSpecialization.ID));
            Assert.That(updated.ShiftId, Is.EqualTo(newShift.ID));
            Assert.That(updated.IsAccepted, Is.True);
            Assert.That(updated.ImageURL, Is.EqualTo("/images/new-doctor.jpg"));
            Assert.That(updated.CloudinaryID, Is.EqualTo("new_public_id"));

            imageServiceMock.Verify(x => x.UploadImageAsync(fileMock.Object), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_WhenDoctorMissing_DoesNothing()
        {
            var dto = new DoctorEditDTO
            {
                ID = Guid.NewGuid(),
                DoctorName = "Missing Doctor",
                SpecializationId = Guid.NewGuid(),
                ShiftId = Guid.NewGuid(),
                IsAccepted = false
            };

            await service.UpdateAsync(dto);

            Assert.That(await context.Doctors.CountAsync(), Is.EqualTo(0));
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
        public async Task FilterBySpecialization_WhenMatchesExist_ReturnsMatchingDoctors()
        {
            await SeedDoctorAsync("Ivan", "Petrov", "Cardiology", "Morning");
            await SeedDoctorAsync("Maria", "Ivanova", "Neurology", "Night");
            await SeedDoctorAsync("Georgi", "Kolev", "Cardio Surgery", "Afternoon");

            var result = await service.FilterBySpecialization("Cardio");

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Any(x => x.SpecializationName == "Cardiology"), Is.True);
            Assert.That(result.Any(x => x.SpecializationName == "Cardio Surgery"), Is.True);
            Assert.That(result.Any(x => x.SpecializationName == "Neurology"), Is.False);
        }

        [Test]
        public async Task FilterBySpecialization_WhenNoMatchesExist_ReturnsEmptyList()
        {
            await SeedDoctorAsync("Ivan", "Petrov", "Cardiology", "Morning");

            var result = await service.FilterBySpecialization("Oncology");

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task SortByFirstName_ReturnsDoctorsOrderedByFirstName()
        {
            await SeedDoctorAsync("Zoran", "Petrov", "Cardiology", "Morning");
            await SeedDoctorAsync("Anna", "Ivanova", "Neurology", "Night");
            await SeedDoctorAsync("Boris", "Kolev", "Surgery", "Afternoon");

            var result = await service.SortByFirstName();

            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result[0].UserName, Is.EqualTo("Anna Ivanova"));
            Assert.That(result[1].UserName, Is.EqualTo("Boris Kolev"));
            Assert.That(result[2].UserName, Is.EqualTo("Zoran Petrov"));
        }
    }
}