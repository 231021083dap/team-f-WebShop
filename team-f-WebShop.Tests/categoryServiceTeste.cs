using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using team_f_WebShop.API.Database.Entities;
using team_f_WebShop.API.DTOs.Requests;
using team_f_WebShop.API.DTOs.Responses;
using team_f_WebShop.API.Repositories;
using team_f_WebShop.API.Services;
using Xunit;

namespace team_f_WebShop.Tests
{

    public class categoryServiceTeste
    {
        private readonly categoryService _sut;
        private readonly Mock<IcategoryRepository> _categoryRepository = new();

        public categoryServiceTeste()
        {
            _sut = new categoryService(_categoryRepository.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnListOfcatagoryResponses_WhencategorysExists()
        {
            // Arrange
            List<category> categorys = new();

            categorys.Add(new category
            {
                Id = 1,
                categoryName = "Computer"
            });
            categorys.Add(new category
            {
                Id = 2,
                categoryName = "Screen"
            });

            _categoryRepository
                .Setup(a => a.GetAll())
                .ReturnsAsync(categorys);

            // Act

            var result = await _sut.GetAllcategory();

            // Assert

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<categoryResponse>>(result);
        }

        [Fact]
        public async void GetAll_ShouldReturnEmptyListOfcategoryResponses_WhenNoCategoryExists()
        {
            // Arrange
            List<category> categorys = new List<category>();

            _categoryRepository
                .Setup(a => a.GetAll())
                .ReturnsAsync(categorys);

            // Act
            var result = await _sut.GetAllcategory();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<categoryResponse>>(result);
        }

        [Fact]
        public async void GetById_ShouldReturnAncategoryResponse_WhencategoryExists()
        {
            // Arrange
            int categoryId = 1;

            category category = new category
            {
                Id = categoryId,
                categoryName = "Computer"
            };

            _categoryRepository
                .Setup(a => a.GetById(It.IsAny<int>()))
                .ReturnsAsync(category);

            // Act
            var result = await _sut.GetById(categoryId);

            // Asseret
            Assert.NotNull(result);
            Assert.IsType<categoryResponse>(result);
            Assert.Equal(categoryId, result.Id);
            Assert.Equal(category.categoryName, result.categoryName);
        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhencategoryDoesNotExist()
        {
            // Arrange
            int categoryId = 1;

            _categoryRepository
                .Setup(a => a.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.GetById(categoryId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Create_ShouldReturncategoryResponse_WhenCreateIsSuccess()
        {
            // Arrange

            NewCategory newCategory = new NewCategory
            {
                categoryName = "Computer"
            };

            int categoryId = 1;

            category category = new category
            {
                Id = categoryId,
                categoryName = "Computer"
            };

            _categoryRepository
                .Setup(a => a.Create(It.IsAny<category>()))
                .ReturnsAsync(category);

            // Act
            var result = await _sut.Create(newCategory);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<categoryResponse>(result);
            Assert.Equal(categoryId, result.Id);
            Assert.Equal(newCategory.categoryName, result.categoryName);
        }

        [Fact]
        public async void Update_ShouldReturnUpdatecategoryResponse_WhenUpdateIsSuccess()
        {
            // Arrange
            UpdateCategory updateCategory = new UpdateCategory
            {
                categoryName = "Computer"
            };

            int categoryId = 1;

            category category = new category
            {
                Id = categoryId,
                categoryName = "Computer"
            };

            _categoryRepository
                .Setup(a => a.Update(It.IsAny<int>(), It.IsAny<category>()))
                .ReturnsAsync(category);

            // Act
            var result = await _sut.Update(categoryId, updateCategory);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<categoryResponse>(result);
            Assert.Equal(categoryId, result.Id);
            Assert.Equal(updateCategory.categoryName, result.categoryName);
        }

        [Fact]
        public async void Update_ShouldReturNull_WhencategoryDoesNotExist()
        {
            // Arrange
            UpdateCategory updateCategory = new UpdateCategory
            {
                categoryName = "Computer"
            };

            int categoryId = 1;

            _categoryRepository
                .Setup(a => a.Update(It.IsAny<int>(), It.IsAny<category>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.Update(categoryId, updateCategory);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Delete_ShouldReturnTrue_WhenDeleteIsSuccess()
        {
            // Arrange
            int categoryId = 1;

            category category = new category
            {
                Id = categoryId,
                categoryName = "Computer",
            };

            _categoryRepository
                .Setup(a => a.Delete(It.IsAny<int>()))
                .ReturnsAsync(category);

            // Act
            var result = await _sut.Delete(categoryId);

            // Assert
            Assert.True(result);
            
        }
    }
}