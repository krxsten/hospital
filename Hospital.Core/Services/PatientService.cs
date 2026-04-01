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
            return await context.Patients
                .Select(x => new PatientIndexDTO
                {
                    ID = x.ID,
                    DoctorId = x.DoctorId,
                    DoctorName = x.Doctor.User.FirstName + " " + x.Doctor.User.LastName,
                    HospitalizationDate = x.HospitalizationDate,
                    HospitalizationTime = x.HospitalizationTime,
                    DischargeDate = x.DischargeDate,
                    DischargeTime = x.DischargeTime,
                    UserID = x.UserId,
                    UserName = x.User.FirstName + " " + x.User.LastName,
                    RoomId = x.RoomId,
                    RoomNumber = x.Room.RoomNumber,
                    BirthCity = x.BirthCity,
                    DateOfBirth = x.DateOfBirth,
                    PhoneNumber = x.PhoneNumber,
                    UCN = x.UCN
                })
                .ToListAsync();
        }

        public async Task<PatientIndexDTO?> GetByIdAsync(Guid id)
        {
            return await context.Patients
                .Where(x => x.ID == id)
                .Select(x => new PatientIndexDTO
                {
                    ID = x.ID,
                    DoctorId = x.DoctorId,
                    DoctorName = x.Doctor.User.FirstName + " " + x.Doctor.User.LastName,
                    HospitalizationDate = x.HospitalizationDate,
                    HospitalizationTime = x.HospitalizationTime,
                    DischargeDate = x.DischargeDate,
                    DischargeTime = x.DischargeTime,
                    UserID = x.UserId,
                    UserName = x.User.FirstName + " " + x.User.LastName,
                    RoomId = x.RoomId,
                    RoomNumber = x.Room.RoomNumber,
                    BirthCity = x.BirthCity,
                    DateOfBirth = x.DateOfBirth,
                    PhoneNumber = x.PhoneNumber,
                    UCN = x.UCN
                })
                .FirstOrDefaultAsync();
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
        
    }
}
