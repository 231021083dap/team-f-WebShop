// Orangular
using OrangularAPI.Database.Entities;
using OrangularAPI.Helpers;
using OrangularAPI.Repositories.Users;
using OrangularAPI.Database;
// Orangular
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System;


namespace OrangularTests.UserTest
{
   public class UserRepositoryTest
    {
        private readonly UserRepository _sut;
        private readonly OrangularProjectContext _context;
        private readonly DbContextOptions<OrangularProjectContext> _options;
        public UserRepositoryTest()
        {
            _options = new DbContextOptionsBuilder<OrangularProjectContext>()
            .UseInMemoryDatabase(databaseName: "OrangularUsersDatabase")
            .Options;
            _context = new OrangularProjectContext(_options);
            _sut = new UserRepository(_context);
        }
        // -----------------------------------------------------------------------------------------------------------------------
        // GetAll Tests
        [Fact]
        public async Task GetAll_ShouldReturnListOfUsers_WhenUsersExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();
            _context.User.Add(new User
            {
                Id = 1,
                Email = "Test1@Mail.com",
                Password = "Passw0rd",
                Role = Role.Admin
            });
            _context.User.Add(new User
            {
                Id = 2,
                Email = "Test2@Mail.com",
                Password = "Passw0rd",
                Role = Role.User
            });
            await _context.SaveChangesAsync();
            //Act
            var result = await _sut.GetAll();
            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<User>>(result);
        }
        [Fact]
        public async Task GetAll_ShouldReturnEmptyListOfUsers_WhenNoUsersExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var result = await _sut.GetAll();
            //Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<User>>(result);
        }
        // -----------------------------------------------------------------------------------------------------------------------
        // GetById Tests
        [Fact]
        public async Task GetById_ShouldReturnUser_IfUserExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int userId = 1;
            _context.User.Add(new User
            {
                Id = userId,
                Email = "Test1@Mail.com",
                Password = "Passw0rd",
                Role = Role.Admin
            });
            await _context.SaveChangesAsync();
            // Act
            var result = await _sut.GetById(userId);
            // Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(userId, result.Id);
        }
        [Fact]
        public async Task GetById_ShouldReturnNull_IfUserDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int userId = 1;
            // Act
            var result = await _sut.GetById(userId);
            // Assert
            Assert.Null(result);
        }
        // -----------------------------------------------------------------------------------------------------------------------
        // GetByEmail Tests
        [Fact]
        public async Task GetByEmail_ShouldReturnEmail_IfEmailExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            string userEmail = "Test1@Mail.com";
            _context.User.Add(new User
            {
                Id = 1,
                Email = userEmail,
                Password = "Passw0rd",
                Role = Role.Admin
            });
            await _context.SaveChangesAsync();
            // Act
            var result = await _sut.GetByEmail(userEmail);
            // Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(userEmail, result.Email);
        }
        [Fact]
        public async Task GetByEmail_ShouldReturnNull_IfEmailDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            string userEmail = "Test1@Mail.com";
            // Act
            var result = await _sut.GetByEmail(userEmail);
            // Assert
            Assert.Null(result);
        }
        // -----------------------------------------------------------------------------------------------------------------------
        // Create Tests
        [Fact]
        public async Task Create_ShouldAddIdToUser_WhenSavingToDatabase()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int expectedId = 1;
            User user = new User
            {
                Email = "Test1@Mail.com",
                Password = "Passw0rd",
                Role = Role.User
            };
            // Act
            var result = await _sut.Create(user);
            // Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(expectedId, result.Id);
        }
        [Fact]
        public async Task Create_ShouldFailToAddUser_WhenAddingUserWithExistingId()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            User user = new User
            {
                Id = 1,
                Email = "Test1@Mail.com",
                Password = "Passw0rd",
                Role = Role.User
            };
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            // Act
            Func<Task> action = async () => await _sut.Create(user);
            // Assert
            var ex = await Assert.ThrowsAsync<Exception>(action);
            Assert.Contains("Nice try, userId " + user.Id + " already Exists", ex.Message);
        }
        [Fact]
        public async Task Create_ShouldFailToAddUser_WhenAddingUserWithExistingEmail()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            User user1 = new User
            {
                Id = 1,
                Email = "Test1@Mail.com",
                Password = "Passw0rd",
                Role = Role.User
            };
            User user2 = new User
            {
                Id = 2,
                Email = "Test1@Mail.com",
                Password = "Passw0rd",
                Role = Role.User
            };
            _context.User.Add(user1);
            await _context.SaveChangesAsync();
            // Act
            Func<Task> action = async () => await _sut.Create(user2);
            // Assert
            var ex = await Assert.ThrowsAsync<Exception>(action);
            Assert.Contains("Email " + user1.Email + " is already taken", ex.Message);
        }
        [Fact]
        public async Task Create_ShouldFailToAddUser_WhenPasswordAndOrEmailIsNull()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            User user = new User
            {
                Id = 1,
                Role = Role.User
            };
            // Act
            Func<Task> action = async () => await _sut.Create(user);
            // Assert
            var ex = await Assert.ThrowsAsync<Exception>(action);
            Assert.Contains("You must enter an email and a password to create a user", ex.Message);
        }
        // -----------------------------------------------------------------------------------------------------------------------
        // Update Tests
        [Fact]
        public async Task Update_ShouldChangeValuesOnUser_WhenUserExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int userId = 1;
            User user = new User
            {
                Id = userId,
                Email = "Test1@Mail.com",
                Password = "Passw0rd",
                Role = Role.User
            };
            User userUpdate = new User
            {
                Id = userId,
                Email = "est1@Mail.com",
                Password = "assw0rd",
                Role = Role.Admin
            };
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            // Act
            var result = await _sut.Update(userId, userUpdate);
            // Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(userId, result.Id);
            Assert.Equal(userUpdate.Email, result.Email);
            Assert.Equal(userUpdate.Password, result.Password);
            Assert.Equal(userUpdate.Role, result.Role);
        }
        [Fact]
        public async Task Update_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int userId = 1;
            User userUpdate = new User
            {
                Id = userId,
                Email = "Test1@Mail.com",
                Password = "Passw0rd",
                Role = Role.User
            };
            // Act
            var result = await _sut.Update(userId, userUpdate);
            // Assert
            Assert.Null(result);
        }
        [Fact]
        public async Task Update_ShouldFailToUpdateUser_WhenUpdatingEmailToExistingEmail()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int user1Id = 1;
            User user1 = new User
            {
                Id = user1Id,
                Email = "Test1@Mail.com",
                Password = "Passw0rd",
                Role = Role.User
            };
            User user2 = new User
            {
                Id = 2,
                Email = "Test2@Mail.com",
                Password = "Passw0rd",
                Role = Role.Admin
            };
            _context.User.Add(user1); 
            _context.User.Add(user2);
            await _context.SaveChangesAsync();
            User user1Update = new User
            {
                Id = user1Id,
                Email = "Test2@Mail.com",
                Password = "Passw0rd",
                Role = Role.User
            };
            // Act
            Func<Task> action = async () => await _sut.Update(user1Id, user1Update);
            // Assert
            var ex = await Assert.ThrowsAsync<Exception>(action);
            Assert.Contains("Email " + user2.Email + " is already taken", ex.Message);
        }
        // -----------------------------------------------------------------------------------------------------------------------
        // Delete Tests
        [Fact]
        public async Task Delete_ShouldReturnDeletedUser_WhenUserIsDeleted()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int userId = 1;
            User user = new User
            {
                Id = userId,
                Email = "Test1@Mail.com",
                Password = "Passw0rd",
                Role = Role.User
            };
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            // Act
            var result = await _sut.Delete(userId);
            var users = await _sut.GetAll();
            // Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(userId, result.Id);
            Assert.Empty(users);
        }
        [Fact]
        public async Task Delete_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int userId = 1;
            // Act
            var result = await _sut.Delete(userId);
            // Assert
            Assert.Null(result);
        }

    }
}
