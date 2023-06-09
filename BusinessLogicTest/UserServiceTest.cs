using BussinesLogic.Services;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.IdentityModel.Tokens;
using Moq;

namespace BusinessLogicTest
{
    public class UserServiceTest
    {
        private readonly UserService service;
        private readonly Mock<IUserRepository> userRepositoryMoq;

        public UserServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            userRepositoryMoq = new Mock<IUserRepository>();

            repositoryWrapperMoq.Setup(x => x.User).Returns(userRepositoryMoq.Object);

            service = new UserService(repositoryWrapperMoq.Object);
        }

        public static IEnumerable<object[]> GetIncorrectUsers()
        {
            return new List<object[]>
            {
                new object[] { new User() {
                Email = "",
                Login = "",
                Password = "",
                RoleId = 1,
                IsDeleted = false,
                Adress = "" } },

                new object[] {new User()
                {
                    Email = "Test",
                    Login = "",
                    Password = "",
                    RoleId = 1,
                    IsDeleted = false,
                    Adress = ""
                } },

                new object[] {new User()
                {
                    Email = "Test",
                    Login = "Test",
                    Password = "",
                    RoleId = 1,
                    IsDeleted = false,
                    Adress = ""
                } },
            };
        }

        [Fact]
        public async Task CreateNullUser()
        {
            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<User>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectUsers))]
        public async Task CreateNewUser()
        {
            var newUser = new User()
            {
                UserId = 1,
                Email = "",
                Login = "",
                Password = "",
                RoleId = 1,
                IsDeleted = false,
                Adress = ""
            };

            var ex = await Assert.ThrowsAnyAsync<ArgumentNullException>(() => service.Create(newUser));

            userRepositoryMoq.Verify(x => x.Create(It.IsAny<User>()), Times.Never);
            Assert.IsType<ArgumentException>(ex);
        }
    }
}
