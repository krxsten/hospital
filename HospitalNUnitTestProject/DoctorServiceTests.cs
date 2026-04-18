using CloudinaryDotNet.Actions;
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
          //  userManagerMock = TestHelpers.CreateUserManagerMock();

          //  service = new DoctorService(context, imageServiceMock.Object, userManagerMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        [Test]
        public async Task CreateAsync_WithValidData_CreatesUserAndDoctor()
        {
            var spec = new Specialization { ID = Guid.NewGuid(), SpecializationName = "Cardiology" };
            var shift = new Shift { ID = Guid.NewGuid(), Type = "Morning" };

            context.Specializations.Add(spec);
            context.Shifts.Add(shift);
            await context.SaveChangesAsync();

            var fileMock = new Mock<IFormFile>();

            imageServiceMock
     .Setup(x => x.UploadImageAsync(It.IsAny<IFormFile>()))
     .ReturnsAsync(("http://image.com/a.jpg", "public-id"));

            var dto = new DoctorCreateDto
            {
                DoctorName = "John Doe",
                SpecializationID = spec.ID,
                ShiftID = shift.ID,
                IsAccepted = true,
                ImageFile = fileMock.Object
            };

            await service.CreateAsync(dto);

            Assert.That(context.Users.Count(), Is.EqualTo(1));
            Assert.That(context.Doctors.Count(), Is.EqualTo(1));

            var user = await context.Users.FirstAsync();
            var doctor = await context.Doctors.FirstAsync();

            Assert.That(user.FirstName, Is.EqualTo("John"));
            Assert.That(user.LastName, Is.EqualTo("Doe"));
            Assert.That(doctor.UserId, Is.EqualTo(user.Id));
            Assert.That(doctor.ImageURL, Is.EqualTo("http://image.com/a.jpg"));
            Assert.That(doctor.CloudinaryID, Is.EqualTo("public-id"));
        }

        [Test]
        public void CreateAsync_WithoutImage_ThrowsException()
        {
            var dto = new DoctorCreateDto
            {
                DoctorName = "John Doe",
                SpecializationID = Guid.NewGuid(),
                ShiftID = Guid.NewGuid(),
                IsAccepted = true,
                ImageFile = null
            };

            Assert.ThrowsAsync<Exception>(async () => await service.CreateAsync(dto));
        }

        [Test]
        public async Task DeleteAsync_WhenDoctorExists_RemovesDoctor()
        {
            var doctor = new Doctor { ID = Guid.NewGuid() };
            context.Doctors.Add(doctor);
            await context.SaveChangesAsync();

            await service.DeleteAsync(doctor.ID);

            Assert.That(await context.Doctors.FindAsync(doctor.ID), Is.Null);
        }

        [Test]
        public async Task DeleteAsync_WhenDoctorMissing_DoesNothing()
        {
            Assert.DoesNotThrowAsync(async () => await service.DeleteAsync(Guid.NewGuid()));
        }

        [Test]
        public async Task GetAllAsync_ReturnsMappedDoctors()
        {
            var user = new User { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe" };
            var spec = new Specialization { ID = Guid.NewGuid(), SpecializationName = "Cardiology" };
            var shift = new Shift { ID = Guid.NewGuid(), Type = "Morning" };

            var doctor = new Doctor
            {
                ID = Guid.NewGuid(),
                UserId = user.Id,
                User = user,
                SpecializationId = spec.ID,
                Specialization = spec,
                ShiftId = shift.ID,
                Shift = shift,
                IsAccepted = true,
                ImageURL = "img"
            };

            context.Users.Add(user);
            context.Specializations.Add(spec);
            context.Shifts.Add(shift);
            context.Doctors.Add(doctor);
            await context.SaveChangesAsync();

            var result = await service.GetAllAsync();

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].UserName, Is.EqualTo("John Doe"));
            Assert.That(result[0].SpecializationName, Is.EqualTo("Cardiology"));
            Assert.That(result[0].ShiftName, Is.EqualTo("Morning"));
        }

        [Test]
        public async Task GetByIdAsync_WhenExists_ReturnsMappedDoctor()
        {
            var user = new User { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe" };
            var spec = new Specialization { ID = Guid.NewGuid(), SpecializationName = "Cardiology" };
            var shift = new Shift { ID = Guid.NewGuid(), Type = "Morning" };

            var doctor = new Doctor
            {
                ID = Guid.NewGuid(),
                UserId = user.Id,
                User = user,
                SpecializationId = spec.ID,
                Specialization = spec,
                ShiftId = shift.ID,
                Shift = shift,
                IsAccepted = true,
                ImageURL = "img"
            };

            context.Users.Add(user);
            context.Specializations.Add(spec);
            context.Shifts.Add(shift);
            context.Doctors.Add(doctor);
            await context.SaveChangesAsync();

            var result = await service.GetByIdAsync(doctor.ID);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.UserName, Is.EqualTo("John Doe"));
        }

        [Test]
        public async Task GetByIdAsync_WhenMissing_ReturnsNull()
        {
            var result = await service.GetByIdAsync(Guid.NewGuid());

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task UpdateAsync_WhenDoctorExists_UpdatesDoctorAndUser()
        {
            var user = new User { Id = Guid.NewGuid(), FirstName = "Old", LastName = "Name" };
            var oldSpec = new Specialization { ID = Guid.NewGuid(), SpecializationName = "OldSpec" };
            var newSpec = new Specialization { ID = Guid.NewGuid(), SpecializationName = "NewSpec" };
            var oldShift = new Shift { ID = Guid.NewGuid(), Type = "OldShift" };
            var newShift = new Shift { ID = Guid.NewGuid(), Type = "NewShift" };

            var doctor = new Doctor
            {
                ID = Guid.NewGuid(),
                UserId = user.Id,
                User = user,
                SpecializationId = oldSpec.ID,
                ShiftId = oldShift.ID,
                IsAccepted = false,
                ImageURL = "old-url",
                CloudinaryID = "old-id"
            };

            context.Users.Add(user);
            context.Specializations.AddRange(oldSpec, newSpec);
            context.Shifts.AddRange(oldShift, newShift);
            context.Doctors.Add(doctor);
            await context.SaveChangesAsync();

            var fileMock = new Mock<IFormFile>();

            //imageServiceMock
            //    .Setup(x => x.UploadImageAsync(It.IsAny<IFormFile>()))
            //    .ReturnsAsync(new ImageUploadResultDTO
            //    {
            //        Url = "new-url",
            //        PublicId = "new-id"
            //    });

            var dto = new DoctorEditDTO
            {
                ID = doctor.ID,
                DoctorName = "New Name",
                SpecializationId = newSpec.ID,
                ShiftId = newShift.ID,
                IsAccepted = true,
                NewImageFile = fileMock.Object
            };

            await service.UpdateAsync(dto);

            var updatedDoctor = await context.Doctors.Include(x => x.User).FirstAsync(x => x.ID == doctor.ID);

            Assert.That(updatedDoctor.User.FirstName, Is.EqualTo("New"));
            Assert.That(updatedDoctor.User.LastName, Is.EqualTo("Name"));
            Assert.That(updatedDoctor.SpecializationId, Is.EqualTo(newSpec.ID));
            Assert.That(updatedDoctor.ShiftId, Is.EqualTo(newShift.ID));
            Assert.That(updatedDoctor.IsAccepted, Is.True);
            Assert.That(updatedDoctor.ImageURL, Is.EqualTo("new-url"));
            Assert.That(updatedDoctor.CloudinaryID, Is.EqualTo("new-id"));
        }

        [Test]
        public async Task FilterBySpecialization_WithEmptyString_ReturnsEmptyList()
        {
            var result = await service.FilterBySpecialization("");

            Assert.That(result, Is.Empty);
        }

        [Test]
        public async Task FilterBySpecialization_ReturnsMatchingDoctors()
        {
            var user = new User { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe" };
            var spec = new Specialization { ID = Guid.NewGuid(), SpecializationName = "Cardiology" };
            var shift = new Shift { ID = Guid.NewGuid(), Type = "Morning" };

            var doctor = new Doctor
            {
                ID = Guid.NewGuid(),
                UserId = user.Id,
                User = user,
                SpecializationId = spec.ID,
                Specialization = spec,
                ShiftId = shift.ID,
                Shift = shift
            };

            context.Users.Add(user);
            context.Specializations.Add(spec);
            context.Shifts.Add(shift);
            context.Doctors.Add(doctor);
            await context.SaveChangesAsync();

            var result = await service.FilterBySpecialization("Cardio");

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].SpecializationName, Is.EqualTo("Cardiology"));
        }

        [Test]
        public async Task SortByFirstName_ReturnsOrderedDoctors()
        {
            var spec = new Specialization { ID = Guid.NewGuid(), SpecializationName = "Spec" };
            var shift = new Shift { ID = Guid.NewGuid(), Type = "Morning" };

            var user1 = new User { Id = Guid.NewGuid(), FirstName = "Zed", LastName = "A" };
            var user2 = new User { Id = Guid.NewGuid(), FirstName = "Anna", LastName = "B" };

            context.Users.AddRange(user1, user2);
            context.Specializations.Add(spec);
            context.Shifts.Add(shift);

            context.Doctors.AddRange(
                new Doctor
                {
                    ID = Guid.NewGuid(),
                    UserId = user1.Id,
                    User = user1,
                    SpecializationId = spec.ID,
                    Specialization = spec,
                    ShiftId = shift.ID,
                    Shift = shift
                },
                new Doctor
                {
                    ID = Guid.NewGuid(),
                    UserId = user2.Id,
                    User = user2,
                    SpecializationId = spec.ID,
                    Specialization = spec,
                    ShiftId = shift.ID,
                    Shift = shift
                });

            await context.SaveChangesAsync();

            var result = await service.SortByFirstName();

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].UserName, Is.EqualTo("Anna B"));
            Assert.That(result[1].UserName, Is.EqualTo("Zed A"));
        }
    }
}