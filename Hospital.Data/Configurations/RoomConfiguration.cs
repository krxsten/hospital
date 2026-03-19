using Hospital.Data.Entities;
using Hospital.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.WebProject.Seed
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasData(CreateRooms());
        }
        public List<Room> CreateRooms()
        {
            List<Room> rooms = new List<Room>()
            {
                new Room { ID = new Guid("5b8e402c-ec9e-4e74-9cf2-ed1c05948e2a"), RoomNumber = 101, IsTaken = false },
                new Room { ID = new Guid("bb9bb784-dd42-4541-9ed2-01e7929a61ab"), RoomNumber = 102, IsTaken = true },
                new Room { ID = new Guid("1ba934d0-494e-431a-a7d6-2fff62af342a"), RoomNumber = 103, IsTaken = false },
                new Room { ID = new Guid("e748053a-f20a-4f80-a314-fbd9c2ac4e8a"), RoomNumber = 104, IsTaken = true },
                new Room { ID = new Guid("3970dc21-5df6-42ac-95db-261e18812d4c"), RoomNumber = 105, IsTaken = true },
                new Room { ID = new Guid("0de4b3b8-318e-42aa-b896-04cd14749d17"), RoomNumber = 106, IsTaken = false },
                new Room { ID = new Guid("92aa881b-d346-49d4-8b3d-a76e13469e99"), RoomNumber = 107, IsTaken = true },
                new Room { ID = new Guid("c4ad2bf7-9eea-4555-b637-00bc1d11cba3"), RoomNumber = 108, IsTaken = true },
                new Room { ID = new Guid("097fe7eb-86e3-43ea-a6da-c8a90373f27a"), RoomNumber = 109, IsTaken = false },
                new Room { ID = new Guid("9e9a26f4-3d2e-4f9a-bcf1-9e63e057a7c6"), RoomNumber = 201, IsTaken = true },
                new Room { ID = new Guid("c219c792-8a69-4850-ac40-a36f5f752786"), RoomNumber = 202, IsTaken = true },
                new Room { ID = new Guid("b5825cde-c8df-4d53-8f69-6d4e6d2683cb"), RoomNumber = 203, IsTaken = false },
                new Room { ID = new Guid("bee36b54-ff99-474a-b9de-82010ff3607c"), RoomNumber = 204, IsTaken = true },
                new Room { ID = new Guid("ba0b5ec2-b3c0-4901-8128-a3703d72ffd3"), RoomNumber = 205, IsTaken = true },
                new Room { ID = new Guid("f2a57cc6-8969-4f63-9691-2c449409ff4d"), RoomNumber = 206, IsTaken = true },
                new Room { ID = new Guid("85242158-d3c2-4542-bc27-88caf4a131c6"), RoomNumber = 207, IsTaken = false },
                new Room { ID = new Guid("44a7a821-54c8-4d2c-acf3-efbfbd75c18e"), RoomNumber = 208, IsTaken = true },
                new Room { ID = new Guid("88b657b6-c9c3-41eb-a26f-5d637c704533"), RoomNumber = 209, IsTaken = false },
                new Room { ID = new Guid("53546e3d-53ee-4e2c-861a-3cf5a3584893"), RoomNumber = 301, IsTaken = true }
            };
            return rooms;
        }
    }

}
