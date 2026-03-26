using Hospital.Data.Configurations;
using Hospital.Data.Entities;
using Hospital.Entities;
using Hospital.WebProject.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Data
{
    public class HospitalDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options)
        {
        }
        public DbSet<Diagnose> Diagnoses { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorAndNurse> DoctorsAndNurses { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<Nurse> Nurses { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientAndDiagnose> PatientsAndDiagnoses { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Checkup> Checkups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(w =>
         w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new SpecializationConfiguration());
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
            modelBuilder.ApplyConfiguration(new ShiftConfiguration());
            modelBuilder.ApplyConfiguration(new MedicationConfiguration());
            modelBuilder.ApplyConfiguration(new DiagnoseConfiguration());
            modelBuilder.ApplyConfiguration(new DoctorConfiguration());
            modelBuilder.ApplyConfiguration(new NurseConfiguration());
            modelBuilder.ApplyConfiguration(new DoctorAndNurseConfiguration());
            modelBuilder.ApplyConfiguration(new PatientConfiguration());
            modelBuilder.ApplyConfiguration(new CheckupConfiguration());
            modelBuilder.ApplyConfiguration(new PatientDiagnoseConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}