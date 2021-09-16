using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using team_f_WebShop.API.Controllers;
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





        [Fact]
        public void GetAll_shouldReturnStatusCode200_WhenDataExists()
        {
            // Arrange
            List<ProductResponse> Products = new();

            Products.Add(new ProductResponse
            {
                ProductId = 1,
                Name = "GIGABYTE FI32U",
                Price = 8575,
                Quantity = 6,
                Desciption = "LED-skærm"
            });

            Products.Add(new ProductResponse
            {
                ProductId = 2,
                Name = "GIGABYTE M28U",
                Price = 5999,
                Quantity = 13,
                Desciption = "3840 x 2160 (4K)"
            });

            _productService
                .Setup(s => s.GetAllProducts())
                .Returns(Products);


            // Act
            var result = _sut.GetAll();


            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }



        [Fact]
        public void GetAll_shouldReturnStatusCode204_WhenDataDoesNotExist()
        {
            // Arrange
            List<ProductResponse> Products = new();


            _productService
                .Setup(s => s.GetAllProducts())
                .Returns(Products);

            // Act
            var result = _sut.GetAll();


            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);
        }




        [Fact]
        public void GetAll_shouldReturnStatusCode500_WhenNullIsReturnedFromService()
        {
            // Arrange
            _productService
                .Setup(s => s.GetAllProducts())
                .Returns(() => null); //return null

            // Act
            var result = _sut.GetAll();


            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }



        [Fact]
        public void GetAll_shouldReturnStatusCode500_WhenExeptionIsRaised()
        {
            // Arrange
            _productService
                .Setup(s => s.GetAllProducts())
                .Returns(() => throw new System.Exception("This is an exception"));

            // Act
            var result = _sut.GetAll();


            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        // _______________________________________________________________________________




        [Fact]
        public void GetById_shouldReturnStatusCode200_WhenDataExists()
        {
            // Arrange


            // Act


            // Assert


        }

    }
}
