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
           // userManagerMock = TestHelpers.CreateUserManagerMock();

           // service = new AdminService(context, userManagerMock.Object);
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
                IsAccepted = false
            };

            context.Doctors.Add(doctor);
            await context.SaveChangesAsync();

            var result = await service.AcceptDoctorAsync(doctor.ID);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.IsAccepted, Is.True);

            var doctorFromDb = await context.Doctors.FindAsync(doctor.ID);
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
                IsAccepted = false
            };

            context.Nurses.Add(nurse);
            await context.SaveChangesAsync();

            var result = await service.AcceptNurseAsync(nurse.ID);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.IsAccepted, Is.True);

            var nurseFromDb = await context.Nurses.FindAsync(nurse.ID);
            Assert.That(nurseFromDb!.IsAccepted, Is.True);
        }

        [Test]
        public async Task GetPendingDoctorsAsync_ReturnsOnlyNotAcceptedDoctors()
        {
            var user1 = new User { Id = Guid.NewGuid(), FirstName = "Aleks", LastName = "Aleks" };
            var user2 = new User { Id = Guid.NewGuid(), FirstName = "Boycheva", LastName = "Boycheva" };
            var spec = new Specialization { ID = Guid.NewGuid(), SpecializationName = "Cardiology" };
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
                    IsAccepted = true
                });

            await context.SaveChangesAsync();

            var result = await service.GetPendingDoctorsAsync();

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().IsAccepted, Is.False);
        }

        [Test]
        public async Task GetPendingNursesAsync_ReturnsOnlyNotAcceptedNurses()
        {
            var user1 = new User { Id = Guid.NewGuid(), FirstName = "Aleks", LastName = "Aleks" };
            var user2 = new User { Id = Guid.NewGuid(), FirstName = "Boycheva", LastName = "Boycheva" };
            var spec = new Specialization { ID = Guid.NewGuid(), SpecializationName = "Surgery" };
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
                    IsAccepted = true
                });

            await context.SaveChangesAsync();

            var result = await service.GetPendingNursesAsync();

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().IsAccepted, Is.False);
        }

        [Test]
        public async Task RejectDoctorAsync_WhenDoctorExists_RemovesDoctorAndDeletesUser()
        {
            var user = new User { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe" };
            var doctor = new Doctor
            {
                ID = Guid.NewGuid(),
                UserId = user.Id
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
        public async Task RejectNurseAsync_WhenNurseExists_RemovesNurseAndDeletesUser()
        {
            var user = new User { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Doe" };
            var nurse = new Nurse
            {
                ID = Guid.NewGuid(),
                UserId = user.Id
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
    }
}