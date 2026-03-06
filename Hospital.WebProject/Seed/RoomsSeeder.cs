using Hospital.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.WebProject.Seed
{
    public class RoomsSeeder : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasData(
                new Room { ID = Guid.NewGuid(), RoomNumber = 101, IsTaken = false },
                new Room { ID = Guid.NewGuid(), RoomNumber = 102, IsTaken = false },
                new Room { ID = Guid.NewGuid(), RoomNumber = 103, IsTaken = false },
                new Room { ID = Guid.NewGuid(), RoomNumber = 104, IsTaken = false },
                new Room { ID = Guid.NewGuid(), RoomNumber = 105, IsTaken = false },
                new Room { ID = Guid.NewGuid(), RoomNumber = 106, IsTaken = false },
                new Room { ID = Guid.NewGuid(), RoomNumber = 107, IsTaken = false },
                new Room { ID = Guid.NewGuid(), RoomNumber = 108, IsTaken = false },
                new Room { ID = Guid.NewGuid(), RoomNumber = 109, IsTaken = false },
                new Room { ID = Guid.NewGuid(), RoomNumber = 201, IsTaken = false },
                new Room { ID = Guid.NewGuid(), RoomNumber = 202, IsTaken = false },
                new Room { ID = Guid.NewGuid(), RoomNumber = 203, IsTaken = false },
                new Room { ID = Guid.NewGuid(), RoomNumber = 204, IsTaken = false },
                new Room { ID = Guid.NewGuid(), RoomNumber = 205, IsTaken = false },
                new Room { ID = Guid.NewGuid(), RoomNumber = 206, IsTaken = false },
                new Room { ID = Guid.NewGuid(), RoomNumber = 207, IsTaken = false },
                new Room { ID = Guid.NewGuid(), RoomNumber = 208, IsTaken = false },
                new Room { ID = Guid.NewGuid(), RoomNumber = 209, IsTaken = false },
                new Room { ID = Guid.NewGuid(), RoomNumber = 301, IsTaken = false }
            );
        }
    
    }
}
