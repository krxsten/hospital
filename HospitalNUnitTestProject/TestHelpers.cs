using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Hospital.Tests.Helpers
{
    [TestFixture]
    public abstract class TestHelpers
    {
        protected HospitalDbContext Context;
        //protected IMapper Mapper;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<HospitalDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            Context = new HospitalDbContext(options);
            Context.Database.EnsureCreated();

          //  var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
          //  Mapper = config.CreateMapper();
        }

        [TearDown]
        public void TearDown()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}