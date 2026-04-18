using Hospital.Core.Services;
using Hospital.Data.Entities;
using Hospital.Entities;
using Hospital.Tests.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalNUnitTestProject
{

    [TestFixture]
    public class StaffServicesTests : TestHelpers
    {
        [Test]
        public async Task AdminService_ApproveDoctor_WorksWithGuid()
        {
           // var service = new AdminService(Context, Mapper);
            Guid doctorId = Guid.NewGuid(); 

            var doctor = new Doctor
            {
                ID = doctorId,
                IsAccepted = false,
                User = new User { FirstName = "Test", LastName = "Test" },
                SpecializationId = Guid.NewGuid(),
                ShiftId = Guid.NewGuid(),
                
            };

            await Context.Doctors.AddAsync(doctor);
            await Context.SaveChangesAsync();

           // await service.AcceptDoctorAsync(doctorId);

            var approvedDoctor = await Context.Doctors.FindAsync(doctorId);
            Assert.That(approvedDoctor.IsAccepted, "Doctor should be accepted.");
        }

        [Test]
        public async Task DoctorService_AllDoctors_ReturnsMappedModels()
        {
            //UserManager<User> userManager = new UserManager<User>(
            //    new UserStore<User>(Context),
            //    null, null, null, null, null, null, null, null);
           // ImageService imageService = new ImageService();

          //  var service = new DoctorService(Context,imageService,userManager);
            await Context.Doctors.AddAsync(new Doctor
            {
                ID = Guid.NewGuid(),
                User = new User { FirstName = "Ivan", LastName = "Ivanov" },
                SpecializationId = Guid.NewGuid(),
                ShiftId = Guid.NewGuid()
            });
            await Context.SaveChangesAsync();

           // var result = await service.SortByFirstName();
           // Assert.That(result.Count(), Is.EqualTo(1));
        }
    }
}
