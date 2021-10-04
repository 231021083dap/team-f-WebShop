using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
// Orangular
using OrangularAPI.Database.Entities;
using OrangularAPI.DTO.Login.Requests;
using OrangularAPI.DTO.Users.Responses;
using OrangularAPI.Helpers;
using OrangularAPI.Repositories.Users;
using OrangularAPI.Services.UsersService;
// Orangular

namespace OrangularTests.UserTest
{
    public class UserServiceTest
    {
        private readonly UserService _sut;
        private readonly Mock<IUserRepository> _userRepository = new();
        // private readonly Mock<IJwtUtils> _jwtUtils = new();
        public UserServiceTest()
        {
            _sut = new UserService(_userRepository.Object);
        }
        // -----------------------------------------------------------------------------------------------------------------------
        // Authenticate Tests
        [Fact]
        public async Task Authenticate_ShouldReturnLoginResponse_WhenAuthenticateIsSuccess()
        {
            // Arrange
            // Act
            // Assert
        }
        [Fact]
        public async Task Authenticate_ShouldReturnNull_WhenEmailIsNull()
        {
            // Arrange
            // Act
            // Assert
        }
        [Fact]
        public async Task Authenticate_ShouldReturnNull_WhenPasswordIsNull()
        {
            // Arrange
            // Act
            // Assert
        }

        // -----------------------------------------------------------------------------------------------------------------------
        // GetAll Tests
        [Fact]
        public async Task GetAll_ShouldReturnListOfUserResponses_WhenUsersExists()
        {
            // Arrange
            List<User> user = new();
            user.Add(new User
            {
                Id = 1,
                Email = "Test1@Mail.com",
                Role = Role.User,
                OrderList = new List<OrderList> { },
                Address = new List<Address> { }

            });
            user.Add(new User
            {
                Id = 2,
                Email = "Test2@Mail.com",
                Role = Role.User,
                OrderList = new List<OrderList> { },
                Address = new List<Address> { }
            });
            _userRepository.Setup(u => u.GetAll()).ReturnsAsync(user);
            // Act
            var result = await _sut.GetAll();
            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<UserResponse>>(result);
        }
        [Fact]
        public async Task GetAll_ShouldReturnEmptyListOfUsers_WhenNoUsersExists()
        {
            // Arrange
            List<User> user = new();
            _userRepository.Setup(a => a.GetAll()).ReturnsAsync(user);
            // Act
            var result = await _sut.GetAll();
            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<UserResponse>>(result);
        }
        // -----------------------------------------------------------------------------------------------------------------------
        // GetById Tests
        [Fact]
        public async Task GetById_ShouldReturnUserResponse_WhenUserExists()
        {
            // Arrange
            int userId = 1;
            User user = new()
            {
                Id = userId,
                Email = "Test1@Mail.com",
                Role = Role.User
            };
            _userRepository.Setup(u => u.GetById(It.IsAny<int>())).ReturnsAsync(user);
            // Act
            var result = await _sut.GetById(userId);
            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserResponse>(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.Email, result.Email);
            Assert.Equal(user.Role, result.Role);
        }
        [Fact]
        public async Task GetById_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            int userId = 1;
            _userRepository.Setup(u => u.GetById(It.IsAny<int>())).ReturnsAsync(() => null);
            // Act
            var result = await _sut.GetById(userId);
            // Assert
            Assert.Null(result);
        }
        // -----------------------------------------------------------------------------------------------------------------------
        // Create Tests
        [Fact]
        public async Task Create_ShouldReturnUserResponse_WhenCreateIsSuccess()
        {
            // Arrange
            NewUser newUser = new() // vi sender ind
            {
                Email = "Test1@Mail.com",
                Password = "Passw0rd"
            };
            int userId = 1;
            User user = new()// vi forventer at få tilbage
            {
                Id = userId,
                Email = "Test1@Mail.com",
                Role = Role.User
            };
            _userRepository.Setup(a => a.Create(It.IsAny<User>())).ReturnsAsync(user);
            // Act
            var result = await _sut.Create(newUser);
            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserResponse>(result);
            Assert.Equal(userId, result.Id);
            Assert.Equal(newUser.Email, result.Email);
            Assert.Equal(Role.User, result.Role);
        }
        [Fact]
        public async Task Create_ShouldReturnNull_WhenCreatedUserIsNull()
        {
            // Arrange
            NewUser newUser = new()
            {
                Email = "Test1@Mail.com",
                Password = "Passw0rd"
            };
            _userRepository.Setup(u => u.Create(It.IsAny<User>())).ReturnsAsync(() => null);
            // Act
            var result = await _sut.Create(newUser);
            // Assert
            Assert.Null(result);
        }
        // -----------------------------------------------------------------------------------------------------------------------
        // Update Tests
        [Fact]
        public async Task Update_ShouldReturnUpdatedUserResponse_WhenUpdateIsSuccess()
        {
            // Arrange
            int userId = 1;
            UpdateUser updateUser = new() // sendes til update metoden
            {
                Email = "Test1@Mail.com",
                Password = "Passw0rd",
                Role = Role.User
            };
            User user = new() // sendes til repository
            {
                Id = userId,
                Email = "Test1@Mail.com",
                Role = Role.User
            };
            _userRepository.Setup(u => u.Update(It.IsAny<int>(), It.IsAny<User>())).ReturnsAsync(user);
            // Act
            var result = await _sut.Update(userId, updateUser);
            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserResponse>(result);
            Assert.Equal(userId, result.Id);
            Assert.Equal(updateUser.Email, result.Email);
            Assert.Equal(updateUser.Role, result.Role);
        }
        [Fact]
        public async Task Update_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            UpdateUser updateUser = new()
            {
                Email = "Test1@Mail.com",
                Password = "Passw0rd",
                Role = Role.User
            };
            int userId = 1;
            _userRepository.Setup(u => u.Update(It.IsAny<int>(), It.IsAny<User>())).ReturnsAsync(() => null);
            // Act
            var result = await _sut.Update(userId, updateUser);
            // Assert
            Assert.Null(result);
        }
        // -----------------------------------------------------------------------------------------------------------------------
        // Delete Tests
        [Fact]
        public async Task Delete_ShouldReturnTrue_WhenDeleteIsSuccess()
        {
            // Arrange
            int userId = 1;
            User user = new()
            {
                Id = userId,
                Email = "Test1@Mail.com",
                Password = "Passw0rd",
                Role = Role.User,
            };
            _userRepository.Setup(a => a.Delete(It.IsAny<int>())).ReturnsAsync(user);
            // Act
            var result = await _sut.Delete(userId);
            // Assert
            Assert.True(result);
        }
        [Fact]
        public async Task Delete_ShouldReturnFalse_WhenDeleteIsUnsuccessfull()
        {
            // Arrange
            int userId = 1;
            User user = new()
            {
                Id = userId,
                Email = "Test1@Mail.com",
                Password = "Passw0rd",
                Role = Role.User,
            };
            _userRepository.Setup(a => a.Delete(It.IsAny<int>())).ReturnsAsync(() => null);
            // Act
            var result = await _sut.Delete(userId);
            // Assert
            Assert.False(result);
        }
    }
}
