using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrangularAPI.Database;
using OrangularAPI.Database.Entities;
using OrangularAPI.Helpers;
using OrangularAPI.Repositories.AddressesRepository;

namespace OrangularTests.AddressesTest
{
    public class AddressesRepositoryTests
    {
        private readonly AddressRepository _sut;
        private readonly OrangularProjectContext _context;
        private readonly DbContextOptions<OrangularProjectContext> _options;

        public AddressesRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<OrangularProjectContext>()
                .UseInMemoryDatabase(databaseName: "OrangularProjectContextAddresses")
                .Options;

            _context = new OrangularProjectContext(_options);

            _sut = new AddressRepository(_context);
        }


        // ---- GetAll tests ---- //
            [Fact]
            public async Task GetAll_ShouldReturnListOfAddresses_WhenAddressesExists()
            {

                // Arrange
                await _context.Database.EnsureDeletedAsync();


                // Tilføj Users
                _context.User.Add(
                new User
                {
                    Id = 1,
                    Email = "admin@admins.com",
                    Password = "Passw0rd",
                    Role = Role.Admin
                });

                _context.User.Add(
                new User
                {
                    Id = 2,
                    Email = "user@users.com",
                    Password = "Passw0rd",
                    Role = Role.User
                });

                _context.Address.Add(
                new Address
                {
                    Id = 1,                    
                    AddressName = "TEC Ballerup",
                    ZipCode = 2750,
                    CityName = "Ballerup",
                    UserId = 1,
                });

                _context.Address.Add(
                new Address
                {
                    Id = 2,                    
                    AddressName = "Hjem Helsingør",
                    ZipCode = 3000,
                    CityName = "Helsingør",
                    UserId = 2
                });


                await _context.SaveChangesAsync();

                // Act
                var result = await _sut.GetAll();

                // Assert
                Assert.NotNull(result);
                Assert.IsType<List<Address>>(result);
                Assert.Equal(2, result.Count);
            }
            [Fact]
            public async Task GetAll_ShouldReturnEmptyListOfAddresses_WhenNoAddressesExists() 
            {
                // Arrange
                await _context.Database.EnsureDeletedAsync();

                // Act
                var result = await _sut.GetAll();

                // Assert
                Assert.NotNull(result);
                Assert.IsType<List<Address>>(result);
                Assert.Empty(result);    
            }
        // ---- GetAll tests ---- //


        // ---- GetById tests ---- //
            [Fact]
            public async Task GetById_ShouldReturnTheAddress_IfAddressExists()
            {
                // Arrange
                await _context.Database.EnsureDeletedAsync();
                int search_id = 1;
                _context.Address.Add(
                new Address
                {
                    Id = 1,
                    // UserId = 1,
                    AddressName = "TEC Ballerup",
                    ZipCode = 2750
                });
                await _context.SaveChangesAsync();

                // Act
                var result = await _sut.GetById(search_id);

                // Assert
                Assert.NotNull(result);
                Assert.IsType<Address>(result);
                Assert.Equal(search_id, result.Id);
            }


            [Fact]
            public async Task GetById_ShouldReturnNull_IfAddressDoesNotExist()
            {
                // Arrange
                await _context.Database.EnsureDeletedAsync(); // sletter db
                int search_id = 1;

                // Act
                var result = await _sut.GetById(search_id);

                // Assert
                Assert.Null(result);
            }
        // ---- GetById tests ---- //

        // ---- Create tests ---- //
             [Fact]
            public async Task Create_ShouldAddIdToAddress_WhenSavingToDatabase()
            {
                // Arrange
                await _context.Database.EnsureDeletedAsync();
                // Vi kan kun bruge det her id til at finde den nye bruger fordi vi ved at den er 1. 
                // Fordi vi har slettet databasen.
                int expectedId = 1;
                Address address = new Address
                {
                    Id = 1,
                    // UserId = 2,
                    AddressName = "Hjem Helsingør",
                    ZipCode = 3000
                };

                // Act
                var result = await _sut.Create(address);

                // Assert
                Assert.NotNull(result);
                Assert.IsType<Address>(result);
                Assert.Equal(expectedId, result.Id);
            }
             [Fact]
            public async Task Create_ShouldFailToAddAddress_WhenAddingAddressWithExistingId()
            {
                // Arrange
                await _context.Database.EnsureDeletedAsync();

                Address address = new Address
                {
                    Id = 2,
                    // UserId = 2,
                    AddressName = "Hjem Helsingør",
                    ZipCode = 3000
                };
                _context.Address.Add(address);
                await _context.SaveChangesAsync();

                // Act
                Func<Task> action = async () => await _sut.Create(address);

                // Assert
                var ex = await Assert.ThrowsAsync<ArgumentException>(action);
                Assert.Contains("An item with the same key has already been added", ex.Message);
            }
        // ---- Create tests ---- //

        // ---- Update tests ---- //
            [Fact]
            public async Task Update_ShouldChangeValuesOnAddress_WhenAddressExists()
            {
                
                // Arrange
                await _context.Database.EnsureDeletedAsync();
                int updateTargetId = 1;

                Address address = new Address
                {
                    Id = updateTargetId,
                    // UserId = 2,
                    AddressName = "Hjem Helsingør",
                    ZipCode = 3000
                };

                _context.Address.Add(address);
                await _context.SaveChangesAsync();




                // Act
                Address updateAddress = new Address
                {
                    Id = 1,
                    // UserId = 1,
                    AddressName = "TEC Ballerup",
                    ZipCode = 2750
                };

                var result = await _sut.Update(updateTargetId, updateAddress);


                // Assert
                Assert.NotNull(result);
                Assert.IsType<Address>(result);
                Assert.Equal(updateTargetId, result.Id);
                // Assert.Equal(updateAddress.UserId, result.UserId);
                Assert.Equal(updateAddress.AddressName, result.AddressName);
                Assert.Equal(updateAddress.ZipCode, result.ZipCode);
            }
            
            [Fact]
            public async Task Update_ShouldReturnNull_WhenAddressDoesNotExists()
            {
                // Arrange
                await _context.Database.EnsureDeletedAsync();
                int addressId = 1;

                // Act
                Address updateAddresses = new Address
                {
                    Id = 1,
                    // UserId = 1,
                    AddressName = "TEC Ballerup",
                    ZipCode = 2750
                };
                var result = await _sut.Update(addressId, updateAddresses);
                // Assert
                Assert.Null(result);
            }
        // ---- Update tests ---- //


        // ---- Delete tests ---- //
            [Fact]
            public async Task Delete_ShouldReturnDeletedAddress_WhenAddressIsDeleted()
            {

                // Arrange
                await _context.Database.EnsureDeletedAsync();
                int addressId = 1;
                Address address = new Address
                {
                    Id = 1,
                    // UserId = 1,
                    AddressName = "TEC Ballerup",
                    ZipCode = 2750
                };
                _context.Address.Add(address);
                await _context.SaveChangesAsync();


                // Act
                var result = await _sut.Delete(addressId);


                // Assert
                Assert.IsType<Boolean>(result);
            }


            [Fact]
            public async Task Delete_ShouldReturnNull_WhenAddressDoesNotExist()
            {
                // Arrange
                await _context.Database.EnsureDeletedAsync();
                int addressId = 1;

                // Act
                var result = await _sut.Delete(addressId);

                // Assert
                Assert.IsType<Boolean>(result);
        }
        // ---- Delete tests ---- //
    }
}
