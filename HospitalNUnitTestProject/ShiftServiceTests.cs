using Hospital.Core.DTOs;
using Hospital.Core.Services;
using Hospital.Data;
using Hospital.Data.Entities;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Hospital.Tests.Services
{
    [TestFixture]
    public class ShiftServiceTests
    {
        private HospitalDbContext context;
        private ShiftService service;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<HospitalDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            context = new HospitalDbContext(options);
            service = new ShiftService(context);
        }

        [TearDown]
        public void TearDown()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        private async Task<Shift> SeedShiftAsync(
            string type = "Morning",
            int startHour = 8,
            int startMinute = 0,
            int endHour = 16,
            int endMinute = 0)
        {
            var shift = new Shift
            {
                ID = Guid.NewGuid(),
                Type = type,
                StartTime = new TimeOnly(startHour, startMinute),
                EndTime = new TimeOnly(endHour, endMinute)
            };

            context.Shifts.Add(shift);
            await context.SaveChangesAsync();

            return shift;
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllShifts()
        {
            await SeedShiftAsync("Morning", 8, 0, 16, 0);
            await SeedShiftAsync("Night", 20, 0, 6, 0);

            var result = await service.GetAllAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Any(x => x.Type == "Morning"), Is.True);
            Assert.That(result.Any(x => x.Type == "Night"), Is.True);
        }

        [Test]
        public async Task GetByIdAsync_WhenShiftExists_ReturnsShift()
        {
            var shift = await SeedShiftAsync("Morning", 8, 0, 16, 0);

            var result = await service.GetByIdAsync(shift.ID);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.ID, Is.EqualTo(shift.ID));
            Assert.That(result.Type, Is.EqualTo("Morning"));
            Assert.That(result.StartTime, Is.EqualTo(new TimeOnly(8, 0)));
            Assert.That(result.EndTime, Is.EqualTo(new TimeOnly(16, 0)));
        }

        [Test]
        public async Task GetByIdAsync_WhenShiftMissing_ReturnsNull()
        {
            var result = await service.GetByIdAsync(Guid.NewGuid());

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task CreateAsync_WhenTimeRangeIsValid_AddsShift()
        {
            var model = new ShiftCreateDTO
            {
                Type = "Afternoon",
                StartTime = new TimeOnly(12, 0),
                EndTime = new TimeOnly(20, 0)
            };

            await service.CreateAsync(model);

            var shift = await context.Shifts.FirstOrDefaultAsync();

            Assert.That(shift, Is.Not.Null);
            Assert.That(shift!.Type, Is.EqualTo("Afternoon"));
            Assert.That(shift.StartTime, Is.EqualTo(new TimeOnly(12, 0)));
            Assert.That(shift.EndTime, Is.EqualTo(new TimeOnly(20, 0)));
        }

        [Test]
        public async Task CreateAsync_WhenEndTimeIsBeforeOrEqualToStartTime_DoesNothing()
        {
            var model = new ShiftCreateDTO
            {
                Type = "Invalid",
                StartTime = new TimeOnly(16, 0),
                EndTime = new TimeOnly(8, 0)
            };

            await service.CreateAsync(model);

            Assert.That(await context.Shifts.CountAsync(), Is.EqualTo(0));
        }

        [Test]
        public async Task CreateAsync_WhenEndTimeEqualsStartTime_DoesNothing()
        {
            var model = new ShiftCreateDTO
            {
                Type = "Invalid",
                StartTime = new TimeOnly(8, 0),
                EndTime = new TimeOnly(8, 0)
            };

            await service.CreateAsync(model);

            Assert.That(await context.Shifts.CountAsync(), Is.EqualTo(0));
        }

        [Test]
        public async Task UpdateAsync_WhenShiftExistsAndTimeRangeIsValid_UpdatesShift()
        {
            var shift = await SeedShiftAsync("Morning", 8, 0, 16, 0);

            var model = new ShiftIndexDTO
            {
                ID = shift.ID,
                Type = "Updated Shift",
                StartTime = new TimeOnly(9, 0),
                EndTime = new TimeOnly(17, 0)
            };

            await service.UpdateAsync(model);

            var updated = await context.Shifts.FindAsync(shift.ID);

            Assert.That(updated, Is.Not.Null);
            Assert.That(updated!.Type, Is.EqualTo("Updated Shift"));
            Assert.That(updated.StartTime, Is.EqualTo(new TimeOnly(9, 0)));
            Assert.That(updated.EndTime, Is.EqualTo(new TimeOnly(17, 0)));
        }

        [Test]
        public async Task UpdateAsync_WhenShiftMissing_DoesNothing()
        {
            var model = new ShiftIndexDTO
            {
                ID = Guid.NewGuid(),
                Type = "Missing",
                StartTime = new TimeOnly(9, 0),
                EndTime = new TimeOnly(17, 0)
            };

            await service.UpdateAsync(model);

            Assert.That(await context.Shifts.CountAsync(), Is.EqualTo(0));
        }

        [Test]
        public async Task UpdateAsync_WhenEndTimeIsBeforeOrEqualToStartTime_DoesNothing()
        {
            var shift = await SeedShiftAsync("Morning", 8, 0, 16, 0);

            var model = new ShiftIndexDTO
            {
                ID = shift.ID,
                Type = "Invalid Update",
                StartTime = new TimeOnly(18, 0),
                EndTime = new TimeOnly(10, 0)
            };

            await service.UpdateAsync(model);

            var unchanged = await context.Shifts.FindAsync(shift.ID);

            Assert.That(unchanged, Is.Not.Null);
            Assert.That(unchanged!.Type, Is.EqualTo("Morning"));
            Assert.That(unchanged.StartTime, Is.EqualTo(new TimeOnly(8, 0)));
            Assert.That(unchanged.EndTime, Is.EqualTo(new TimeOnly(16, 0)));
        }

        [Test]
        public async Task DeleteAsync_WhenShiftExists_RemovesShift()
        {
            var shift = await SeedShiftAsync();

            await service.DeleteAsync(shift.ID);

            var deleted = await context.Shifts.FindAsync(shift.ID);
            Assert.That(deleted, Is.Null);
        }

        [Test]
        public async Task DeleteAsync_WhenShiftMissing_DoesNothing()
        {
            await service.DeleteAsync(Guid.NewGuid());

            Assert.That(await context.Shifts.CountAsync(), Is.EqualTo(0));
        }

        [Test]
        public async Task GetShiftByTime_WhenTimeIsNull_ReturnsEmptyList()
        {
            var result = await service.GetShiftByTime(null);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task GetShiftByTime_WhenTimeMatchesNormalShift_ReturnsShift()
        {
            await SeedShiftAsync("Morning", 8, 0, 16, 0);
            await SeedShiftAsync("Afternoon", 16, 30, 23, 0);

            var result = await service.GetShiftByTime(new TimeOnly(10, 0));

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Type, Is.EqualTo("Morning"));
        }

        [Test]
        public async Task GetShiftByTime_WhenTimeMatchesNightShiftAcrossMidnight_ReturnsShift()
        {
            await SeedShiftAsync("Night", 20, 0, 6, 0);

            var resultLate = await service.GetShiftByTime(new TimeOnly(22, 0));
            var resultEarly = await service.GetShiftByTime(new TimeOnly(3, 0));

            Assert.That(resultLate.Count, Is.EqualTo(1));
            Assert.That(resultLate[0].Type, Is.EqualTo("Night"));

            Assert.That(resultEarly.Count, Is.EqualTo(1));
            Assert.That(resultEarly[0].Type, Is.EqualTo("Night"));
        }

        [Test]
        public async Task GetShiftByTime_WhenTimeDoesNotMatchAnyShift_ReturnsEmptyList()
        {
            await SeedShiftAsync("Morning", 8, 0, 16, 0);

            var result = await service.GetShiftByTime(new TimeOnly(2, 0));

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task GetShiftByTime_WhenTimeIsOnBoundary_ReturnsMatchingShift()
        {
            await SeedShiftAsync("Morning", 8, 0, 16, 0);

            var resultAtStart = await service.GetShiftByTime(new TimeOnly(8, 0));
            var resultAtEnd = await service.GetShiftByTime(new TimeOnly(16, 0));

            Assert.That(resultAtStart.Count, Is.EqualTo(1));
            Assert.That(resultAtStart[0].Type, Is.EqualTo("Morning"));

            Assert.That(resultAtEnd.Count, Is.EqualTo(1));
            Assert.That(resultAtEnd[0].Type, Is.EqualTo("Morning"));
        }
    }
}