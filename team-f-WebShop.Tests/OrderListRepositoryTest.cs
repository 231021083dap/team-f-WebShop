using Microsoft.EntityFrameworkCore;
using OrangularAPI.Database;
using OrangularAPI.Database.Entities;
using OrangularAPI.Repositories.OrderListsRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace OrangularTests.OrderListsTest
{
    public class OrderListRepositoryTest
    {
        private readonly OrderListRepository _sut;
        private readonly OrangularProjectContext _context;
        private readonly DbContextOptions<OrangularProjectContext> _options;

        public OrderListRepositoryTest()
        {
            _options = new DbContextOptionsBuilder<OrangularProjectContext>()
                .UseInMemoryDatabase(databaseName: "OrangularProject")
                .Options;

            _context = new OrangularProjectContext(_options);

            _sut = new OrderListRepository(_context);
        }

        [Fact]
        public async Task GetAll_ShouldReturnListOfOrderList_WhenOrderListExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            _context.OrderList.Add(new OrderList
            {
                Id = 1,
                OrderDateTime = DateTime.Parse("2021-12-21 12:55:00"),
                User = new()

            });
            _context.OrderList.Add(new OrderList
            {
                Id = 2,
                OrderDateTime = DateTime.Parse("2020-12-21 12:57:00"),
                User = new()

            });
            await _context.SaveChangesAsync();
            int expectedSize = 2;

            // Act
            var result = await _sut.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<OrderList>>(result);
            Assert.Equal(expectedSize, result.Count);
        }

        [Fact]
        public async Task GetAll_ShouldReturnEmptyListOfOrder_Lists_WhenNoOrder_ListsExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            
            // Act
            var result = await _sut.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<OrderList>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetById_ShouldReturnTheOrder_Lists_IfOrder_ListsExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int OrderListId = 1;
            _context.OrderList.Add(new OrderList
            {
                Id = OrderListId,
                OrderDateTime = DateTime.Parse("2021-12-21 12:55:00")
            });
            await _context.SaveChangesAsync();

            // Act
            var result = await _sut.GetById(OrderListId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OrderList>(result);
            Assert.Equal(OrderListId, result.Id);
        }

        [Fact]
        public async Task GetById_ShouldReturnNull_IfOrder_ListsDoesNotExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int OrderListId = 1;

            // Act
            var result = await _sut.GetById(OrderListId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Create_ShouldAddIdToOrder_Lists_WhenSavingToDatabase()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int expectedId = 1;
            OrderList Order_Lists = new OrderList
            {
                OrderDateTime = DateTime.Parse("2021-12-21 12:55:00")
            };

            // act
            var result = await _sut.Create(Order_Lists);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OrderList>(result);
            Assert.Equal(expectedId, result.Id);
        }

        [Fact]
        public async Task Create_ShouldFailToAddOrder_Lists_WhenAddingOrder_ListsWithExistingId()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            OrderList Order_Lists = new OrderList
            {
                Id = 1,
                OrderDateTime = DateTime.Parse("2021-12-21 12:55:00")
            };

            _context.OrderList.Add(Order_Lists);
            await _context.SaveChangesAsync();

            // act
            Func<Task> action = async () => await _sut.Create(Order_Lists);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async Task Update_ShouldChangeValuesOnOrder_Lists_WhenOrder_ListsExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int OrderListId = 1;
            OrderList Order_Lists = new OrderList
            {
                Id = OrderListId,
                OrderDateTime = DateTime.Parse("2021-12-21 12:55:00")

            };
            _context.OrderList.Add(Order_Lists);
            await _context.SaveChangesAsync();

            OrderList updateOrder_Lists = new OrderList
            {
                Id = OrderListId,
                OrderDateTime = DateTime.Parse("2021-12-21 12:55:00"),

            };

            // Act
            var result = await _sut.Update(OrderListId, updateOrder_Lists);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OrderList>(result);
            Assert.Equal(OrderListId, result.Id);
            Assert.Equal(updateOrder_Lists.OrderDateTime, result.OrderDateTime);

        }

        [Fact]
        public async Task Update_ShouldReturnNull_WhenOrder_ListsDoesNotExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int OrderListId = 1;
            OrderList updateOrder_Lists = new OrderList
            {
                Id = OrderListId,
                OrderDateTime = DateTime.Parse("2021-12-21 12:55:00")
            };

            // Act
            var result = await _sut.Update(OrderListId, updateOrder_Lists);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnDeletedOrder_Lists_WhenOrder_ListsIsDeleted()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int OrderListId = 1;
            OrderList order_Lists = new OrderList
            {
                Id = OrderListId,
                OrderDateTime = DateTime.Parse("2021-12-21 12:55:00")

            };
            _context.OrderList.Add(order_Lists);
            await _context.SaveChangesAsync();

            var result = await _sut.Delete(OrderListId);
            var Order_Lists = await _sut.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OrderList>(result);
            Assert.Equal(OrderListId, result.Id);

            Assert.Empty(Order_Lists);
        }

        [Fact]
        public async Task Delete_ShouldReturnNull_WhenOrder_ListsDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int OrderListId = 1;

            // Act
            var result = await _sut.Delete(OrderListId);

            // Assert
            Assert.Null(result);
        }
    }
}
