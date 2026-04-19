using Hospital.Core.Services;
using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.Entities;
using Hospital.Tests.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace Hospital.Tests.Services
{
    [TestFixture]
    public class AdminServiceTests
    {
        private HospitalDbContext context;
        private Mock<UserManager<User>> userManagerMock;
        private AdminService service;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<HospitalDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            context = new HospitalDbContext(options);
            userManagerMock = TestHelpers.CreateUserManagerMock<User>();
            service = new AdminService(context, userManagerMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        [Test]
        public async Task AcceptDoctorAsync_WhenDoctorExists_SetsIsAcceptedTrue()
        {
            var doctor = new Doctor
            {
                ID = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                SpecializationId = Guid.NewGuid(),
                ShiftId = Guid.NewGuid(),
                CloudinaryID = "publicId",
                ImageURL = "imageUrl",
                IsAccepted = false
            };

            context.Doctors.Add(doctor);
            await context.SaveChangesAsync();

            var result = await service.AcceptDoctorAsync(doctor.ID);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.IsAccepted, Is.True);

            var doctorFromDb = await context.Doctors.FindAsync(doctor.ID);
            Assert.That(doctorFromDb, Is.Not.Null);
            Assert.That(doctorFromDb!.IsAccepted, Is.True);
        }

        [Test]
        public async Task AcceptDoctorAsync_WhenDoctorMissing_ReturnsNull()
        {
            var result = await service.AcceptDoctorAsync(Guid.NewGuid());

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task AcceptNurseAsync_WhenNurseExists_SetsIsAcceptedTrue()
        {
            var nurse = new Nurse
            {
                ID = Guid.NewGuid(),
                IsAccepted = false,
                ShiftId = Guid.NewGuid(),
                SpecializationId = Guid.NewGuid(),
                PublicID = "publicId",
                ImageURL = "imageUrl",
                UserId = Guid.NewGuid()
            };

            context.Nurses.Add(nurse);
            await context.SaveChangesAsync();

            var result = await service.AcceptNurseAsync(nurse.ID);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.IsAccepted, Is.True);

            var nurseFromDb = await context.Nurses.FindAsync(nurse.ID);
            Assert.That(nurseFromDb, Is.Not.Null);
            Assert.That(nurseFromDb!.IsAccepted, Is.True);
        }

        [Test]
        public async Task AcceptNurseAsync_WhenNurseMissing_ReturnsNull()
        {
            var result = await service.AcceptNurseAsync(Guid.NewGuid());

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetPendingDoctorsAsync_ReturnsOnlyNotAcceptedDoctors()
        {
            var user1 = new User { Id = Guid.NewGuid(), FirstName = "Aleks", LastName = "Aleks" };
            var user2 = new User { Id = Guid.NewGuid(), FirstName = "Boycheva", LastName = "Boycheva" };
            var spec = new Specialization
            {
                ID = Guid.NewGuid(),
                SpecializationName = "Cardiology",
                ImageURL = "imageUrl",
                PublicID = "publicId"
            };
            var shift = new Shift { ID = Guid.NewGuid(), Type = "Morning" };

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
                    Shift = shift,
                    CloudinaryID = "publicId",
                    ImageURL = "imageUrl",
                    IsAccepted = false
                },
                new Doctor
                {
                    ID = Guid.NewGuid(),
                    UserId = user2.Id,
                    User = user2,
                    SpecializationId = spec.ID,
                    Specialization = spec,
                    ShiftId = shift.ID,
                    Shift = shift,
                    CloudinaryID = "publicId",
                    ImageURL = "imageUrl",
                    IsAccepted = true
                });

            await context.SaveChangesAsync();

            var result = (await service.GetPendingDoctorsAsync()).ToList();

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].IsAccepted, Is.False);
            Assert.That(result[0].User, Is.Not.Null);
            Assert.That(result[0].Specialization, Is.Not.Null);
            Assert.That(result[0].Shift, Is.Not.Null);
        }

        [Test]
        public async Task GetPendingDoctorsAsync_WhenNoPendingDoctors_ReturnsEmptyCollection()
        {
            var user = new User { Id = Guid.NewGuid(), FirstName = "Ivan", LastName = "Petrov" };
            var spec = new Specialization
            {
                ID = Guid.NewGuid(),
                SpecializationName = "Cardiology",
                ImageURL = "imageUrl",
                PublicID = "publicId"
            };
            var shift = new Shift { ID = Guid.NewGuid(), Type = "Morning" };

            context.Users.Add(user);
            context.Specializations.Add(spec);
            context.Shifts.Add(shift);

            context.Doctors.Add(new Doctor
            {
                ID = Guid.NewGuid(),
                UserId = user.Id,
                User = user,
                SpecializationId = spec.ID,
                Specialization = spec,
                ShiftId = shift.ID,
                Shift = shift,
                CloudinaryID = "publicId",
                ImageURL = "imageUrl",
                IsAccepted = true
            });

            await context.SaveChangesAsync();

            var result = await service.GetPendingDoctorsAsync();

            Assert.That(result.Any(), Is.False);
        }

        [Test]
        public async Task GetPendingNursesAsync_ReturnsOnlyNotAcceptedNurses()
        {
            var user1 = new User { Id = Guid.NewGuid(), FirstName = "Aleks", LastName = "Aleks" };
            var user2 = new User { Id = Guid.NewGuid(), FirstName = "Boycheva", LastName = "Boycheva" };
            var spec = new Specialization
            {
                ID = Guid.NewGuid(),
                SpecializationName = "Surgery",
                ImageURL = "imageUrl",
                PublicID = "publicId"
            };
            var shift = new Shift { ID = Guid.NewGuid(), Type = "Night" };

            context.Users.AddRange(user1, user2);
            context.Specializations.Add(spec);
            context.Shifts.Add(shift);

            context.Nurses.AddRange(
                new Nurse
                {
                    ID = Guid.NewGuid(),
                    UserId = user1.Id,
                    User = user1,
                    SpecializationId = spec.ID,
                    Specialization = spec,
                    ShiftId = shift.ID,
                    Shift = shift,
                    PublicID = "publicId",
                    ImageURL = "imageUrl",
                    IsAccepted = false
                },
                new Nurse
                {
                    ID = Guid.NewGuid(),
                    UserId = user2.Id,
                    User = user2,
                    SpecializationId = spec.ID,
                    Specialization = spec,
                    ShiftId = shift.ID,
                    Shift = shift,
                    PublicID = "publicId",
                    ImageURL = "imageUrl",
                    IsAccepted = true
                });

            await context.SaveChangesAsync();

            var result = (await service.GetPendingNursesAsync()).ToList();

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].IsAccepted, Is.False);
            Assert.That(result[0].User, Is.Not.Null);
            Assert.That(result[0].Specialization, Is.Not.Null);
            Assert.That(result[0].Shift, Is.Not.Null);
        }

        [Test]
        public async Task GetPendingNursesAsync_WhenNoPendingNurses_ReturnsEmptyCollection()
        {
            var user = new User { Id = Guid.NewGuid(), FirstName = "Maria", LastName = "Ivanova" };
            var spec = new Specialization
            {
                ID = Guid.NewGuid(),
                SpecializationName = "Surgery",
                ImageURL = "imageUrl",
                PublicID = "publicId"
            };
            var shift = new Shift { ID = Guid.NewGuid(), Type = "Night" };

            context.Users.Add(user);
            context.Specializations.Add(spec);
            context.Shifts.Add(shift);

            context.Nurses.Add(new Nurse
            {
                ID = Guid.NewGuid(),
                UserId = user.Id,
                User = user,
                SpecializationId = spec.ID,
                Specialization = spec,
                ShiftId = shift.ID,
                Shift = shift,
                PublicID = "publicId",
                ImageURL = "imageUrl",
                IsAccepted = true
            });

            await context.SaveChangesAsync();

            var result = await service.GetPendingNursesAsync();

            Assert.That(result.Any(), Is.False);
        }

        [Test]
        public async Task RejectDoctorAsync_WhenDoctorExistsAndUserExists_RemovesDoctorAndDeletesUser()
        {
            var user = new User { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe" };
            var doctor = new Doctor
            {
                ID = Guid.NewGuid(),
                UserId = user.Id,
                User = user,
                SpecializationId = Guid.NewGuid(),
                ShiftId = Guid.NewGuid(),
                CloudinaryID = "publicId",
                ImageURL = "imageUrl",
                IsAccepted = true
            };

            context.Users.Add(user);
            context.Doctors.Add(doctor);
            await context.SaveChangesAsync();

            userManagerMock
                .Setup(x => x.FindByIdAsync(user.Id.ToString()))
                .ReturnsAsync(user);

            userManagerMock
                .Setup(x => x.DeleteAsync(user))
                .ReturnsAsync(IdentityResult.Success);

            var result = await service.RejectDoctorAsync(doctor.ID);

            Assert.That(result, Is.Not.Null);
            Assert.That(await context.Doctors.FindAsync(doctor.ID), Is.Null);

            userManagerMock.Verify(x => x.FindByIdAsync(user.Id.ToString()), Times.Once);
            userManagerMock.Verify(x => x.DeleteAsync(user), Times.Once);
        }

        [Test]
        public async Task RejectDoctorAsync_WhenDoctorExistsButUserMissing_RemovesDoctorWithoutDeletingUser()
        {
            var doctor = new Doctor
            {
                ID = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                SpecializationId = Guid.NewGuid(),
                ShiftId = Guid.NewGuid(),
                CloudinaryID = "publicId",
                ImageURL = "imageUrl",
                IsAccepted = true
            };

            context.Doctors.Add(doctor);
            await context.SaveChangesAsync();

            userManagerMock
                .Setup(x => x.FindByIdAsync(doctor.UserId.ToString()))
                .ReturnsAsync((User?)null);

            var result = await service.RejectDoctorAsync(doctor.ID);

            Assert.That(result, Is.Not.Null);
            Assert.That(await context.Doctors.FindAsync(doctor.ID), Is.Null);

            userManagerMock.Verify(x => x.FindByIdAsync(doctor.UserId.ToString()), Times.Once);
            userManagerMock.Verify(x => x.DeleteAsync(It.IsAny<User>()), Times.Never);
        }

        [Test]
        public async Task RejectDoctorAsync_WhenDoctorMissing_ReturnsNull()
        {
            var result = await service.RejectDoctorAsync(Guid.NewGuid());

            Assert.That(result, Is.Null);
            userManagerMock.Verify(x => x.FindByIdAsync(It.IsAny<string>()), Times.Never);
            userManagerMock.Verify(x => x.DeleteAsync(It.IsAny<User>()), Times.Never);
        }

        [Test]
        public async Task RejectNurseAsync_WhenNurseExistsAndUserExists_RemovesNurseAndDeletesUser()
        {
            var user = new User { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Doe" };
            var nurse = new Nurse
            {
                ID = Guid.NewGuid(),
                IsAccepted = false,
                ShiftId = Guid.NewGuid(),
                SpecializationId = Guid.NewGuid(),
                PublicID = "publicId",
                ImageURL = "imageUrl",
                UserId = user.Id,
                User = user
            };

            context.Users.Add(user);
            context.Nurses.Add(nurse);
            await context.SaveChangesAsync();

            userManagerMock
                .Setup(x => x.FindByIdAsync(user.Id.ToString()))
                .ReturnsAsync(user);

            userManagerMock
                .Setup(x => x.DeleteAsync(user))
                .ReturnsAsync(IdentityResult.Success);

            var result = await service.RejectNurseAsync(nurse.ID);

            Assert.That(result, Is.Not.Null);
            Assert.That(await context.Nurses.FindAsync(nurse.ID), Is.Null);

            userManagerMock.Verify(x => x.FindByIdAsync(user.Id.ToString()), Times.Once);
            userManagerMock.Verify(x => x.DeleteAsync(user), Times.Once);
        }

        [Test]
        public async Task RejectNurseAsync_WhenNurseExistsButUserMissing_RemovesNurseWithoutDeletingUser()
        {
            var nurse = new Nurse
            {
                ID = Guid.NewGuid(),
                IsAccepted = false,
                ShiftId = Guid.NewGuid(),
                SpecializationId = Guid.NewGuid(),
                PublicID = "publicId",
                ImageURL = "imageUrl",
                UserId = Guid.NewGuid()
            };

            context.Nurses.Add(nurse);
            await context.SaveChangesAsync();

            userManagerMock
                .Setup(x => x.FindByIdAsync(nurse.UserId.ToString()))
                .ReturnsAsync((User?)null);

            var result = await service.RejectNurseAsync(nurse.ID);

            Assert.That(result, Is.Not.Null);
            Assert.That(await context.Nurses.FindAsync(nurse.ID), Is.Null);

            userManagerMock.Verify(x => x.FindByIdAsync(nurse.UserId.ToString()), Times.Once);
            userManagerMock.Verify(x => x.DeleteAsync(It.IsAny<User>()), Times.Never);
        }

        [Test]
        public async Task RejectNurseAsync_WhenNurseMissing_ReturnsNull()
        {
            var result = await service.RejectNurseAsync(Guid.NewGuid());

            Assert.That(result, Is.Null);
            userManagerMock.Verify(x => x.FindByIdAsync(It.IsAny<string>()), Times.Never);
            userManagerMock.Verify(x => x.DeleteAsync(It.IsAny<User>()), Times.Never);
        }
    }
}