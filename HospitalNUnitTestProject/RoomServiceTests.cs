using Hospital.Core.DTOs;
using Hospital.Core.Services;
using Hospital.Data;
using Hospital.Entities;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Hospital.Tests.Services
{
    [TestFixture]
    public class RoomServiceTests
    {
        private HospitalDbContext context;
        private RoomService service;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<HospitalDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            context = new HospitalDbContext(options);
            service = new RoomService(context);
        }

        [TearDown]
        public void TearDown()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        private async Task<Room> SeedRoomAsync(int roomNumber = 101, bool isTaken = false)
        {
            var room = new Room
            {
                ID = Guid.NewGuid(),
                RoomNumber = roomNumber,
                IsTaken = isTaken
            };

            context.Rooms.Add(room);
            await context.SaveChangesAsync();

            return room;
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllRoomsOrderedByRoomNumber()
        {
            await SeedRoomAsync(203, true);
            await SeedRoomAsync(101, false);
            await SeedRoomAsync(150, true);

            var result = await service.GetAllAsync();

            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result[0].RoomNumber, Is.EqualTo(101));
            Assert.That(result[1].RoomNumber, Is.EqualTo(150));
            Assert.That(result[2].RoomNumber, Is.EqualTo(203));
        }

        [Test]
        public async Task GetByIdAsync_WhenRoomExists_ReturnsRoom()
        {
            var room = await SeedRoomAsync(101, true);

            var result = await service.GetByIdAsync(room.ID);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.ID, Is.EqualTo(room.ID));
            Assert.That(result.RoomNumber, Is.EqualTo(101));
            Assert.That(result.IsTaken, Is.True);
        }

        [Test]
        public async Task GetByIdAsync_WhenRoomMissing_ReturnsNull()
        {
            var result = await service.GetByIdAsync(Guid.NewGuid());

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task CreateAsync_AddsRoomToDatabase()
        {
            var model = new RoomCreateDTO
            {
                RoomNumber = 205,
                IsTaken = true
            };

            await service.CreateAsync(model);

            var room = await context.Rooms.FirstOrDefaultAsync();

            Assert.That(room, Is.Not.Null);
            Assert.That(room!.RoomNumber, Is.EqualTo(205));
            Assert.That(room.IsTaken, Is.True);
        }

        [Test]
        public async Task UpdateAsync_WhenRoomExists_UpdatesRoom()
        {
            var room = await SeedRoomAsync(101, false);

            var model = new RoomIndexDTO
            {
                ID = room.ID,
                RoomNumber = 301,
                IsTaken = true
            };

            await service.UpdateAsync(model);

            var updated = await context.Rooms.FindAsync(room.ID);

            Assert.That(updated, Is.Not.Null);
            Assert.That(updated!.RoomNumber, Is.EqualTo(301));
            Assert.That(updated.IsTaken, Is.True);
        }

        [Test]
        public async Task UpdateAsync_WhenRoomMissing_DoesNothing()
        {
            var model = new RoomIndexDTO
            {
                ID = Guid.NewGuid(),
                RoomNumber = 500,
                IsTaken = true
            };

            await service.UpdateAsync(model);

            Assert.That(await context.Rooms.CountAsync(), Is.EqualTo(0));
        }

        [Test]
        public async Task DeleteAsync_WhenRoomExists_RemovesRoom()
        {
            var room = await SeedRoomAsync(101, false);

            await service.DeleteAsync(room.ID);

            var deleted = await context.Rooms.FindAsync(room.ID);
            Assert.That(deleted, Is.Null);
        }

        [Test]
        public async Task DeleteAsync_WhenRoomMissing_DoesNothing()
        {
            await service.DeleteAsync(Guid.NewGuid());

            Assert.That(await context.Rooms.CountAsync(), Is.EqualTo(0));
        }

        [Test]
        public async Task GetRoomsAfterNum_ReturnsRoomsWithNumberGreaterThanOrEqualToInput()
        {
            await SeedRoomAsync(101, false);
            await SeedRoomAsync(150, true);
            await SeedRoomAsync(203, false);

            var result = await service.GetRoomsAfterNum(150);

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].RoomNumber, Is.EqualTo(150));
            Assert.That(result[1].RoomNumber, Is.EqualTo(203));
        }

        [Test]
        public async Task GetRoomsAfterNum_WhenNoRoomsMatch_ReturnsEmptyList()
        {
            await SeedRoomAsync(101, false);
            await SeedRoomAsync(150, true);

            var result = await service.GetRoomsAfterNum(300);

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task GetRoomsAfterNum_WhenInputMatchesAll_ReturnsAllOrderedRooms()
        {
            await SeedRoomAsync(101, false);
            await SeedRoomAsync(150, true);
            await SeedRoomAsync(203, false);

            var result = await service.GetRoomsAfterNum(100);

            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result[0].RoomNumber, Is.EqualTo(101));
            Assert.That(result[1].RoomNumber, Is.EqualTo(150));
            Assert.That(result[2].RoomNumber, Is.EqualTo(203));
        }
    }
}