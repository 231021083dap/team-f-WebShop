using OrangularAPI.Database.Entities;
using Moq;
using System.Collections.Generic;
using Xunit;

using OrangularAPI.Services.AddressServices;
using OrangularAPI.DTO.Addresses.Requests;
using OrangularAPI.DTO.Addresses.Responses;
using OrangularAPI.Repositories.AddressesRepository;
using OrangularAPI.Repositories.Users;

namespace OrangularTests.AddressesTest
{
    public class AddressesServiceTests
    {
        private readonly AddressService _sut;
        private readonly Mock<IAddressRepository> _addressesRepository = new();
        private readonly Mock<IUserRepository> _userRepository = new();

        public AddressesServiceTests()
        {
            _sut = new AddressService(_addressesRepository.Object, _userRepository.Object);
        }

        // ---- GetAll Tests ---- //
           [Fact]
            public async void GetAll_ShouldReturnListOfAddressesResponses_WhenAddressesExist()
            {
                // Arrange
                List<Address> Address = new List<Address>();

                Address.Add(new Address
                {
                    Id = 1,
                    AddressName = "TEC Ballerup",
                    ZipCode = 2750,
                    CityName = "Ballerup",
                    UserId = 1,
                    User = new ()
                });
                Address.Add(new Address
                {
                    Id = 2,
                    AddressName = "Hjem Helsingør",
                    ZipCode = 3000,
                    CityName = "Helsingør",
                    UserId = 2,
                    User = new()
                });

                _addressesRepository
                    .Setup(_addressRepository => 
                    _addressRepository.GetAll())
                    .ReturnsAsync(Address);

                // Act
                var result = await _sut.GetAll();

                // Assert
                Assert.NotNull(result);
                Assert.Equal(2, result.Count);
                Assert.IsType<List<AddressResponse>>(result); // vi forventer en liste af typen AddressesResponse
            }



            [Fact]
            public async void GetAll_ShouldReturnEmptyListOfAddressesResponses_WhenNoAddressesExists()
            {

            // Arrange
            List<Address> Addresses = new List<Address>();

            _addressesRepository
                .Setup(a => a.GetAll())
                .ReturnsAsync(Addresses);

            // Act
            var result = await _sut.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<AddressResponse>>(result);
            }
        // ---- GetAll Tests ---- //

        // ---- GetById Tests ---- //
            [Fact]
            public async void GetById_ShouldReturnAnAddressesResponse_WhenAddressesExists()
            {
                // Arrange
                int search_id = 1;

                Address address = new Address
                {
                    Id = 1,
                    AddressName = "TEC Ballerup",
                    ZipCode = 2750,
                    // UserId = 1,
                };

                _addressesRepository
                    .Setup(a => a.GetById(It.IsAny<int>()))
                    .ReturnsAsync(address);

                // Act
                var result = await _sut.GetById(search_id);

                // Assert
                Assert.NotNull(result);
                Assert.IsType<AddressResponse>(result);
                Assert.Equal(address.Id, result.Id);
            }

            [Fact]
            public async void GetById_ShouldReturnNull_WhenAddressesDoesNotExist()
            {
                // Arrange
                int search_id = 1;

                _addressesRepository
                    .Setup(a => a.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);
                
                // Act
                var result = await _sut.GetById(search_id);

                // Assert
                Assert.Null(result);
            }
        // ---- GetById Tests ---- //

        // ---- Create Tests ---- //
            [Fact]
            public async void Create_ShouldReturnAddressesResponse_WhenCreateIsSuccess()
            {
                // Arrange
                int search_id = 1;

                Address addressssss = new Address
                {
                    Id = 1, 
                    User = new()
                };

                NewAddress newAddress = new NewAddress
                {
                    UserId = 1,
                    Address = "TEC Ballerup",
                    ZipCode = 2750,
                    CityName = "Ballerup"
                };

                _addressesRepository
                    .Setup(a => a.Create(It.IsAny<Address>()))
                .ReturnsAsync(addressssss);

                // Act
                var result = await _sut.Create(newAddress);

                // Assert
                Assert.NotNull(result);
                Assert.IsType<AddressResponse>(result);

        }
        // ---- Create Tests ---- //

        // ---- Update Tests ---- //
            [Fact]
            public async void Update_ShouldReturnUpdatedAddressesResponse_WhenUpdateIsSuccess()
            {
                // Arrange
                int search_id = 1;

                UpdateAddress updateAddress = new UpdateAddress
                {
                    UserId = 2,
                    Address = "Hjem Helsingør",
                    ZipCode = 3000,
                    CityName = "Helsingør"
                };

                Address address = new Address
                {
                    Id = 1,
                    // UserId = 2,
                    AddressName = "Hjem Helsingør",
                    ZipCode = 3000,
                    CityName = "Helsingør"
                };

                // Opstiller address service typen så den retunere 'address' instancen. 
                _addressesRepository
                    .Setup(lambda => lambda.Update(It.IsAny<int>(), It.IsAny<Address>()))
                    .ReturnsAsync(address);

                // Act - vi forventer en autherResponse
                var result = await _sut.Update(1, updateAddress);

                // Assert
                Assert.NotNull(result);
                Assert.IsType<AddressResponse>(result);
                Assert.Equal(search_id, result.Id);
                Assert.Equal(updateAddress.Address, result.Address);
                Assert.Equal(updateAddress.ZipCode, result.ZipCode);
            }

            [Fact]
            public async void Update_ShouldReturnNull_WhenAddressesDoesNotExist()
            {
                // Arrange
                int search_id = 1;

                UpdateAddress updateAddress = new UpdateAddress
                {
                    UserId = 2,
                    Address = "Hjem Helsingør",
                    ZipCode = 3000,
                    CityName = "Helsingør"
                };

            // Opstiller 
            _addressesRepository
                .Setup(lambda => lambda.Update(It.IsAny<int>(), It.IsAny<Address>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.Update(1, updateAddress);

            // Assert
            Assert.Null(result);
        }
        // ---- Update Tests ---- //

        // ---- Delete Tests ---- //
            [Fact]
            public async void Delete_ShouldReturnTrue_WhenDeleteIsSuccess()
            {
                // Arrange
                int search_id = 1;

                Address address = new Address
                {
                    Id = 1,
                    // UserId = 2,
                    AddressName = "Hjem Helsingør",
                    ZipCode = 3000,
                    CityName = "Helsingør"
                };

                // Opstiller 
                _addressesRepository
                    .Setup(a => a.Delete(It.IsAny<int>()))
                    .ReturnsAsync(true);


                // Act
                var result = await _sut.Delete(1);

                // Assert
                Assert.True(result);
        }
        // ---- Delete Tests ---- //
    }
}