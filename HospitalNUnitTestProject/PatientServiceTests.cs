using Hospital.Core.Contracts;
using Hospital.Core.DTOs;
using Hospital.Core.Services;
using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace Hospital.Tests.Services
{
    [TestFixture]
    public class PatientServiceTests
    {
        private HospitalDbContext context;
        private Mock<ICityService> cityServiceMock;
        private PatientService service;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<HospitalDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            context = new HospitalDbContext(options);
            cityServiceMock = new Mock<ICityService>();
            service = new PatientService(context, cityServiceMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        private async Task<(Doctor doctor, Room room, User patientUser, Patient patient)> SeedPatientAsync(
            string doctorFirstName = "Ivan",
            string doctorLastName = "Petrov",
            string patientFirstName = "Maria",
            string patientLastName = "Georgieva",
            int roomNumber = 101)
        {
            var doctorUser = new User
            {
                Id = Guid.NewGuid(),
                FirstName = doctorFirstName,
                LastName = doctorLastName,
                UserName = $"{doctorFirstName}_{doctorLastName}",
                Email = $"{doctorFirstName.ToLower()}{Guid.NewGuid():N}@test.com"
            };

            var patientUser = new User
            {
                Id = Guid.NewGuid(),
                FirstName = patientFirstName,
                LastName = patientLastName,
                UserName = $"{patientFirstName}_{patientLastName}",
                Email = $"{patientFirstName.ToLower()}{Guid.NewGuid():N}@test.com"
            };

            var specialization = new Specialization
            {
                ID = Guid.NewGuid(),
                SpecializationName = "Cardiology",
                ImageURL = "imageUrl",
                PublicID = "publicId"
            };

            var shift = new Shift
            {
                ID = Guid.NewGuid(),
                Type = "Morning",
                StartTime = new TimeOnly(8, 0),
                EndTime = new TimeOnly(16, 0)
            };

            var doctor = new Doctor
            {
                ID = Guid.NewGuid(),
                UserId = doctorUser.Id,
                User = doctorUser,
                SpecializationId = specialization.ID,
                Specialization = specialization,
                ShiftId = shift.ID,
                Shift = shift,
                IsAccepted = true,
                ImageURL = "/images/doctor.jpg",
                CloudinaryID = "doctor_public_id"
            };

            var room = new Room
            {
                ID = Guid.NewGuid(),
                RoomNumber = roomNumber,
                IsTaken = false
            };

            var patient = new Patient
            {
                ID = Guid.NewGuid(),
                UserId = patientUser.Id,
                User = patientUser,
                DoctorId = doctor.ID,
                Doctor = doctor,
                RoomId = room.ID,
                Room = room,
                HospitalizationDate = new DateOnly(2025, 1, 10),
                HospitalizationTime = new TimeOnly(9, 30),
                DischargeDate = new DateOnly(2025, 1, 15),
                DischargeTime = new TimeOnly(11, 0),
                BirthCity = "Sofia",
                DateOfBirth = new DateOnly(2000, 5, 20),
                PhoneNumber = "0888123456",
                UCN = "1234567890"
            };

            context.Users.AddRange(doctorUser, patientUser);
            context.Specializations.Add(specialization);
            context.Shifts.Add(shift);
            context.Doctors.Add(doctor);
            context.Rooms.Add(room);
            context.Patients.Add(patient);

            await context.SaveChangesAsync();

            return (doctor, room, patientUser, patient);
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllPatients()
        {
            await SeedPatientAsync("Ivan", "Petrov", "Maria", "Georgieva", 101);
            await SeedPatientAsync("Georgi", "Ivanov", "Elena", "Stoyanova", 102);

            var result = await service.GetAllAsync();

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Any(x => x.UserName == "Maria Georgieva"), Is.True);
            Assert.That(result.Any(x => x.UserName == "Elena Stoyanova"), Is.True);
            Assert.That(result.Any(x => x.DoctorName == "Ivan Petrov"), Is.True);
            Assert.That(result.Any(x => x.RoomNumber == 101), Is.True);
            Assert.That(result.Any(x => x.RoomNumber == 102), Is.True);
        }

        [Test]
        public async Task GetByIdAsync_WhenPatientExists_ReturnsPatient()
        {
            var seed = await SeedPatientAsync();

            var result = await service.GetByIdAsync(seed.patient.ID);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.ID, Is.EqualTo(seed.patient.ID));
            Assert.That(result.UserName, Is.EqualTo("Maria Georgieva"));
            Assert.That(result.DoctorName, Is.EqualTo("Ivan Petrov"));
            Assert.That(result.RoomNumber, Is.EqualTo(101));
            Assert.That(result.BirthCity, Is.EqualTo("Sofia"));
            Assert.That(result.PhoneNumber, Is.EqualTo("0888123456"));
            Assert.That(result.UCN, Is.EqualTo("1234567890"));
        }

        [Test]
        public async Task GetByIdAsync_WhenPatientMissing_ReturnsNull()
        {
            var result = await service.GetByIdAsync(Guid.NewGuid());

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task CreateAsync_WhenValidData_AddsPatientAndUser()
        {
            var doctorSeed = await SeedPatientAsync();
            context.Patients.Remove(doctorSeed.patient);
            context.Users.Remove(doctorSeed.patientUser);
            await context.SaveChangesAsync();

            var model = new PatientCreateDTO
            {
                PatientName = "Anna Dimitrova",
                DoctorId = doctorSeed.doctor.ID,
                HospitalizationDate = new DateOnly(2025, 2, 1),
                HospitalizationTime = new TimeOnly(10, 0),
                DischargeDate = new DateOnly(2025, 2, 5),
                DischargeTime = new TimeOnly(12, 0),
                RoomId = doctorSeed.room.ID,
                BirthCity = "Plovdiv",
                DateOfBirth = new DateOnly(1999, 3, 15),
                PhoneNumber = "0899123456",
                UCN = "0987654321"
            };

            var usersBefore = await context.Users.CountAsync();
            var patientsBefore = await context.Patients.CountAsync();

            await service.CreateAsync(model);

            Assert.That(await context.Users.CountAsync(), Is.EqualTo(usersBefore + 1));
            Assert.That(await context.Patients.CountAsync(), Is.EqualTo(patientsBefore + 1));

            var patient = await context.Patients
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.User.FirstName == "Anna");

            Assert.That(patient, Is.Not.Null);
            Assert.That(patient!.User.FirstName, Is.EqualTo("Anna"));
            Assert.That(patient.User.LastName, Is.EqualTo("Dimitrova"));
            Assert.That(patient.User.UserName, Is.EqualTo("Anna_Dimitrova"));
            Assert.That(patient.DoctorId, Is.EqualTo(model.DoctorId));
            Assert.That(patient.RoomId, Is.EqualTo(model.RoomId));
            Assert.That(patient.BirthCity, Is.EqualTo("Plovdiv"));
            Assert.That(patient.PhoneNumber, Is.EqualTo("0899123456"));
            Assert.That(patient.UCN, Is.EqualTo("0987654321"));
        }

        [Test]
        public async Task CreateAsync_WhenPatientNameHasOnlyFirstName_SetsLastNameEmpty()
        {
            var seed = await SeedPatientAsync();
            context.Patients.Remove(seed.patient);
            context.Users.Remove(seed.patientUser);
            await context.SaveChangesAsync();

            var dto = new PatientCreateDTO
            {
                PatientName = "Ivan",
                DoctorId = seed.doctor.ID,
                RoomId = seed.room.ID,
                HospitalizationDate = new DateOnly(2025, 5, 1),
                HospitalizationTime = new TimeOnly(9, 0),
                DischargeDate = new DateOnly(2025, 5, 5),
                DischargeTime = new TimeOnly(10, 0),
                BirthCity = "Sofia",
                DateOfBirth = new DateOnly(2000, 1, 1),
                PhoneNumber = "123456",
                UCN = "1234567890"
            };

            await service.CreateAsync(dto);

            var patient = await context.Patients
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.User.FirstName == "Ivan");

            Assert.That(patient, Is.Not.Null);
            Assert.That(patient!.User.FirstName, Is.EqualTo("Ivan"));
            Assert.That(patient.User.LastName, Is.EqualTo(""));
            Assert.That(patient.User.UserName, Is.EqualTo("Ivan_"));
        }

        [Test]
        public async Task CreateAsync_WhenPatientNameIsEmpty_SetsBothNamesEmpty()
        {
            var seed = await SeedPatientAsync();
            context.Patients.Remove(seed.patient);
            context.Users.Remove(seed.patientUser);
            await context.SaveChangesAsync();

            var dto = new PatientCreateDTO
            {
                PatientName = "",
                DoctorId = seed.doctor.ID,
                RoomId = seed.room.ID,
                HospitalizationDate = new DateOnly(2025, 5, 1),
                HospitalizationTime = new TimeOnly(9, 0),
                DischargeDate = new DateOnly(2025, 5, 5),
                DischargeTime = new TimeOnly(10, 0),
                BirthCity = "Sofia",
                DateOfBirth = new DateOnly(2000, 1, 1),
                PhoneNumber = "123456",
                UCN = "1234567890"
            };

            await service.CreateAsync(dto);

            var patient = await context.Patients
                .Include(x => x.User)
                .FirstOrDefaultAsync();

            Assert.That(patient, Is.Not.Null);
            Assert.That(patient!.User.FirstName, Is.EqualTo(""));
            Assert.That(patient.User.LastName, Is.EqualTo(""));
            Assert.That(patient.User.UserName, Is.EqualTo("_"));
        }
        [Test]
        public void CreateAsync_WhenDateOfBirthBefore1920_ThrowsException()
        {
            var dto = new PatientCreateDTO
            {
                PatientName = "Ivan Petrov",
                DoctorId = Guid.NewGuid(),
                RoomId = Guid.NewGuid(),
                HospitalizationDate = new DateOnly(2025, 1, 1),
                HospitalizationTime = new TimeOnly(9, 0),
                DischargeDate = new DateOnly(2025, 1, 5),
                DischargeTime = new TimeOnly(10, 0),
                BirthCity = "Sofia",
                DateOfBirth = new DateOnly(1910, 1, 1),
                PhoneNumber = "1111111111",
                UCN = "1111111111"
            };

            var ex = Assert.ThrowsAsync<Exception>(() => service.CreateAsync(dto));
            Assert.That(ex!.Message, Is.EqualTo("Invalid Date of Birth"));
        }
        [Test]
        public void CreateAsync_WhenDateOfBirthInFuture_ThrowsException()
        {
            var dto = new PatientCreateDTO
            {
                PatientName = "Ivan Petrov",
                DoctorId = Guid.NewGuid(),
                RoomId = Guid.NewGuid(),
                HospitalizationDate = new DateOnly(2025, 1, 1),
                HospitalizationTime = new TimeOnly(9, 0),
                DischargeDate = new DateOnly(2025, 1, 5),
                DischargeTime = new TimeOnly(10, 0),
                BirthCity = "Sofia",
                DateOfBirth = DateOnly.FromDateTime(DateTime.Now.AddDays(1)), 
                PhoneNumber = "2222222222",
                UCN = "2222222222"
            };

            var ex = Assert.ThrowsAsync<Exception>(() => service.CreateAsync(dto));
            Assert.That(ex!.Message, Is.EqualTo("Invalid Date of Birth"));
        }
        [Test]
        public void CreateAsync_WhenHospitalizationBefore1985_ThrowsException()
        {
            var dto = new PatientCreateDTO
            {
                PatientName = "Ivan Petrov",
                DoctorId = Guid.NewGuid(),
                RoomId = Guid.NewGuid(),
                HospitalizationDate = new DateOnly(1980, 1, 1),
                HospitalizationTime = new TimeOnly(9, 0),
                DischargeDate = new DateOnly(2025, 1, 5),
                DischargeTime = new TimeOnly(10, 0),
                BirthCity = "Sofia",
                DateOfBirth = new DateOnly(2000, 1, 1),
                PhoneNumber = "3333333333",
                UCN = "3333333333"
            };

            var ex = Assert.ThrowsAsync<Exception>(() => service.CreateAsync(dto));
            Assert.That(ex!.Message, Is.EqualTo("Hospitalization date cannot be before 1985"));
        }
        [Test]
        public void CreateAsync_WhenDischargeBefore1985_ThrowsException()
        {
            var dto = new PatientCreateDTO
            {
                PatientName = "Ivan Petrov",
                DoctorId = Guid.NewGuid(),
                RoomId = Guid.NewGuid(),
                HospitalizationDate = new DateOnly(2025, 1, 1),
                HospitalizationTime = new TimeOnly(9, 0),
                DischargeDate = new DateOnly(1980, 1, 1),
                DischargeTime = new TimeOnly(10, 0),
                BirthCity = "Sofia",
                DateOfBirth = new DateOnly(2000, 1, 1),
                PhoneNumber = "4444444444",
                UCN = "4444444444"
            };

            var ex = Assert.ThrowsAsync<Exception>(() => service.CreateAsync(dto));
            Assert.That(ex!.Message, Is.EqualTo("Discharge date cannot be before 1985"));
        }
        [Test]
        public void CreateAsync_WhenDischargeBeforeHospitalization_ThrowsException()
        {
            var dto = new PatientCreateDTO
            {
                PatientName = "Ivan Petrov",
                DoctorId = Guid.NewGuid(),
                RoomId = Guid.NewGuid(),
                HospitalizationDate = new DateOnly(2025, 5, 10),
                HospitalizationTime = new TimeOnly(9, 0),
                DischargeDate = new DateOnly(2025, 5, 5), 
                DischargeTime = new TimeOnly(10, 0),
                BirthCity = "Sofia",
                DateOfBirth = new DateOnly(2000, 1, 1),
                PhoneNumber = "5555555555",
                UCN = "5555555555"
            };

            var ex = Assert.ThrowsAsync<Exception>(() => service.CreateAsync(dto));
            Assert.That(ex!.Message, Is.EqualTo("Discharge date cannot be before hospitalization date"));
        }
        [Test]
        public async Task CreateAsync_WhenPatientNameHasMoreThanTwoParts_UsesFirstTwo()
        {
            var seed = await SeedPatientAsync();
            context.Patients.Remove(seed.patient);
            context.Users.Remove(seed.patientUser);
            await context.SaveChangesAsync();

            var dto = new PatientCreateDTO
            {
                PatientName = "Ivan Petrov Ivanov",
                DoctorId = seed.doctor.ID,
                RoomId = seed.room.ID,
                HospitalizationDate = new DateOnly(2025, 5, 1),
                HospitalizationTime = new TimeOnly(9, 0),
                DischargeDate = new DateOnly(2025, 5, 5),
                DischargeTime = new TimeOnly(10, 0),
                BirthCity = "Sofia",
                DateOfBirth = new DateOnly(2000, 1, 1),
                PhoneNumber = "123456",
                UCN = "1234567890"
            };

            await service.CreateAsync(dto);

            var patient = await context.Patients
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.User.FirstName == "Ivan");

            Assert.That(patient, Is.Not.Null);
            Assert.That(patient!.User.FirstName, Is.EqualTo("Ivan"));
            Assert.That(patient.User.LastName, Is.EqualTo("Petrov"));
            Assert.That(patient.User.UserName, Is.EqualTo("Ivan_Petrov"));
        }

        [Test]
        public async Task UpdateAsync_WhenPatientExists_UpdatesPatient()
        {
            var seed = await SeedPatientAsync();
            var newDoctorSeed = await SeedPatientAsync("Nikolay", "Dimitrov", "Other", "Patient", 202);

            var model = new PatientEditDTO
            {
                ID = seed.patient.ID,
                PatientName = "Silvia Petrova",
                DoctorId = newDoctorSeed.doctor.ID,
                HospitalizationDate = new DateOnly(2025, 3, 1),
                HospitalizationTime = new TimeOnly(8, 0),
                DischargeDate = new DateOnly(2025, 3, 7),
                DischargeTime = new TimeOnly(10, 0),
                RoomId = newDoctorSeed.room.ID,
                BirthCity = "Varna",
                DateOfBirth = new DateOnly(1998, 4, 10),
                PhoneNumber = "0877000000",
                UCN = "1111111111"
            };

            await service.UpdateAsync(model);

            var updated = await context.Patients
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.ID == seed.patient.ID);

            Assert.That(updated, Is.Not.Null);
            Assert.That(updated!.User.FirstName, Is.EqualTo("Silvia"));
            Assert.That(updated.User.LastName, Is.EqualTo("Petrova"));
            Assert.That(updated.DoctorId, Is.EqualTo(newDoctorSeed.doctor.ID));
            Assert.That(updated.RoomId, Is.EqualTo(newDoctorSeed.room.ID));
            Assert.That(updated.BirthCity, Is.EqualTo("Varna"));
            Assert.That(updated.PhoneNumber, Is.EqualTo("0877000000"));
            Assert.That(updated.UCN, Is.EqualTo("1111111111"));
            Assert.That(updated.HospitalizationDate, Is.EqualTo(new DateOnly(2025, 3, 1)));
        }

        [Test]
        public async Task UpdateAsync_WhenPatientNameHasOnlyFirstName_SetsLastNameEmpty()
        {
            var seed = await SeedPatientAsync();

            var model = new PatientEditDTO
            {
                ID = seed.patient.ID,
                PatientName = "Ivan",
                DoctorId = seed.patient.DoctorId,
                RoomId = seed.patient.RoomId,
                HospitalizationDate = seed.patient.HospitalizationDate,
                HospitalizationTime = seed.patient.HospitalizationTime,
                DischargeDate = seed.patient.DischargeDate,
                DischargeTime = seed.patient.DischargeTime,
                BirthCity = seed.patient.BirthCity,
                DateOfBirth = seed.patient.DateOfBirth,
                PhoneNumber = seed.patient.PhoneNumber,
                UCN = seed.patient.UCN
            };

            await service.UpdateAsync(model);

            var updated = await context.Patients
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.ID == seed.patient.ID);

            Assert.That(updated, Is.Not.Null);
            Assert.That(updated!.User.FirstName, Is.EqualTo("Ivan"));
            Assert.That(updated.User.LastName, Is.EqualTo(""));
        }

        [Test]
        public async Task UpdateAsync_WhenPatientNameIsEmpty_SetsBothNamesEmpty()
        {
            var seed = await SeedPatientAsync();

            var model = new PatientEditDTO
            {
                ID = seed.patient.ID,
                PatientName = "",
                DoctorId = seed.patient.DoctorId,
                RoomId = seed.patient.RoomId,
                HospitalizationDate = seed.patient.HospitalizationDate,
                HospitalizationTime = seed.patient.HospitalizationTime,
                DischargeDate = seed.patient.DischargeDate,
                DischargeTime = seed.patient.DischargeTime,
                BirthCity = seed.patient.BirthCity,
                DateOfBirth = seed.patient.DateOfBirth,
                PhoneNumber = seed.patient.PhoneNumber,
                UCN = seed.patient.UCN
            };

            await service.UpdateAsync(model);

            var updated = await context.Patients
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.ID == seed.patient.ID);

            Assert.That(updated, Is.Not.Null);
            Assert.That(updated!.User.FirstName, Is.EqualTo(""));
            Assert.That(updated.User.LastName, Is.EqualTo(""));
        }

        [Test]
        public async Task UpdateAsync_WhenPatientNameHasMoreThanTwoParts_UsesFirstTwo()
        {
            var seed = await SeedPatientAsync();

            var model = new PatientEditDTO
            {
                ID = seed.patient.ID,
                PatientName = "Ivan Petrov Ivanov",
                DoctorId = seed.patient.DoctorId,
                RoomId = seed.patient.RoomId,
                HospitalizationDate = seed.patient.HospitalizationDate,
                HospitalizationTime = seed.patient.HospitalizationTime,
                DischargeDate = seed.patient.DischargeDate,
                DischargeTime = seed.patient.DischargeTime,
                BirthCity = seed.patient.BirthCity,
                DateOfBirth = seed.patient.DateOfBirth,
                PhoneNumber = seed.patient.PhoneNumber,
                UCN = seed.patient.UCN
            };

            await service.UpdateAsync(model);

            var updated = await context.Patients
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.ID == seed.patient.ID);

            Assert.That(updated, Is.Not.Null);
            Assert.That(updated!.User.FirstName, Is.EqualTo("Ivan"));
            Assert.That(updated.User.LastName, Is.EqualTo("Petrov"));
        }

        [Test]
        public async Task UpdateAsync_WhenPatientMissing_DoesNothing()
        {
            var model = new PatientEditDTO
            {
                ID = Guid.NewGuid(),
                PatientName = "Missing Patient",
                DoctorId = Guid.NewGuid(),
                HospitalizationDate = new DateOnly(2025, 1, 10),
                HospitalizationTime = new TimeOnly(8, 0),
                DischargeDate = new DateOnly(2025, 1, 12),
                DischargeTime = new TimeOnly(10, 0),
                RoomId = Guid.NewGuid(),
                BirthCity = "Sofia",
                DateOfBirth = new DateOnly(2000, 1, 1),
                PhoneNumber = "0000000000",
                UCN = "0000000000"
            };

            await service.UpdateAsync(model);

            Assert.That(await context.Patients.CountAsync(), Is.EqualTo(0));
        }

        [Test]
        public void UpdateAsync_WhenDateOfBirthIsBefore1920_ThrowsException()
        {
            var model = new PatientEditDTO
            {
                ID = Guid.NewGuid(),
                PatientName = "Ivan Petrov",
                DoctorId = Guid.NewGuid(),
                HospitalizationDate = new DateOnly(2025, 1, 10),
                HospitalizationTime = new TimeOnly(9, 0),
                DischargeDate = new DateOnly(2025, 1, 15),
                DischargeTime = new TimeOnly(10, 0),
                RoomId = Guid.NewGuid(),
                BirthCity = "Sofia",
                DateOfBirth = new DateOnly(1910, 1, 1),
                PhoneNumber = "0888123456",
                UCN = "1234567890"
            };

            var ex = Assert.ThrowsAsync<Exception>(async () => await service.UpdateAsync(model));

            Assert.That(ex!.Message, Is.EqualTo("Invalid Date of Birth"));
        }

        [Test]
        public void UpdateAsync_WhenDateOfBirthIsInFuture_ThrowsException()
        {
            var model = new PatientEditDTO
            {
                ID = Guid.NewGuid(),
                PatientName = "Ivan Petrov",
                DoctorId = Guid.NewGuid(),
                HospitalizationDate = new DateOnly(2025, 1, 10),
                HospitalizationTime = new TimeOnly(9, 0),
                DischargeDate = new DateOnly(2025, 1, 15),
                DischargeTime = new TimeOnly(10, 0),
                RoomId = Guid.NewGuid(),
                BirthCity = "Sofia",
                DateOfBirth = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                PhoneNumber = "0888123456",
                UCN = "1234567890"
            };

            var ex = Assert.ThrowsAsync<Exception>(async () => await service.UpdateAsync(model));

            Assert.That(ex!.Message, Is.EqualTo("Invalid Date of Birth"));
        }

        [Test]
        public void UpdateAsync_WhenHospitalizationDateIsBefore1985_ThrowsException()
        {
            var model = new PatientEditDTO
            {
                ID = Guid.NewGuid(),
                PatientName = "Ivan Petrov",
                DoctorId = Guid.NewGuid(),
                HospitalizationDate = new DateOnly(1980, 1, 10),
                HospitalizationTime = new TimeOnly(9, 0),
                DischargeDate = new DateOnly(2025, 1, 15),
                DischargeTime = new TimeOnly(10, 0),
                RoomId = Guid.NewGuid(),
                BirthCity = "Sofia",
                DateOfBirth = new DateOnly(1990, 1, 1),
                PhoneNumber = "0888123456",
                UCN = "1234567890"
            };

            var ex = Assert.ThrowsAsync<Exception>(async () => await service.UpdateAsync(model));

            Assert.That(ex!.Message, Is.EqualTo("Hospitalization date cannot be before 1985"));
        }

        [Test]
        public void UpdateAsync_WhenDischargeDateIsBefore1985_ThrowsException()
        {
            var model = new PatientEditDTO
            {
                ID = Guid.NewGuid(),
                PatientName = "Ivan Petrov",
                DoctorId = Guid.NewGuid(),
                HospitalizationDate = new DateOnly(2025, 1, 10),
                HospitalizationTime = new TimeOnly(9, 0),
                DischargeDate = new DateOnly(1980, 1, 15),
                DischargeTime = new TimeOnly(10, 0),
                RoomId = Guid.NewGuid(),
                BirthCity = "Sofia",
                DateOfBirth = new DateOnly(1990, 1, 1),
                PhoneNumber = "0888123456",
                UCN = "1234567890"
            };

            var ex = Assert.ThrowsAsync<Exception>(async () => await service.UpdateAsync(model));

            Assert.That(ex!.Message, Is.EqualTo("Discharge date cannot be before 1985"));
        }

        [Test]
        public void UpdateAsync_WhenDischargeDateIsBeforeHospitalizationDate_ThrowsException()
        {
            var model = new PatientEditDTO
            {
                ID = Guid.NewGuid(),
                PatientName = "Ivan Petrov",
                DoctorId = Guid.NewGuid(),
                HospitalizationDate = new DateOnly(2025, 1, 10),
                HospitalizationTime = new TimeOnly(9, 0),
                DischargeDate = new DateOnly(2025, 1, 5),
                DischargeTime = new TimeOnly(10, 0),
                RoomId = Guid.NewGuid(),
                BirthCity = "Sofia",
                DateOfBirth = new DateOnly(1990, 1, 1),
                PhoneNumber = "0888123456",
                UCN = "1234567890"
            };

            var ex = Assert.ThrowsAsync<Exception>(async () => await service.UpdateAsync(model));

            Assert.That(ex!.Message, Is.EqualTo("Discharge date cannot be before hospitalization date"));
        }

        [Test]
        public async Task DeleteAsync_WhenPatientExists_RemovesPatient()
        {
            var seed = await SeedPatientAsync();

            await service.DeleteAsync(seed.patient.ID);

            var deleted = await context.Patients.FindAsync(seed.patient.ID);
            Assert.That(deleted, Is.Null);
        }

        [Test]
        public void DeleteAsync_WhenPatientMissing_ThrowsInvalidOperationException()
        {
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await service.DeleteAsync(Guid.NewGuid()));

            Assert.That(ex!.Message, Is.EqualTo("Couldn't remove the patient."));
        }

        [Test]
        public async Task PatientsWithSuchDoctor_WhenDoctorNameIsNull_ReturnsEmptyList()
        {
            var result = await service.PatientsWithSuchDoctor(null!);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task PatientsWithSuchDoctor_WhenDoctorNameIsWhitespace_ReturnsEmptyList()
        {
            var result = await service.PatientsWithSuchDoctor("   ");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task PatientsWithSuchDoctor_WhenDoctorExists_ReturnsMatchingPatients()
        {
            await SeedPatientAsync("Ivan", "Petrov", "Maria", "Georgieva", 101);
            await SeedPatientAsync("Ivan", "Petrov", "Elena", "Stoyanova", 102);
            await SeedPatientAsync("Georgi", "Ivanov", "Anna", "Dimitrova", 103);

            var result = await service.PatientsWithSuchDoctor("Ivan");

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.All(x => x.DoctorName.StartsWith("Ivan")), Is.True);
            Assert.That(result.Any(x => x.UserName == "Maria Georgieva"), Is.True);
            Assert.That(result.Any(x => x.UserName == "Elena Stoyanova"), Is.True);
        }

        [Test]
        public async Task PatientsWithSuchDoctor_WhenNoMatches_ReturnsEmptyList()
        {
            await SeedPatientAsync("Ivan", "Petrov", "Maria", "Georgieva", 101);

            var result = await service.PatientsWithSuchDoctor("Nikolay");

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task Details_WhenPatientExists_ReturnsPatientDetails()
        {
            var seed = await SeedPatientAsync();

            var result = await service.Details(seed.patient.ID);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ID, Is.EqualTo(seed.patient.ID));
            Assert.That(result.UserName, Is.EqualTo("Maria Georgieva"));
            Assert.That(result.DoctorName, Is.EqualTo("Ivan Petrov"));
            Assert.That(result.RoomNumber, Is.EqualTo(101));
            Assert.That(result.BirthCity, Is.EqualTo("Sofia"));
            Assert.That(result.PhoneNumber, Is.EqualTo("0888123456"));
            Assert.That(result.UCN, Is.EqualTo("1234567890"));
        }

        [Test]
        public void Details_WhenPatientMissing_ThrowsInvalidOperationException()
        {
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await service.Details(Guid.NewGuid()));

            Assert.That(ex!.Message, Is.EqualTo("Patient was not found."));
        }
    }
}