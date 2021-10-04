using Moq;
using System.Collections.Generic;
using Xunit;
using OrangularAPI.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using OrangularAPI.Services.AddressServices;
using OrangularAPI.DTO.Addresses.Requests;
using OrangularAPI.DTO.Addresses.Responses;

namespace OrangularTests.AddressesTest
{
    public class AddressControllerTests
    {
        // underscore symbolisere private property
        private readonly AddressController _sut;

        private readonly Mock<IAddressService> _addressService = new();

        public AddressControllerTests()
        {
            _sut = new AddressController(_addressService.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenDataExists()
        {
            // Arrange
            List<AddressResponse> addresses = new();

            addresses.Add(new AddressResponse
            {
                Id = 1,
                Address = "Vinkelvej",
                ZipCode = 2800,
                CityName = "Lyngby"
            });

            addresses.Add(new AddressResponse
            {
                Id = 2,
                Address = "Kusør",
                ZipCode = 1234,
                CityName = "Kagerup"
            });


            _addressService.Setup(s => s.GetAll()).ReturnsAsync(addresses);


            // Act
            var result = await _sut.GetAll();

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode204_WhenNoElementExists()
        {
            // Arrange
            List<AddressResponse> addresses = new();

            _addressService.Setup(s => s.GetAll()).ReturnsAsync(addresses);

            // Act
            var result = await _sut.GetAll();

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenNullIsReturnedFromService()
        {
            // Arrange
            _addressService.Setup(s => s.GetAll()).ReturnsAsync(() => null);
            // Act
            var result = await _sut.GetAll();
            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            _addressService.Setup(s => s.GetAll()).ReturnsAsync(() => throw new System.Exception("This is an exeption"));
            // Act
            var result = await _sut.GetAll();
            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode200_WhenDataExists()
        {
            // Arrange
            AddressResponse address = new AddressResponse
            {
                Id = 1,
                Address = "Vinkelvej",
                ZipCode = 2800,
                CityName = "Lyngby"
            };


            _addressService.Setup(s => s.GetById(1)).ReturnsAsync(address);


            // Act
            var result = await _sut.GetById(1);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhenAddressDoesNotExist()
        {
            // Arrange
            AddressResponse address = new AddressResponse
            {
                Id = 1,
                Address = "Vinkelvej",
                ZipCode = 2800,
                CityName = "Lyngby"
            };


            _addressService.Setup(s => s.GetById(1)).ReturnsAsync(() => null);


            // Act
            var result = await _sut.GetById(1);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);
        }
        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            AddressResponse address = new AddressResponse
            {
                Id = 1,
                Address = "Vinkelvej",
                ZipCode = 2800,
                CityName = "Lyngby"
            };


            _addressService.Setup(s => s.GetById(1)).ReturnsAsync(() => throw new System.Exception("This is an exeption"));


            // Act
            var result = await _sut.GetById(1);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode200_WhenDataIsCreated()
        {
            // Arrange
            NewAddress newAddress = new NewAddress
            {
                UserId = 1,
                Address = "Vinkelvej",
                ZipCode = 2800,
                CityName = "Lyngby"
            };

            AddressResponse addressResponse = new AddressResponse
            {
                Id = 1,
                Address = "Vinkelvej",
                ZipCode = 2800,
                CityName = "Lyngby"
            };
            _addressService.Setup(s => s.Create(newAddress)).ReturnsAsync(addressResponse);

            // Act
            var result = await _sut.Create(newAddress);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);


        }

        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            NewAddress newAddress = new NewAddress
            {
                UserId = 1,
                Address = "Vinkelvej",
                ZipCode = 2800,
                CityName = "Lyngby"
            };

            _addressService.Setup(s => s.Create(It.IsAny<NewAddress>())).ReturnsAsync(() => throw new System.Exception("This is an exception"));

            // Act
            var result = await _sut.Create(newAddress);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);

        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenDataIsSaved()
        {
            // Arrange
            UpdateAddress updateAddress = new UpdateAddress
            {
                UserId = 1,
                Address = "Vinkelvej",
                ZipCode = 2800,
                CityName = "Lyngby"
            };

            AddressResponse addressResponse = new AddressResponse
            {
                Id = 1,
                Address = "Vinkelvej",
                ZipCode = 2800,
                CityName = "Lyngby"
            };

            _addressService.Setup(s => s.Update(1,updateAddress)).ReturnsAsync(addressResponse);

            // Act
            var result = await _sut.Update(1,updateAddress);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);


        }

        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            UpdateAddress updateAddress = new UpdateAddress
            {
                UserId = 1,
                Address = "Vinkelvej",
                ZipCode = 2800,
                CityName = "Lyngby"
            };

            _addressService.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<UpdateAddress>())).ReturnsAsync(() => throw new System.Exception("This is an exception"));

            // Act
            var result = await _sut.Update(1, updateAddress);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode204_WhenAddressIsDeleted()
        {
            // Arrange
            int search_id = 1;

            _addressService.Setup(s => s.Delete(It.IsAny<int>())).ReturnsAsync(() => true);

            // Act
            var result = await _sut.Delete(1);
            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int search_id = 1;

            _addressService.Setup(s => s.Delete(It.IsAny<int>())).ReturnsAsync(() => throw new System.Exception("This is an exception"));

            // Act
            var result = await _sut.Delete(1);
            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}
