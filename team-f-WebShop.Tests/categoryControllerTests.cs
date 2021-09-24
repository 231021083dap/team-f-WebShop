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

        [Fact]
        public async void GetById_ShouldReturnStatusCode200_WhenDataExists()
        {
            // Arrange
            int categoryId = 1;
            categoryResponse category = new categoryResponse
            {
                Id = categoryId,
                categoryName = "Computer"
            };

            _categoryservice
                .Setup(a => a.GetById(It.IsAny<int>()))
                .ReturnsAsync(category);

            // Act
            var result = await _sut.GetById(categoryId);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhencategoryDoesNotExists()
        {
            // Arrange

            int categoryId = 1;

            _categoryservice
                .Setup(a => a.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.GetById(categoryId);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            _categoryservice
                .Setup(a => a.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("This is an execption"));

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
            int categoryId = 1;
            NewCategory newCategory = new NewCategory
            {
                categoryName = "Computer"
            };

            categoryResponse category = new categoryResponse
            {
                Id = categoryId,
                categoryName = "Screen"
            };

            _categoryservice
                .Setup(a => a.Create(It.IsAny<NewCategory>()))
                .ReturnsAsync(category);

            // Act
            var result = await _sut.Create(newCategory);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }


        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenExecptionIsRaised()
        {
            // Arrange
            NewCategory newCategory = new NewCategory
            {
                categoryName = "Computer"
            };

            _categoryservice
                .Setup(a => a.Create(It.IsAny<NewCategory>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            // Act
            var result = await _sut.Create(newCategory);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenDataIsSaved()
        {
            int categoryId = 1;
            UpdateCategory updateCategory = new UpdateCategory
            {
                categoryName = "Computer"
            };

            categoryResponse category = new categoryResponse
            {
                Id = categoryId,
                categoryName = "Computer"
            };

            _categoryservice
                .Setup(a => a.Update(It.IsAny<int>(), It.IsAny<UpdateCategory>()))
                .ReturnsAsync(category);

            // Act
            var result = await _sut.Update(categoryId, updateCategory);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }


        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenExecptionIsRaised()
        {
            // Arrange
            int categoryId = 1;
            UpdateCategory updateCategory = new UpdateCategory
            {
                categoryName = "Computer"
            };

            _categoryservice
                .Setup(a => a.Update(It.IsAny<int>(), It.IsAny<UpdateCategory>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            // Act
            var result = await _sut.Update(categoryId, updateCategory);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }


        [Fact]
        public async void Delete_ShouldReturnStatusCode204_WhencategoryIsDeleted()
        {
            // Arrange
            int categoryId = 1;

            _categoryservice
                .Setup(a => a.Delete(It.IsAny<int>()))
                .ReturnsAsync(true);

            // Act
            var result = await _sut.Delete(categoryId);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);
        }


        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenExecptionIsRaised()
        {
            // Arrange
            int categoryId = 1;

            _categoryservice
                .Setup(a => a.Delete(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            // Act
            var result = await _sut.Delete(categoryId);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);

         }      
    }
}
 