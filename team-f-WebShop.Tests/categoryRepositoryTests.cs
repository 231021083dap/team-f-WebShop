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
                .UseInMemoryDatabase(databaseName: "WebShopProject")
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
    }
}
