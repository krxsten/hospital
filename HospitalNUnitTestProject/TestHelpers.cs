using Microsoft.AspNetCore.Identity;
using Moq;

namespace Hospital.Tests.Helpers
{
    public static class TestHelpers
    {
        public static Mock<UserManager<TUser>> CreateUserManagerMock<TUser>()
            where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();

            return new Mock<UserManager<TUser>>(
                store.Object,
                null!,
                null!,
                new List<IUserValidator<TUser>>(),
                new List<IPasswordValidator<TUser>>(),
                null!,
                null!,
                null!,
                null!);
        }
    }
}