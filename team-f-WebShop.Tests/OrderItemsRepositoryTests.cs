using OrangularAPI.Database;
using OrangularAPI.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using OrangularAPI.Repositories.OrderItemsRepository;
using OrangularAPI.Helpers;

namespace OrangularTests.OrderItemsTest
{
    public class OrderItemsRepositoryTests
    {
        private readonly OrderItemRepository _sut;
        private readonly OrangularProjectContext _context;
        private readonly DbContextOptions<OrangularProjectContext> _options;
        public OrderItemsRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<OrangularProjectContext>()
                .UseInMemoryDatabase(databaseName: "OrangularOrderItemsDatabase")
                .Options;
            _context = new OrangularProjectContext(_options);
            _sut = new OrderItemRepository(_context);
        }
        // -----------------------------------------------------------------------------------------------------------------------
        // GetAll Tests
        [Fact]
        public async Task GetAll_ShouldReturnListOfOrderItem_WhenOrderItemExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            
            _context.OrderItem.Add(new OrderItem
            {
                Id = 1,
                Price = 750000,     // F.eks to hunde købt til 7500 kr stykket
                Quantity = 2,
                OrderList = new OrderList
                {
                    Id = 1,
                    OrderDateTime = DateTime.Now,
                    User = new User
                    {
                        Id = 1,
                        Email = "admin@admins.com",
                        Password = "Passw0rd",
                        Role = Role.Admin,
                    }
                },
                Product = new Product
                {
                    Id = 1,
                    BreedName = "chefer hund",
                    Price = 750000,
                    Weight = 35000,
                    Gender = "male",
                    Description = "Description",
                    Category = new Category
                    {
                        Id = 1,
                        CategoryName = "hund"
                    }
                }
            });

            await _context.SaveChangesAsync();

            // Act
            var result = await _sut.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<OrderItem>>(result);
        }


        [Fact]
        public async Task GetAll_ShouldReturnEmptyListOfOrderItem_WhenNoOrderItemExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            // Act
            var result = await _sut.GetAll();
            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<OrderItem>>(result);
            Assert.Empty(result);
        }
        // -----------------------------------------------------------------------------------------------------------------------
        // ById Tests
        [Fact]
        public async Task GetById_ShouldReturnOrderItem_IfOrderItemExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int orderItemId = 1;
            _context.OrderItem.Add(new OrderItem
            {
                Id = 1,
                Price = 750000,     // F.eks to hunde købt til 7500 kr stykket
                Quantity = 2,
                OrderList = new OrderList
                {
                    Id = 1,
                    OrderDateTime = DateTime.Now,
                    User = new User
                    {
                        Id = 1,
                        Email = "admin@admins.com",
                        Password = "Passw0rd",
                        Role = Role.Admin,
                    }
                },
                Product = new Product
                {
                    Id = 1,
                    BreedName = "chefer hund",
                    Price = 750000,
                    Weight = 35000,
                    Gender = "male",
                    Description = "Description",
                    Category = new Category
                    {
                        Id = 1,
                        CategoryName = "hund"
                    }
                }
            });
            await _context.SaveChangesAsync();
            // Act
            var result = await _sut.GetById(orderItemId);
            // Assert
            Assert.NotNull(result);
            Assert.IsType<OrderItem>(result);
            Assert.Equal(orderItemId, result.Id);
        }


        [Fact]
        public async Task GetById_ShouldReturnNull_IfOrderItemDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int orderItemId = 1;
            // Act
            var result = await _sut.GetById(orderItemId);
            // Assert
            Assert.Null(result);
        }
        // -----------------------------------------------------------------------------------------------------------------------
        // Create Tests
        [Fact]
        public async Task Create_ShouldAddIdToOrderItem_WhenSavingToDatabase()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int expectedId = 1;
            OrderItem orderItem = new()
            {
                Id = 1,
                Price = 750000,     // F.eks to hunde købt til 7500 kr stykket
                Quantity = 2,
                OrderList = new OrderList
                {
                    Id = 1,
                    OrderDateTime = DateTime.Now,
                    User = new User
                    {
                        Id = 1,
                        Email = "admin@admins.com",
                        Password = "Passw0rd",
                        Role = Role.Admin,
                    }
                },
                Product = new Product
                {
                    Id = 1,
                    BreedName = "chefer hund",
                    Price = 750000,
                    Weight = 35000,
                    Gender = "male",
                    Description = "Description",
                    Category = new Category
                    {
                        Id = 1,
                        CategoryName = "hund"
                    }
                }
            };


            // Act
            var result = await _sut.Create(orderItem);
            // Assert

            Assert.NotNull(result);
            Assert.IsType<OrderItem>(result);
            Assert.Equal(expectedId, result.Id);
        }
        [Fact]
        public async Task Create_ShouldFailToAddOrderItem_whenAddingOrderItemWithExistingId()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            OrderItem orderItem = new()
            {
                Id = 1,
                Price = 1,
                Quantity = 1,
                OrderList = new OrderList
                {},
                Product = new Product
                {}
            };
            _context.OrderItem.Add(orderItem);
            await _context.SaveChangesAsync();
            // Act
            Func<Task> action = async () => await _sut.Create(orderItem);
            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }
        // -----------------------------------------------------------------------------------------------------------------------
        // Update Tests
        [Fact]
        public async Task Update_ShouldChangeValuesOnOrderItems_WhenOrderItemsExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int orderItemId = 1;
            OrderItem orderItem = new OrderItem
            {
                Id = orderItemId,
                Price = 1,
                Quantity = 1,
                // OrderListIdxxx = 1,
                // ProductId = 1
            };
            _context.OrderItem.Add(orderItem);
            await _context.SaveChangesAsync();
            OrderItem updateOrderItem = new OrderItem
            {
                Id = orderItemId,
                Price = 2,
                Quantity = 2,
                // OrderListIdxxx = 2,
                // ProductId = 2
            };
            // Act
            var result = await _sut.Update(orderItemId, updateOrderItem);
            // Assert
            Assert.NotNull(result);
            Assert.IsType<OrderItem>(result);
            Assert.Equal(orderItemId, result.Id);
            Assert.Equal(updateOrderItem.Price, result.Price);
            Assert.Equal(updateOrderItem.Quantity, result.Quantity);
            // Assert.Equal(updateOrderItem.OrderListIdxxx, result.OrderListIdxxx);
            // Assert.Equal(updateOrderItem.ProductId, result.ProductId);
        }
        [Fact]
        public async Task Update_ShouldNotChangeValues_WhereNoValuesWasPut()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int orderItemId = 1;
            int orderItemPrice = 1;
            int orderItemQuantity = 1;
            int orderItemOrderListsId = 1;
            int orderItemProductsId = 1;
            OrderItem orderItem = new OrderItem // En eksisterende orderItem i vores database
            {
                Id = orderItemId,
                Price = orderItemPrice,
                Quantity = orderItemQuantity,
                // OrderListIdxxx = orderItemOrderListsId,
                // ProductId = orderItemProductsId
            };
            _context.OrderItem.Add(orderItem);
            await _context.SaveChangesAsync();
            OrderItem updateOrderItem = new OrderItem // Prøver at opdatere uden at skrive inputs
            {
                Id = orderItemId
                // Når der ingen inputs er, laves et int til 0
            };
            // Act
            var result = await _sut.Update(orderItemId, updateOrderItem); 
            // Assert
            Assert.NotNull(result);
            Assert.IsType<OrderItem>(result);
            // Et if statement tjkker om den ønskede int opdatering er 0, hvis det er tilfældet skippes opdateringen af int.
            Assert.Equal(orderItemId, result.Id);
            Assert.Equal(orderItemPrice, result.Price);
            Assert.Equal(orderItem.Quantity, result.Quantity);
            // Assert.Equal(orderItem.OrderListIdxxx, result.OrderListIdxxx);
            // Assert.Equal(orderItem.ProductId, result.ProductId);
        }
        [Fact]
        public async Task Update_ShouldReturnNull_WhenOrderItemDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int orderItemId = 1;
            OrderItem updateOrderItem = new OrderItem{};
            // Act
            var result = await _sut.Update(orderItemId, updateOrderItem);
            // Assert
            Assert.Null(result);
        }
    }
}
