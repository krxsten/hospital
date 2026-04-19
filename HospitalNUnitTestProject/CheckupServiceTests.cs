using Hospital.Core.DTOs;
using Hospital.Core.Services;
using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.Entities;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Hospital.Tests.Services
{
    [TestFixture]
    public class CheckupServiceTests
    {
        private HospitalDbContext context;
        private CheckupService service;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<HospitalDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            context = new HospitalDbContext(options);
            service = new CheckupService(context);
        }

        [TearDown]
        public void TearDown()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        private async Task<(Doctor doctor, Patient patient, Shift shift)> SeedDoctorPatientAsync(
            string doctorFirstName = "Ivan",
            string doctorLastName = "Petrov",
            string patientFirstName = "Maria",
            string patientLastName = "Georgieva",
            TimeOnly? shiftStart = null,
            TimeOnly? shiftEnd = null)
        {
            var doctorUser = new User
            {
                Id = Guid.NewGuid(),
                FirstName = doctorFirstName,
                LastName = doctorLastName,
                UserName = "doctor_" + Guid.NewGuid().ToString("N"),
                Email = "doctor@test.com"
            };

            var patientUser = new User
            {
                Id = Guid.NewGuid(),
                FirstName = patientFirstName,
                LastName = patientLastName,
                UserName = "patient_" + Guid.NewGuid().ToString("N"),
                Email = "patient@test.com"
            };
            var shift = new Shift
            {
                ID = Guid.NewGuid(),
                Type = "Morning",
                StartTime = shiftStart ?? new TimeOnly(8, 0),
                EndTime = shiftEnd ?? new TimeOnly(12, 0)
            };

            var specialization = new Specialization
            {
                ID = Guid.NewGuid(),
                SpecializationName = "Cardiology",
                ImageURL = "imageUrl",
                PublicID = "publicId"
            };

            var doctor = new Doctor
            {
                ID = Guid.NewGuid(),
                UserId = doctorUser.Id,
                User = doctorUser,
                ShiftId = shift.ID,
                Shift = shift,
                SpecializationId = specialization.ID,
                Specialization = specialization,
                CloudinaryID = "cloudinaryId",
                ImageURL = "doctorImage",
                IsAccepted = true
            };

            var patient = new Patient
            {
                ID = Guid.NewGuid(),
                UserId = patientUser.Id,
                User = patientUser,
                BirthCity = "Sofia",
                PhoneNumber = "0888123456",
                UCN = "1234567890"
            };

            context.Users.AddRange(doctorUser, patientUser);
            context.Shifts.Add(shift);
            context.Specializations.Add(specialization);
            context.Doctors.Add(doctor);
            context.Patients.Add(patient);

            await context.SaveChangesAsync();

            return (doctor, patient, shift);
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllCheckupsOrderedByDateThenPatientName()
        {
            var seed1 = await SeedDoctorPatientAsync("Ivan", "Petrov", "Zara", "Zlateva");
            var seed2 = await SeedDoctorPatientAsync("Georgi", "Ivanov", "Anna", "Angelova");

            context.Checkups.AddRange(
                new Checkup
                {
                    ID = Guid.NewGuid(),
                    Date = new DateOnly(2025, 5, 20),
                    Time = new TimeOnly(10, 0),
                    DoctorID = seed1.doctor.ID,
                    Doctor = seed1.doctor,
                    PatientID = seed1.patient.ID,
                    Patient = seed1.patient
                },
                new Checkup
                {
                    ID = Guid.NewGuid(),
                    Date = new DateOnly(2025, 5, 19),
                    Time = new TimeOnly(9, 0),
                    DoctorID = seed2.doctor.ID,
                    Doctor = seed2.doctor,
                    PatientID = seed2.patient.ID,
                    Patient = seed2.patient
                },
                new Checkup
                {
                    ID = Guid.NewGuid(),
                    Date = new DateOnly(2025, 5, 20),
                    Time = new TimeOnly(8, 30),
                    DoctorID = seed2.doctor.ID,
                    Doctor = seed2.doctor,
                    PatientID = seed2.patient.ID,
                    Patient = seed2.patient
                });

            await context.SaveChangesAsync();

            var result = await service.GetAllAsync();

            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result[0].Date, Is.EqualTo(new DateOnly(2025, 5, 19)));
            Assert.That(result[1].PatientName, Is.EqualTo("Anna Angelova"));
            Assert.That(result[2].PatientName, Is.EqualTo("Zara Zlateva"));
        }

        [Test]
        public async Task GetPatientAppointmentAsync_ReturnsOnlyAppointmentsForGivenPatient()
        {
            var seed1 = await SeedDoctorPatientAsync();
            var seed2 = await SeedDoctorPatientAsync("Dimitar", "Kolev", "Petya", "Stoyanova");

            context.Checkups.AddRange(
                new Checkup
                {
                    ID = Guid.NewGuid(),
                    Date = new DateOnly(2025, 6, 1),
                    Time = new TimeOnly(9, 0),
                    DoctorID = seed1.doctor.ID,
                    Doctor = seed1.doctor,
                    PatientID = seed1.patient.ID,
                    Patient = seed1.patient
                },
                new Checkup
                {
                    ID = Guid.NewGuid(),
                    Date = new DateOnly(2025, 6, 2),
                    Time = new TimeOnly(10, 0),
                    DoctorID = seed1.doctor.ID,
                    Doctor = seed1.doctor,
                    PatientID = seed2.patient.ID,
                    Patient = seed2.patient
                });

            await context.SaveChangesAsync();

            var result = await service.GetPatientAppointmentAsync(seed1.patient.ID);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].PatientID, Is.EqualTo(seed1.patient.ID));
            Assert.That(result[0].DoctorName, Is.EqualTo("Ivan Petrov"));
            Assert.That(result[0].PatientName, Is.EqualTo("Maria Georgieva"));
        }

        [Test]
        public async Task GetByIdAsync_WhenCheckupExists_ReturnsMappedDto()
        {
            var seed = await SeedDoctorPatientAsync();

            var checkup = new Checkup
            {
                ID = Guid.NewGuid(),
                Date = new DateOnly(2025, 7, 10),
                Time = new TimeOnly(11, 30),
                DoctorID = seed.doctor.ID,
                Doctor = seed.doctor,
                PatientID = seed.patient.ID,
                Patient = seed.patient
            };

            context.Checkups.Add(checkup);
            await context.SaveChangesAsync();

            var result = await service.GetByIdAsync(checkup.ID);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.ID, Is.EqualTo(checkup.ID));
            Assert.That(result.DoctorName, Is.EqualTo("Ivan Petrov"));
            Assert.That(result.PatientName, Is.EqualTo("Maria Georgieva"));
            Assert.That(result.Time, Is.EqualTo(new TimeOnly(11, 30)));
        }

        [Test]
        public async Task GetByIdAsync_WhenCheckupMissing_ReturnsNull()
        {
            var result = await service.GetByIdAsync(Guid.NewGuid());

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task CreateAsync_AddsCheckupToDatabase()
        {
            var seed = await SeedDoctorPatientAsync();

            var model = new CheckupCreateDTO
            {
                Date = new DateOnly(2025, 8, 1),
                Time = new TimeOnly(14, 0),
                DoctorID = seed.doctor.ID,
                PatientID = seed.patient.ID
            };

            await service.CreateAsync(model);

            var checkup = await context.Checkups.FirstOrDefaultAsync();

            Assert.That(checkup, Is.Not.Null);
            Assert.That(checkup!.Date, Is.EqualTo(model.Date));
            Assert.That(checkup.Time, Is.EqualTo(model.Time));
            Assert.That(checkup.DoctorID, Is.EqualTo(model.DoctorID));
            Assert.That(checkup.PatientID, Is.EqualTo(model.PatientID));
        }

        [Test]
        public async Task UpdateAsync_WhenCheckupExists_UpdatesProperties()
        {
            var seed = await SeedDoctorPatientAsync();
            var otherSeed = await SeedDoctorPatientAsync("Nikolay", "Dimitrov", "Elena", "Marinova");

            var checkup = new Checkup
            {
                ID = Guid.NewGuid(),
                Date = new DateOnly(2025, 8, 2),
                Time = new TimeOnly(9, 0),
                DoctorID = seed.doctor.ID,
                PatientID = seed.patient.ID
            };

            context.Checkups.Add(checkup);
            await context.SaveChangesAsync();

            var model = new CheckupEditDTO
            {
                ID = checkup.ID,
                Date = new DateOnly(2025, 8, 5),
                Time = new TimeOnly(10, 30),
                DoctorID = otherSeed.doctor.ID
            };

            await service.UpdateAsync(model);

            var updated = await context.Checkups.FindAsync(checkup.ID);

            Assert.That(updated, Is.Not.Null);
            Assert.That(updated!.Date, Is.EqualTo(model.Date));
            Assert.That(updated.Time, Is.EqualTo(model.Time));
            Assert.That(updated.DoctorID, Is.EqualTo(model.DoctorID));
            Assert.That(updated.PatientID, Is.EqualTo(seed.patient.ID));
        }

        [Test]
        public async Task UpdateAsync_WhenCheckupMissing_DoesNothing()
        {
            var seed = await SeedDoctorPatientAsync();

            var model = new CheckupEditDTO
            {
                ID = Guid.NewGuid(),
                Date = new DateOnly(2025, 8, 5),
                Time = new TimeOnly(10, 30),
                DoctorID = seed.doctor.ID
            };

            await service.UpdateAsync(model);

            Assert.That(await context.Checkups.CountAsync(), Is.EqualTo(0));
        }

        [Test]
        public async Task DeleteAsync_WhenCheckupExists_RemovesIt()
        {
            var seed = await SeedDoctorPatientAsync();

            var checkup = new Checkup
            {
                ID = Guid.NewGuid(),
                Date = new DateOnly(2025, 9, 1),
                Time = new TimeOnly(8, 0),
                DoctorID = seed.doctor.ID,
                PatientID = seed.patient.ID
            };

            context.Checkups.Add(checkup);
            await context.SaveChangesAsync();

            await service.DeleteAsync(checkup.ID);

            var deleted = await context.Checkups.FindAsync(checkup.ID);
            Assert.That(deleted, Is.Null);
        }

        [Test]
        public async Task DeleteAsync_WhenCheckupMissing_DoesNothing()
        {
            await service.DeleteAsync(Guid.NewGuid());

            Assert.That(await context.Checkups.CountAsync(), Is.EqualTo(0));
        }

        [Test]
        public async Task GetBusyTimes_ReturnsOnlyDatesForDoctorOnGivenDate()
        {
            var seed = await SeedDoctorPatientAsync();
            var otherSeed = await SeedDoctorPatientAsync("Stoyan", "Iliev", "Mira", "Ivanova");

            context.Checkups.AddRange(
                new Checkup
                {
                    ID = Guid.NewGuid(),
                    Date = new DateOnly(2025, 10, 15),
                    Time = new TimeOnly(9, 0),
                    DoctorID = seed.doctor.ID,
                    PatientID = seed.patient.ID
                },
                new Checkup
                {
                    ID = Guid.NewGuid(),
                    Date = new DateOnly(2025, 10, 15),
                    Time = new TimeOnly(10, 0),
                    DoctorID = seed.doctor.ID,
                    PatientID = seed.patient.ID
                },
                new Checkup
                {
                    ID = Guid.NewGuid(),
                    Date = new DateOnly(2025, 10, 16),
                    Time = new TimeOnly(9, 0),
                    DoctorID = seed.doctor.ID,
                    PatientID = seed.patient.ID
                },
                new Checkup
                {
                    ID = Guid.NewGuid(),
                    Date = new DateOnly(2025, 10, 15),
                    Time = new TimeOnly(11, 0),
                    DoctorID = otherSeed.doctor.ID,
                    PatientID = otherSeed.patient.ID
                });

            await context.SaveChangesAsync();

            var result = await service.GetBusyTimes(seed.doctor.ID, new DateTime(2025, 10, 15));

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.All(x => x == new DateOnly(2025, 10, 15)), Is.True);
        }

        [Test]
        public async Task GetDoctorShift_WhenDoctorExists_ReturnsShiftDto()
        {
            var seed = await SeedDoctorPatientAsync(shiftStart: new TimeOnly(7, 30), shiftEnd: new TimeOnly(13, 30));

            var result = await service.GetDoctorShift(seed.doctor.ID);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.StartTime, Is.EqualTo(new TimeOnly(7, 30)));
            Assert.That(result.EndTime, Is.EqualTo(new TimeOnly(13, 30)));
        }

        [Test]
        public async Task GetDoctorShift_WhenDoctorMissing_ReturnsNull()
        {
            var result = await service.GetDoctorShift(Guid.NewGuid());

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetCheckupsDate_ReturnsOnlyCheckupsForGivenDate()
        {
            var seed = await SeedDoctorPatientAsync();
            var otherSeed = await SeedDoctorPatientAsync("Kiril", "Vasilev", "Diana", "Popova");

            context.Checkups.AddRange(
                new Checkup
                {
                    ID = Guid.NewGuid(),
                    Date = new DateOnly(2025, 11, 20),
                    Time = new TimeOnly(8, 0),
                    DoctorID = seed.doctor.ID,
                    Doctor = seed.doctor,
                    PatientID = seed.patient.ID,
                    Patient = seed.patient
                },
                new Checkup
                {
                    ID = Guid.NewGuid(),
                    Date = new DateOnly(2025, 11, 20),
                    Time = new TimeOnly(9, 30),
                    DoctorID = otherSeed.doctor.ID,
                    Doctor = otherSeed.doctor,
                    PatientID = otherSeed.patient.ID,
                    Patient = otherSeed.patient
                },
                new Checkup
                {
                    ID = Guid.NewGuid(),
                    Date = new DateOnly(2025, 11, 21),
                    Time = new TimeOnly(10, 0),
                    DoctorID = seed.doctor.ID,
                    Doctor = seed.doctor,
                    PatientID = seed.patient.ID,
                    Patient = seed.patient
                });

            await context.SaveChangesAsync();

            var result = await service.GetCheckupsDate(new DateOnly(2025, 11, 20));

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.All(x => x.Date == new DateOnly(2025, 11, 20)), Is.True);
        }

        [Test]
        public async Task GetAvailableTimeSlotsAsync_WhenDoctorMissing_ReturnsEmptyList()
        {
            var result = await service.GetAvailableTimeSlotsAsync(Guid.NewGuid(), new DateOnly(2025, 12, 1));

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task GetAvailableTimeSlotsAsync_WhenDoctorHasNoShift_ReturnsEmptyList()
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = "No",
                LastName = "Shift"
            };

            var specialization = new Specialization
            {
                ID = Guid.NewGuid(),
                SpecializationName = "Neurology",
                ImageURL = "imageUrl",
                PublicID = "publicId"
            };

            var doctor = new Doctor
            {
                ID = Guid.NewGuid(),
                UserId = user.Id,
                User = user,
                SpecializationId = specialization.ID,
                Specialization = specialization,
                ShiftId = Guid.NewGuid(),
                Shift = null!,
                CloudinaryID = "cloudId",
                ImageURL = "img",
                IsAccepted = true
            };

            context.Users.Add(user);
            context.Specializations.Add(specialization);
            context.Doctors.Add(doctor);
            await context.SaveChangesAsync();

            var result = await service.GetAvailableTimeSlotsAsync(doctor.ID, new DateOnly(2025, 12, 1));

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task GetAvailableTimeSlotsAsync_ReturnsOnlyFreeHalfHourSlots()
        {
            var seed = await SeedDoctorPatientAsync(shiftStart: new TimeOnly(8, 0), shiftEnd: new TimeOnly(10, 0));
            var date = new DateOnly(2025, 12, 15);

            context.Checkups.AddRange(
                new Checkup
                {
                    ID = Guid.NewGuid(),
                    Date = date,
                    Time = new TimeOnly(8, 30),
                    DoctorID = seed.doctor.ID,
                    PatientID = seed.patient.ID
                },
                new Checkup
                {
                    ID = Guid.NewGuid(),
                    Date = date,
                    Time = new TimeOnly(9, 30),
                    DoctorID = seed.doctor.ID,
                    PatientID = seed.patient.ID
                });

            await context.SaveChangesAsync();

            var result = await service.GetAvailableTimeSlotsAsync(seed.doctor.ID, date);

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result, Does.Contain(new TimeOnly(8, 0)));
            Assert.That(result, Does.Contain(new TimeOnly(9, 0)));
            Assert.That(result, Does.Not.Contain(new TimeOnly(8, 30)));
            Assert.That(result, Does.Not.Contain(new TimeOnly(9, 30)));
        }
    }
}