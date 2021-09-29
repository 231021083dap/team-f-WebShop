using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using team_f_WebShop.API.Controllers;
using team_f_WebShop.API.DTOs.Requests;
using team_f_WebShop.API.DTOs.Responses;
using team_f_WebShop.API.Services;
using Xunit;

namespace team_f_WebShop.Tests
{
    public class ProductControllerTests
    {
        private readonly ProductController _sut;
        private readonly Mock<IProductService> _productService = new();

        public ProductControllerTests()
        {
            _sut = new ProductController(_productService.Object);
        }



        // GET ALL

        [Fact]
        public async void GetAll_shouldReturnStatusCode200_WhenDataExists()
        {
            // Arrange
            List<ProductResponse> Products = new();

            Products.Add(new ProductResponse
            {
                ProductId = 1,
                Name = "GIGABYTE FI32U",
                Price = 8575,
                Quantity = 6,
                Description = "LED-skærm"
            });

            Products.Add(new ProductResponse
            {
                ProductId = 2,
                Name = "GIGABYTE M28U",
                Price = 5999,
                Quantity = 13,
                Description = "3840 x 2160 (4K)"
            });

            _productService
                .Setup(s => s.GetAllProductService())
                .ReturnsAsync(Products);


            // Act
            var result = await _sut.GetAllProductController();


            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, statusCodeResult.StatusCode);
        }


        [Fact]
        public async void GetAll_shouldReturnStatusCode204_WhenDataDoesNotExist()
        {
            // Arrange
            List<ProductResponse> Products = new();


            _productService
                .Setup(s => s.GetAllProductService())
                .ReturnsAsync(Products);

            // Act
            var result = await _sut.GetAllProductController();


            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);
        }


        [Fact]
        public async void GetAll_shouldReturnStatusCode500_WhenNullIsReturnedFromService()
        {
            // Arrange
            _productService
                .Setup(s => s.GetAllProductService())
                .Returns(() => null); //return null

            // Act
            var result = await _sut.GetAllProductController();


            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }


        [Fact]
        public async void GetAll_shouldReturnStatusCode500_WhenExeptionIsRaised()
        {
            // Arrange
            _productService
                .Setup(s => s.GetAllProductService())
                .Returns(() => throw new System.Exception("This is an exception"));

            // Act
            var result = await _sut.GetAllProductController();


            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }


        // _______________________________________________________________________________________________________

        // GET BY ID

        [Fact]
        public async void GetById_shouldReturnStatusCode200_WhenDataExists()
        {
            // Arrange
            int productId = 1;
            ProductResponse Product = new ProductResponse
            {
                ProductId = productId,
                Name = "GIGABYTE FI32U",
                Price = 8575,
                Quantity = 6,
                Description = "LED-skærm"
            }; 

            _productService
                .Setup(s => s.GetByIdProductService(It.IsAny<int>()))
                .ReturnsAsync(Product);


            // Act
            var result = await _sut.GetByIdProductController(productId);


            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, statusCodeResult.StatusCode);
        }


        [Fact]
        public async void GetById_shouldReturnStatusCode404_WhenProductDoesNotExist()
        {
            // Arrange
            int productId = 1;

            _productService
                .Setup(s => s.GetByIdProductService(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.GetByIdProductController(productId);


            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);
        }


        [Fact]
        public async void GetById_shouldReturnStatusCode500_WhenExeptionIsRaised()
        {
            // Arrange
            _productService
                .Setup(s => s.GetByIdProductService(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exeption"));

            // Act
            var result = await _sut.GetByIdProductController(1);


            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }


        //_____________________________________________________________________________________________________

        // CREATE

        [Fact]
        public async void Create_shouldReturnStatusCode200_WhenDataIsCreated()
        {
            // Arrange
            int productId = 1;
            NewProduct newProduct = new NewProduct
            {
                Name = "GIGABYTE FI32U",
                Price = 8575,
                Quantity = 6,
                Description = "LED-skærm"
            };


            ProductResponse product = new ProductResponse
            {
                ProductId = productId,
                Name = "GIGABYTE FI32U",
                Price = 8575,
                Quantity = 6,
                Description = "LED-skærm"
            };

            _productService
                .Setup(s => s.CreateProductService(It.IsAny<NewProduct>()))
                .ReturnsAsync(product);


            // Act
            var result = await _sut.CreateProductController(newProduct);


            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }


        [Fact]
        public async void Create_shouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            NewProduct newProduct = new NewProduct
            {
                Name = "GIGABYTE FI32U",
                Price = 8575,
                Quantity = 6,
                Description = "LED-skærm"
            };

            _productService
                .Setup(s => s.CreateProductService(It.IsAny<NewProduct>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));


            // Act
            var result = await _sut.CreateProductController(newProduct);


            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }


        //_____________________________________________________________________________________________________

        // UPDATE

        [Fact]
        public async void Update_shouldReturnStatusCode200_WhenDataIsSaved()
        {
            // Arrange
            int productId = 1;
            UpdateProduct updateProduct = new UpdateProduct
            {
                Name = "GIGABYTE FI32U",
                Price = 8575,
                Quantity = 6,
                Description = "LED-skærm"
            };


            ProductResponse product = new ProductResponse
            {
                ProductId = productId,
                Name = "GIGABYTE FI32U",
                Price = 8575,
                Quantity = 6,
                Description = "LED-skærm"
            };

            _productService
                .Setup(s => s.UpdateProductService(It.IsAny<int>(), It.IsAny<UpdateProduct>()))
                .ReturnsAsync(product);


            // Act
            var result = await _sut.UpdateProductController(productId, updateProduct);


            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }


        [Fact]
        public async void Update_shouldReturnStatusCode500_WhenExeptionIsRaised()
        {
            // Arrange
            int productId = 1;
            UpdateProduct updateProduct = new UpdateProduct
            {
                Name = "GIGABYTE FI32U",
                Price = 8575,
                Quantity = 6,
                Description = "LED-skærm"
            };


            _productService
                .Setup(s => s.UpdateProductService(It.IsAny<int>(), It.IsAny<UpdateProduct>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));


            // Act
            var result = await _sut.UpdateProductController(productId, updateProduct);


            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }



        //_____________________________________________________________________________________________________
        
        // DELETE

        [Fact]
        public async void Delete_shouldReturnStatusCode204_WhenProductIsDeleted()
        {
            // Arrange
            int productId = 1;

            _productService
                .Setup(s => s.DeleteProductService(It.IsAny<int>()))
                .ReturnsAsync(true);


            // Act
            var result = await _sut.DeleteProductController(productId);


            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);
        }


        [Fact]
        public async void Delete_shouldReturnStatusCode500_WhenExeptionIsRaised()
        {
            // Arrange
            int productId = 1;

            _productService
                .Setup(s => s.DeleteProductService(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));


            // Act
            var result = await _sut.DeleteProductController(productId);


            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}
