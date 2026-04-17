using Hospital.Core.Contracts;
using Hospital.Core.DTOs;
using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Services
{
    public class PatientService : IPatientService
    {
        private readonly HospitalDbContext context;
        private readonly ICityService cityService;
        public PatientService(HospitalDbContext context, ICityService cityService)
        {
            this.context = context;
            this.cityService = cityService;
        }

        public async Task<List<PatientIndexDTO>> GetAllAsync()
        {
            var patients = await context.Patients
                .Include(p => p.Doctor.User)
                .Include(p => p.Room)
                .Include(p => p.User)
                .ToListAsync();

            return patients.Select(x => new PatientIndexDTO
            {
                ID = x.ID,
                DoctorId = x.DoctorId,
                DoctorName = x.Doctor?.User != null ? x.Doctor.User.FirstName + " " + x.Doctor.User.LastName : "No data",
                HospitalizationDate = x.HospitalizationDate,
                HospitalizationTime = x.HospitalizationTime,
                DischargeDate = x.DischargeDate,
                DischargeTime = x.DischargeTime,
                UserID = x.UserId,
                UserName = x.User != null ? x.User.FirstName + " " + x.User.LastName : "No data",
                RoomId = x.RoomId,
                RoomNumber = x.Room?.RoomNumber ?? 0,
                BirthCity = x.BirthCity,
                DateOfBirth = x.DateOfBirth,
                PhoneNumber = x.PhoneNumber,
                UCN = x.UCN
            }).ToList();
        }

        public async Task<PatientIndexDTO?> GetByIdAsync(Guid id)
        {
            var x = await context.Patients
                .Include(p => p.Doctor)
                .ThenInclude(p=> p.User)
                .Include(p => p.Room)
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.ID == id);

            if (x == null) return null;

            return new PatientIndexDTO
            {
                ID = x.ID,
                DoctorId = x.DoctorId,
                DoctorName = x.Doctor?.User != null ? x.Doctor.User.FirstName + " " + x.Doctor.User.LastName : "No data",
                HospitalizationDate = x.HospitalizationDate,
                HospitalizationTime = x.HospitalizationTime,
                DischargeDate = x.DischargeDate,
                DischargeTime = x.DischargeTime,
                UserID = x.UserId,
                UserName = x.User != null ? x.User.FirstName + " " + x.User.LastName : "No data",
                RoomId = x.RoomId,
                RoomNumber = x.Room?.RoomNumber ?? 0,
                BirthCity = x.BirthCity,
                DateOfBirth = x.DateOfBirth,
                PhoneNumber = x.PhoneNumber,
                UCN = x.UCN
            };
        }

        public async Task CreateAsync(PatientCreateDTO model)
        {
            var names = model.PatientName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var firstName = names.Length > 0 ? names[0] : "";
            var lastName = names.Length > 1 ? names[1] : "";

            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                UserName = firstName + "_" + lastName
            };
            await context.Users.AddAsync(user);

            var patient = new Patient
            {
                UserId = user.Id,
                DoctorId = model.DoctorId,
                HospitalizationDate = model.HospitalizationDate,
                HospitalizationTime = model.HospitalizationTime,
                DischargeDate = model.DischargeDate,
                DischargeTime = model.DischargeTime,
                RoomId = model.RoomId,
                BirthCity = model.BirthCity,
                DateOfBirth = model.DateOfBirth,
                PhoneNumber = model.PhoneNumber,
                UCN = model.UCN
            };

            await context.Patients.AddAsync(patient);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PatientEditDTO model)
        {
            var patient = await context.Patients
              .Include(d => d.User)
              .FirstOrDefaultAsync(d => d.ID == model.ID);

            if (patient == null)
            {
                return;
            }

            var names = model.PatientName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            patient.User.FirstName = names.Length > 0 ? names[0] : "";
            patient.User.LastName = names.Length > 1 ? names[1] : "";


            patient.DoctorId = model.DoctorId;
            patient.HospitalizationDate = model.HospitalizationDate;
            patient.HospitalizationTime = model.HospitalizationTime;
            patient.DischargeDate = model.DischargeDate;
            patient.DischargeTime = model.DischargeTime;
            patient.RoomId = model.RoomId;
            patient.BirthCity = model.BirthCity;
            patient.DateOfBirth = model.DateOfBirth;
            patient.PhoneNumber = model.PhoneNumber;
            patient.UCN = model.UCN;

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var patient = await context.Patients.FindAsync(id);
            if (patient == null)
            {
                throw new InvalidOperationException("Couldn't remove the patient.");
            }

            context.Patients.Remove(patient);
            await context.SaveChangesAsync();
        }
 
        public async Task<List<PatientIndexDTO>> PatientsWithSuchDoctor(string doctorName)
        {
            if (string.IsNullOrWhiteSpace(doctorName))
            {
                return new List<PatientIndexDTO>();
            }

            string pattern = $"%{doctorName}%";

            var patients = await context.Patients
                .Include(p => p.Doctor)
                    .ThenInclude(d => d.User)
                .Include(p => p.Room)
                .Include(p => p.User)
                .Where(x => x.Doctor != null &&
                            x.Doctor.User != null &&
                            EF.Functions.Like(x.Doctor.User.FirstName, pattern))
                .ToListAsync();


            return patients.Select(x => new PatientIndexDTO
            {
                ID = x.ID,
                DoctorId = x.DoctorId,
                DoctorName = x.Doctor?.User != null
                    ? x.Doctor.User.FirstName + " " + x.Doctor.User.LastName
                    : "No data",
                HospitalizationDate = x.HospitalizationDate,
                HospitalizationTime = x.HospitalizationTime,
                DischargeDate = x.DischargeDate,
                DischargeTime = x.DischargeTime,
                UserID = x.UserId,
                UserName = x.User != null
                    ? x.User.FirstName + " " + x.User.LastName
                    : "No data",
                RoomId = x.RoomId,
                RoomNumber = x.Room?.RoomNumber ?? 0,
                BirthCity = x.BirthCity,
                DateOfBirth = x.DateOfBirth,
                PhoneNumber = x.PhoneNumber,
                UCN = x.UCN
            }).ToList();
        }

        public async Task<PatientIndexDTO> Details(Guid id)
        {
            var patient = await context.Patients
                .Include(p => p.User)
                .Include(p => p.Doctor)
                .ThenInclude(d => d.User)
                .Include(p => p.Room)
                .FirstOrDefaultAsync(p => p.ID == id);

            if (patient == null)
            {
                throw new InvalidOperationException($"Patient was not found.");
            }

            return new PatientIndexDTO
            {
                ID = patient.ID,
                DoctorId = patient.DoctorId,
                DoctorName = patient.Doctor?.User != null
                    ? $"{patient.Doctor.User.FirstName} {patient.Doctor.User.LastName}"
                    : "No Doctor Assigned",
                HospitalizationDate = patient.HospitalizationDate,
                HospitalizationTime = patient.HospitalizationTime,
                DischargeDate = patient.DischargeDate,
                DischargeTime = patient.DischargeTime,
                UserID = patient.UserId,
                UserName = patient.User != null
                    ? $"{patient.User.FirstName} {patient.User.LastName}"
                    : "Unknown User",
                RoomId = patient.RoomId,
                RoomNumber = patient.Room?.RoomNumber ?? 0,
                BirthCity = patient.BirthCity,
                DateOfBirth = patient.DateOfBirth,
                PhoneNumber = patient.PhoneNumber,
                UCN = patient.UCN
            };
        }
    }
}
