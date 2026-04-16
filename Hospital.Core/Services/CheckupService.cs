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
    public class CheckupService : ICheckupService
    {
        private readonly HospitalDbContext context;

        public CheckupService(HospitalDbContext context)
        {
            this.context = context;
        }

        public async Task<List<CheckupIndexDTO>> GetAllAsync()
        {
            return await context.Checkups
                .Select(x => new CheckupIndexDTO
                {
                    ID = x.ID,
                    Date = x.Date,
                    Time = x.Time,
                    DoctorID = x.DoctorID,
                    DoctorName = x.Doctor.User.FirstName + " " + x.Doctor.User.LastName,
                    PatientID = x.PatientID,
                    PatientName = x.Patient.User.FirstName + " " + x.Patient.User.LastName
                })
                 .OrderBy(x => x.Date)
                .ThenBy(x=>x.PatientName)
                .ToListAsync();
        }
        public async Task<List<CheckupIndexDTO>> GetPatientAppointmentAsync(Guid patientID)
        {
            return await context.Checkups
                .Where(x=>x.PatientID==patientID)
                .Select(x => new CheckupIndexDTO
                {
                    ID = x.ID,
                    Date = x.Date,
                    Time = x.Time,
                    DoctorID = x.DoctorID,
                    DoctorName = x.Doctor.User.FirstName + " " + x.Doctor.User.LastName,
                    PatientID = x.PatientID,
                    PatientName = x.Patient.User.FirstName + " " + x.Patient.User.LastName
                })
                .ToListAsync();
        }

        public async Task<CheckupIndexDTO?> GetByIdAsync(Guid id)
        {
            return await context.Checkups
                .Where(x => x.ID == id)
                .Select(x => new CheckupIndexDTO
                {
                    ID = x.ID,
                    Date = x.Date,
                    Time = x.Time,
                    DoctorID = x.DoctorID,
                    DoctorName = x.Doctor.User.FirstName + " " + x.Doctor.User.LastName,
                    PatientID = x.PatientID,
                    PatientName = x.Patient.User.FirstName + " " + x.Patient.User.LastName
                })
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(CheckupCreateDTO model)
        {
            var checkup = new Checkup
            {
                ID = Guid.NewGuid(),
                Date = model.Date,
                Time = model.Time,
                DoctorID = model.DoctorID,
                PatientID = model.PatientID
            };

            await context.Checkups.AddAsync(checkup);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CheckupEditDTO model)
        {
            var checkup = await context.Checkups.FindAsync(model.ID);
            if (checkup == null)
            {
                return;
            }

            checkup.Date = model.Date;
            checkup.Time = model.Time;
            checkup.DoctorID = model.DoctorID;
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var checkup = await context.Checkups.FindAsync(id);
            if (checkup == null)
            {
                return;
            }

            context.Checkups.Remove(checkup);
            await context.SaveChangesAsync();
        }
        public async Task<List<DateOnly>> GetBusyTimes(Guid doctorId, DateTime date)
        {
            var busy = await context.Checkups
                .Where(c => c.DoctorID == doctorId &&
                            c.Date.Day == date.Day &&
                            c.Date.Month == date.Month &&
                            c.Date.Year == date.Year)
                .Select(c => c.Date)
                .ToListAsync();

            return busy;
        }

        public async Task<DoctorShiftDTO?> GetDoctorShift(Guid doctorId)
        {
            var shift = await context.Doctors
                .Include(d => d.Shift)
                .Where(d => d.ID == doctorId)
                .Select(d => new DoctorShiftDTO
                {
                    StartTime = d.Shift.StartTime,
                    EndTime = d.Shift.EndTime
                })
                .FirstOrDefaultAsync();

            return shift;
        }

        public async Task<List<CheckupIndexDTO>> GetCheckupsDate(DateOnly date)
        {
            return await context.Checkups.Where(x => x.Date == date)
                .Select(x => new CheckupIndexDTO
                {
                    ID = x.ID,
                    Date = x.Date,
                    Time = x.Time,
                    DoctorID = x.DoctorID,
                    DoctorName = x.Doctor.User.FirstName + " " + x.Doctor.User.LastName,
                    PatientID = x.PatientID,
                    PatientName = x.Patient.User.FirstName + " " + x.Patient.User.LastName
                })
                .ToListAsync();
        }
        public async Task<List<TimeOnly>> GetAvailableTimeSlotsAsync(Guid doctorId, DateOnly date)
        {
            var doctor = await context.Doctors
                .Include(d => d.Shift)
                .FirstOrDefaultAsync(d => d.ID == doctorId);

            if (doctor == null || doctor.Shift == null)
            {
                return new List<TimeOnly>();
            }

            var bookedTimes = await context.Checkups
                .Where(c => c.DoctorID == doctorId && c.Date == date)
                .Select(c => c.Time)
                .ToListAsync();

            var availableSlots = new List<TimeOnly>();

            var start = doctor.Shift.StartTime;
            var end = doctor.Shift.EndTime;

            for (var time = start; time < end; time = time.AddMinutes(30))
            {
                if (!bookedTimes.Contains(time))
                {
                    availableSlots.Add(time);
                }
            }

            return availableSlots;
        }
    }
}
