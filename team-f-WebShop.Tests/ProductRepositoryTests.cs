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
        private DbContextOptions<WebShopProjectContext> _options;

        public ProductRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<WebShopProjectContext>()
                .UseInMemoryDatabase(databaseName: "WebShopProjectProduct")
                .Options;

            _context = new WebShopProjectContext(_options);

            _sut = new ProductRepository(_context);
        }



        // GET ALL

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
                Description = "LED-skærm"
            });


            _context.Product.Add(new Product
            {
                ProductId = 2,
                Name = "GIGABYTE M28U",
                Price = 5999,
                Quantity = 13,
                Description = "3840 x 2160 (4K)"
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


        //________________________________________________________________________________________

        // GET BY ID

        [Fact]
        public async Task GeById_ShouldReturnTheProduct_IfProductExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int productId = 1;
            _context.Product.Add(new Product
            {
                ProductId = productId,
                Name = "GIGABYTE FI32U",
                Price = 8575,
                Quantity = 6,
                Description = "LED-skærm"
            });
            await _context.SaveChangesAsync();

            // Act
            var result = await _sut.GetByIdProductRepository(productId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(productId, result.ProductId);
        }


        [Fact]
        public async Task GeById_ShouldReturnNull_IfProductDoesNotExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int productId = 1;


            // Act
            var result = await _sut.GetByIdProductRepository(productId);

            // Assert
            Assert.Null(result);
        }


        //________________________________________________________________________________________

        // CREATE

        [Fact]
        public async Task Create_ShouldAddIdToProduct_WhenSavingToDatabase()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int expectedId = 1;
            Product product = new Product
            {
                Name = "GIGABYTE FI32U",
                Price = 8575,
                Quantity = 6,
                Description = "LED-skærm"
            };


            // Act
            var result = await _sut.CreateProductRepository(product);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(expectedId, result.ProductId);
        }


        [Fact]
        public async Task Create_ShouldFailToAddProduct_WhenAddingProductWithExistingId()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            Product product = new Product
            {
                ProductId = 6,
                Name = "GIGABYTE",
                Price = 8375,
                Quantity = 68,
                Description = "skærm"
            };

            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            // Act
            Func<Task> action = async () => await _sut.CreateProductRepository(product);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }


        // ________________________________________________________________________________________

        // UPDATE

        [Fact]
        public async Task Update_ShouldChangeValuesOnProduct_WhenProductExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int productId = 1;
            Product product = new Product
            {
                ProductId = productId,
                Name = "GIGABYTE FI32U",
                Price = 8575,
                Quantity = 6,
                Description = "LED-skærm"
            };

            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            Product updateProduct = new Product
            {
                ProductId = productId,
                Name = "GIGABYTE",
                Price = 857,
                Quantity = 62,
                Description = "skærm"
            };


            // Act
            var result = await _sut.UpdateProductRepository(productId, updateProduct);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(productId, result.ProductId);
            Assert.Equal(updateProduct.Name, result.Name);
            Assert.Equal(updateProduct.Price, result.Price);
            Assert.Equal(updateProduct.Quantity, result.Quantity);
            Assert.Equal(updateProduct.Description, result.Description);

        }


        [Fact]
        public async Task Update_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int productId = 1;
            Product updateProduct = new Product
            {
                ProductId = productId,
                Name = "GIGABYTE FI32U",
                Price = 8575,
                Quantity = 6,
                Description = "LED-skærm"
            };


            // Act
            var result = await _sut.UpdateProductRepository(productId, updateProduct);

            // Assert
            Assert.Null(result);

        }


        // ________________________________________________________________________________________

        // DELETE

        [Fact]
        public async Task Delete_ShouldReturnDeletedProduct_WhenProductIsDeleted()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int productId = 1;
            Product product = new Product
            {
                ProductId = productId,
                Name = "GIGABYTE FI32U",
                Price = 8575,
                Quantity = 6,
                Description = "LED-skærm"
            };

            _context.Product.Add(product);
            await _context.SaveChangesAsync();


            // Act
            var result = await _sut.DeleteProductRepository(productId);
            var products = await _sut.GetAllProductRepository();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(productId, result.ProductId);
            Assert.Empty(products);
        }


        [Fact]
        public async Task Delete_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int productId = 1;


            // Act
            var result = await _sut.DeleteProductRepository(productId);


            // Assert
            Assert.Null(result);
        }
    }
}
