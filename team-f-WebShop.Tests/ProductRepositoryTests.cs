using Microsoft.EntityFrameworkCore;
using Moq;
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
    public class ProductRepositoryTests
    {
        private readonly ProductRepository _sut;
        private readonly WebShopProjectContext _context;
        private readonly DbContextOptions<WebShopProjectContext> _options;

        public ProductRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<WebShopProjectContext>()
                .UseInMemoryDatabase(databaseName: "WebShopProject")
                .Options;

            _context = new WebShopProjectContext(_options);

            _sut = new ProductRepository(_context);
        }


        [Fact]
        public async Task GetAll_ShouldReturnListOfProducts_WhenProductsExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync(); 

            _context.Product.Add(new Product
            {
                ProductId = 1,
                Name = "GIGABYTE FI32U",
                Price = 8575,
                Quantity = 6,
                Desciption = "LED-skærm"
            });


            _context.Product.Add(new Product
            {
                ProductId = 2,
                Name = "GIGABYTE M28U",
                Price = 5999,
                Quantity = 13,
                Desciption = "3840 x 2160 (4K)"
            });


            await _context.SaveChangesAsync();

            int expectedSize = 2;

            // Act
            var result = await _sut.GetAllProductRepository();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Product>>(result);
            Assert.Equal(expectedSize, result.Count);
        }


        [Fact]
        public async Task GetAll_ShouldReturnEmptyListOfProducts_WhenNoProductsExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _sut.GetAllProductRepository();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Product>>(result);
            Assert.Empty(result);
        }




    }
}
