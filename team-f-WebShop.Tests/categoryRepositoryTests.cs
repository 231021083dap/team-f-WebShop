using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using team_f_WebShop.API.Database;
using team_f_WebShop.API.Database.Entities;
using team_f_WebShop.API.Repositories;
using Xunit;

namespace team_f_WebShop.Tests
{
    public class categoryRepositoryTests
    {
        private readonly categoryRepository _sut;
        private readonly WebShopProjectContext _context;
        private readonly DbContextOptions<WebShopProjectContext> _options;

        public categoryRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<WebShopProjectContext>()
                .UseInMemoryDatabase(databaseName: "WebShopProjectCategory")
                .Options;

            _context = new WebShopProjectContext(_options);

            _sut = new categoryRepository(_context);
        }

        [Fact]
        public async Task GetAll_ShouldReturnListOfcategorys_WhencategroysExists() 
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            _context.category.Add(new category
            {
                Id = 1,
                categoryName = "Computer"
            });
            _context.category.Add(new category
            {
                 Id = 2,
                 categoryName = "Screen"
            });
            await _context.SaveChangesAsync();

            // Act

            var result = await _sut.GetAll();

            // Assert

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<category>>(result);
        }

        [Fact]
        public async Task GetAll_ShouldReturnEmptyListOfcategorys_WhenNocategroysExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act

            var result = await _sut.GetAll();

            // Assert

            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<category>>(result);
        }

        [Fact]
        public async Task Create_ShouldAddIdTocategory_WhenSavingToDatabase()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int expectedId = 1;
            category category = new category
            {
                categoryName = "Computer"
            };

            // Act

            var result = await _sut.Create(category);

            // Asserts
            Assert.NotNull(result);
            Assert.IsType<category>(result);
            Assert.Equal(expectedId, result.Id);

        }

        [Fact]
        public async Task Create_ShouldFailToAddcategory_WhenAddingcategoryWithExistingId()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            category category = new category
            {
                Id = 1,
                categoryName = "Computer"
            };

            _context.category.Add(category);
            await _context.SaveChangesAsync();

            // Act
            Func<Task> action = async () => await _sut.Create(category);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async Task GetById_ShouldReturnThecategory_IfcategoryExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int categoryId = 1;
            _context.category.Add(new category
            {
                Id = categoryId,
                categoryName = "Computer"
            });
            await _context.SaveChangesAsync();

            // Act
            var result = await _sut.GetById(categoryId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<category>(result);
            Assert.Equal(categoryId, result.Id);
        }

        [Fact]
        public async Task GetById_ShouldReturnNull_IfcategoryDoesNotExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int categoryId = 1;

            // Act
            var result = await _sut.GetById(categoryId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Update_ShouldChangeValuesOncategory_WhencategoryExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int categoryId = 1;
            category category = new category
            {
                Id = categoryId,
                categoryName = "Computer"
            };
            _context.category.Add(category);
            await _context.SaveChangesAsync();

            category updatecategory = new category
            {
                Id = categoryId,
                categoryName = "screen"
            };

            // Act
            var result = await _sut.Update(categoryId, updatecategory);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<category>(result);
            Assert.Equal(categoryId, result.Id);
            Assert.Equal(updatecategory.categoryName, result.categoryName);
        }

        [Fact]
        public async Task Update_shouldReturnNull_WhencategoryDoesNotExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int categoryId = 1;
            category updatecategory = new category
            {
                Id = categoryId,
                categoryName = "Computer"
            };

            // Act
            var result = await _sut.Update(categoryId, updatecategory);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnDeletedcategory_WhencategoryIsDelete()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int categoryId = 1;
            category category = new category
            {
                Id = categoryId,
                categoryName = "Computer"
            };
            _context.category.Add(category);
            await _context.SaveChangesAsync();

            var result = await _sut.Delete(categoryId);
            var categorys = await _sut.GetAll();
        }

        [Fact]
        public async Task Delete_ShouldReturnNull_WhencategoryDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int categoryId = 1;

            // Act
            var result = await _sut.Delete(categoryId);

            // Assert
            Assert.Null(result);
        }
    }
}
