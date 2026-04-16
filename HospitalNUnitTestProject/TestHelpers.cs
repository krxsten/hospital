using Hospital.Data.Entities;
using Hospital.Entities;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Hospital.Tests.Helpers
{
    public static class TestHelpers
    {
        public static Mock<UserManager<User>> CreateUserManagerMock()
        {
            var store = new Mock<IUserStore<User>>();

            return new Mock<UserManager<User>>(
                store.Object,
                null!,
                null!,
                null!,
                null!,
                null!,
                null!,
                null!,
                null!);
        }
    }
}