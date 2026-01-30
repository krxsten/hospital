using Hospital.Data.Entities;
using Hospital.Entities;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.User)
                .WithOne(u => u.Doctor)
                .HasForeignKey<Doctor>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Nurse>()
                .HasOne(n => n.User)
                .WithOne(u => u.Nurse)
                .HasForeignKey<Nurse>(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Patient>()
                .HasOne(p => p.User)
                .WithOne(u => u.Patient)
                .HasForeignKey<Patient>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Patient>()
                .HasOne(p => p.Doctor)
                .WithMany(d => d.Patients)
                .HasForeignKey(p => p.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Patient>()
                .HasOne(p => p.Room)
                .WithMany(r => r.ListOfPatients)
                .HasForeignKey(p => p.RoomId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<DoctorAndNurse>()
                .HasOne(x => x.Doctor)
                .WithMany(d => d.DoctorNurses)
                .HasForeignKey(x => x.DoctorID);

            modelBuilder.Entity<DoctorAndNurse>()
                .HasOne(x => x.Nurse)
                .WithMany(n => n.DoctorNurses)
                .HasForeignKey(x => x.NurseID);


            modelBuilder.Entity<PatientAndDiagnose>()
                .HasOne(x => x.Patient)
                .WithMany(p => p.PatientDiagnoses)
                .HasForeignKey(x => x.PatientId);

            modelBuilder.Entity<PatientAndDiagnose>()
                .HasOne(x => x.Diagnose)
                .WithMany(d => d.ListOfPatientsAndDiagnoses)
                .HasForeignKey(x => x.DiagnoseId);


            modelBuilder.Entity<DoctorAndNurse>()
                .HasIndex(x => new { x.DoctorID, x.NurseID })
                .IsUnique();

            modelBuilder.Entity<PatientAndDiagnose>()
                .HasIndex(x => new { x.PatientId, x.DiagnoseId })
                .IsUnique();
        }

    }
}