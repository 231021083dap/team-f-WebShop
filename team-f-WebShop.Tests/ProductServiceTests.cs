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
    public class ProductServiceTests
    {
        
        private readonly ProductService _sut;
        private readonly Mock<IProductRepository> _productRepository = new();

        public ProductServiceTests()
        {
            _sut = new ProductService(_productRepository.Object);
        }



        // GET ALL

        [Fact]
        public async void GetAll_ShouldReturnListOfProductResponses_WhenProductsExist()
        {
            // Arrange
            List<Product> products = new();
            products.Add(new Product
            {
                ProductId = 1,
                Name = "GIGABYTE FI32U",
                Price = 8575,
                Quantity = 6,
                Description = "LED-skærm"
            });


            products.Add(new Product
            {
                ProductId = 2,
                Name = "GIGABYTE M28U",
                Price = 5999,
                Quantity = 13,
                Description = "3840 x 2160 (4K)"
            });


            _productRepository
                .Setup(a => a.GetAllProductRepository())
                .ReturnsAsync(products);

            // Act
            var result = await _sut.GetAllProductService();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<ProductResponse>>(result);
        }


        [Fact]
        public async void GetAll_ShouldReturnEmptyListOfProductResponses_WhenNoProductsExists()
        {
            // Arrange
            List<Product> products = new List<Product>();

            _productRepository
                .Setup(a => a.GetAllProductRepository())
                .ReturnsAsync(products);

            // Act
            var result = await _sut.GetAllProductService();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<ProductResponse>>(result);
        }


        // ______________________________________________________________________________

        // GET BY ID

        [Fact]
        public async void GetById_ShouldReturnAProduct_WhenProductExists()
        {
            // Arrange
            int productId = 1;

            Product product = new Product
            {
                ProductId = productId,
                Name = "GIGABYTE FI32U",
                Price = 8575,
                Quantity = 6,
                Description = "LED-skærm"

            };

            _productRepository
                .Setup(a => a.GetByIdProductRepository(It.IsAny<int>()))
                .ReturnsAsync(product);

            // Act
            var result = await _sut.GetByIdProductService(productId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProductResponse>(result);
            Assert.Equal(product.Name, result.Name);
            Assert.Equal(product.Price, result.Price);
            Assert.Equal(product.Quantity, result.Quantity);
            Assert.Equal(product.Description, result.Description);
        }



        [Fact]
        public async void GetById_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange
            int productId = 1;

            _productRepository
                .Setup(a => a.GetByIdProductRepository(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.GetByIdProductService(productId);

            // Assert
            Assert.Null(result);
        }



        // ______________________________________________________________________________

        // CREATE

        [Fact]
        public async void Create_ShouldReturnProductResponse_WhenCreateIsSuccess()
        {
            // Arrange
            NewProduct newProduct = new NewProduct
            {
                Name = "GIGABYTE FI32U",
                Price = 8575,
                Quantity = 6,
                Description = "LED-skærm"
            };

            int productId = 1;

            Product product = new Product
            {
                ProductId = productId,
                Name = "GIGABYTE FI32U",
                Price = 8575,
                Quantity = 6,
                Description = "LED-skærm"
            };

            _productRepository
                .Setup(a => a.CreateProductRepository(It.IsAny<Product>()))
                .ReturnsAsync(product);

            // Act
            var result = await _sut.CreateProductService(newProduct);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProductResponse>(result);
            Assert.Equal(productId, result.ProductId);
            Assert.Equal(newProduct.Name, result.Name);
            Assert.Equal(newProduct.Price, result.Price);
            Assert.Equal(newProduct.Quantity, result.Quantity);
            Assert.Equal(newProduct.Description, result.Description);

        }


        // ______________________________________________________________________________

        // UPDATE

        [Fact]
        public async void Update_ShouldReturnUpdatedProductResponse_WhenUpdateIsSuccess()
        {
            // Arrange
            UpdateProduct updateProduct = new UpdateProduct
            {
                Name = "GIGABYTE FI32U",
                Price = 8575,
                Quantity = 6,
                Description = "LED-skærm"
            };

            int productId = 1;

            ProductResponse productResponse = new ProductResponse
            {
                ProductId = productId,
                Name = "GIGABYTE FI32U",
                Price = 8575,
                Quantity = 6,
                Description = "LED-skærm"
            };

            Product product = new Product
            {
                ProductId = productId,
                Name = "GIGABYTE FI32U",
                Price = 8575,
                Quantity = 6,
                Description = "LED-skærm"
            };

            _productRepository
                .Setup(a => a.UpdateProductRepository(It.IsAny<int>(), It.IsAny<Product>()))
                .ReturnsAsync(product);

            // Act
            var result = await _sut.UpdateProductService(productId, updateProduct);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProductResponse>(result);
            Assert.Equal(productId, result.ProductId);
            Assert.Equal(productResponse.Name, result.Name);
            Assert.Equal(productResponse.Price, result.Price);
            Assert.Equal(productResponse.Quantity, result.Quantity);
            Assert.Equal(productResponse.Description, result.Description);
        }



        [Fact]
        public async void Update_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange
            UpdateProduct updateProduct = new UpdateProduct
            {
                Name = "GIGABYTE FI32U",
                Price = 8575,
                Quantity = 6,
                Description = "LED-skærm"
            };

            int productId = 1;

            _productRepository
                .Setup(a => a.UpdateProductRepository(It.IsAny<int>(), It.IsAny<Product>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.UpdateProductService(productId, updateProduct);

            // Assert
            Assert.Null(result);
        }


        // _____________________________________________________________________________

        // DELETE

        [Fact]
        public async void Delete_ShouldReturnTrue_WhenProductDeleteIsSuccess()
        {
            // Arrange
            int productId = 1;
            Product product = new Product
            {
                ProductId = productId,
                Name = "GIGABYTE FI32U",
                Price = 8575,
                Quantity = 6,
                Description = "LED-skærm"
            };

            _productRepository
                .Setup(a => a.DeleteProductRepository(It.IsAny<int>()))
                .ReturnsAsync(product);


            // Act
            var result = await _sut.DeleteProductService(productId);


            // Assert
            Assert.True(result);
        }
    }
}
