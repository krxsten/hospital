using Hospital.Core.Contracts;
using Hospital.Core.DTOs;
using Hospital.Data;
using Hospital.Data.Entities;
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

        public PatientService(HospitalDbContext context)
        {
            this.context = context;
        }

        public async Task<List<PatientIndexDTO>> GetAllAsync()
        {
            var patients = await context.Patients
                .Include(p => p.Doctor.User)
                .Include(p => p.Room)
                .Include(p => p.User)
                .ToListAsync();

            var cityIdsAsGuids = patients
                .Select(p => p.BirthCity)
                .Where(bc => Guid.TryParse(bc, out _))
                .Select(bc => Guid.Parse(bc))
                .Distinct()
                .ToList();

            var cityNames = await context.Cities
                .Where(c => cityIdsAsGuids.Contains(c.Id))
                .ToDictionaryAsync(c => c.Id.ToString(), c => c.Name);

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
                BirthCity = cityNames.ContainsKey(x.BirthCity) ? cityNames[x.BirthCity] : x.BirthCity,
                DateOfBirth = x.DateOfBirth,
                PhoneNumber = x.PhoneNumber,
                UCN = x.UCN
            }).ToList();
        }

        public async Task<PatientIndexDTO?> GetByIdAsync(Guid id)
        {
            var x = await context.Patients
                .Include(p => p.Doctor.User)
                .Include(p => p.Room)
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.ID == id);

            if (x == null) return null;

            var birthCity = x.BirthCity;
            if (Guid.TryParse(x.BirthCity, out var cityId))
            {
                var city = await context.Cities.FindAsync(cityId);
                if (city != null) birthCity = city.Name;
            }

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
                BirthCity = birthCity,
                DateOfBirth = x.DateOfBirth,
                PhoneNumber = x.PhoneNumber,
                UCN = x.UCN
            };
        }

        public async Task CreateAsync(PatientCreateDTO model)
        {
            var patient = new Patient
            {
                ID = Guid.NewGuid(),
                UserId = model.UserID,
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

        public async Task UpdateAsync(PatientIndexDTO model)
        {
            var patient = await context.Patients.FindAsync(model.ID);
            if (patient == null)
            {
                return;
            }

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
                return;
            }

            context.Patients.Remove(patient);
            await context.SaveChangesAsync();
        }
        public async Task SelectDoctorAndRoomAsync(Guid userId, Guid doctorId, Guid roomId,
            string birthCity, DateOnly dateOfBirth, string phoneNumber, string ucn)
        {
            var room = await context.Rooms.FindAsync(roomId);
            if (room == null || room.IsTaken)
            {
                throw new InvalidOperationException("The selected room is no longer available.");
            }

            var birthCityName = birthCity;
            if (Guid.TryParse(birthCity, out var cityId))
            {
                var city = await context.Cities.FindAsync(cityId);
                if (city != null) birthCityName = city.Name;
            }

            var patient = new Patient
            {
                ID = Guid.NewGuid(),
                UserId = userId,
                DoctorId = doctorId,
                RoomId = roomId,
                HospitalizationDate = DateOnly.FromDateTime(DateTime.Now),
                HospitalizationTime = TimeOnly.FromDateTime(DateTime.Now),
                DischargeDate = DateOnly.FromDateTime(DateTime.Now.AddDays(7)),
                DischargeTime = TimeOnly.FromDateTime(DateTime.Now),
                BirthCity = birthCityName,
                DateOfBirth = dateOfBirth,
                PhoneNumber = phoneNumber,
                UCN = ucn
            };

            room.IsTaken = true;

            await context.Patients.AddAsync(patient);
            await context.SaveChangesAsync();
        }
        public async Task<List<PatientIndexDTO>> PatientsWithSuchDoctor(string doctroName)
        {
            var patients = await context.Patients
              .Include(p => p.Doctor.User)
              .Include(p => p.Room)
              .Include(p => p.User)
              .ToListAsync();

            var cityIdsAsGuids = patients
                .Select(p => p.BirthCity)
                .Where(bc => Guid.TryParse(bc, out _))
                .Select(bc => Guid.Parse(bc))
                .Distinct()
                .ToList();

            var cityNames = await context.Cities
                .Where(c => cityIdsAsGuids.Contains(c.Id))
                .ToDictionaryAsync(c => c.Id.ToString(), c => c.Name);

            return patients.Where(x=>x.Doctor.User.FirstName==doctroName).Select(x => new PatientIndexDTO
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
                BirthCity = cityNames.ContainsKey(x.BirthCity) ? cityNames[x.BirthCity] : x.BirthCity,
                DateOfBirth = x.DateOfBirth,
                PhoneNumber = x.PhoneNumber,
                UCN = x.UCN
            }).ToList();
        }
        
    }
}
