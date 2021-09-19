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
    public class categoryControllerTests
    {

        private readonly categoryController _sut;
        private readonly Mock<IcategoryService> _categoryservice = new();

        public categoryControllerTests()
        {
            _sut = new categoryController(_categoryservice.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenDataExists()
        {
            // Arrange
            List<categoryResponse> categorys = new();

            categorys.Add(new categoryResponse
            {
                Id = 1,
                categoryName = "Computer"
            });

            categorys.Add(new categoryResponse
            {
                Id = 2,
                categoryName = "Screen"
            });

            categorys.Add(new categoryResponse
            {
                Id = 3,
                categoryName = "Webcam"
            });

            categorys.Add(new categoryResponse
            {
                Id = 4,
                categoryName = "Printer"
            });

            categorys.Add(new categoryResponse
            {
                Id = 5,
                categoryName = "Tablet"
            });

            _categoryservice
                .Setup(s => s.GetAllcategory())
                .ReturnsAsync(categorys);

            // Act
            var result = await _sut.GetAll();

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode204_WhenNoDataExists()
        {
            // Arrange
            List<categoryResponse> category = new();
            _categoryservice
                .Setup(s => s.GetAllcategory())
                .ReturnsAsync(category);

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
            _categoryservice
                .Setup(s => s.GetAllcategory())
                .ReturnsAsync(() => null);

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
            _categoryservice
                .Setup(s => s.GetAllcategory())
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            // Act
            var result = await _sut.GetAll();

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}
