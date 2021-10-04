using Moq;
using OrangularAPI.Database.Entities;
using OrangularAPI.DTO.OrderItems.Requests;
using OrangularAPI.DTO.OrderItems.Responses;
using OrangularAPI.Repositories.OrderItemsRepository;
using OrangularAPI.Repositories.OrderListsRepository;
using OrangularAPI.Repositories.ProductsRepository;
using OrangularAPI.Services.OrderItemServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OrangularTests.OrderItemsTest
{
    public class OrderItemsServiceTests
    {
        private readonly OrderItemService _sut;
        private readonly Mock<IOrderItemRepository> _orderItemsRepository = new();
        private readonly Mock<IOrderListRepository> _order_ListsRepository = new();
        private readonly Mock<IProductRepository> _productsRepository = new();
        public OrderItemsServiceTests()
        {
            _sut = new OrderItemService(_orderItemsRepository.Object, _order_ListsRepository.Object, _productsRepository.Object);
        }
        // -----------------------------------------------------------------------------------------------------------------------
        // GetAll Tests
        [Fact]
        public async void GetAll_ShouldReturnListofOrderItems_WhenOrderItemsExists()
        {
            // Arrange
            List<OrderItem> orderItem = new();
            orderItem.Add(new OrderItem
            {
                Id = 1,
                Price = 1,
                Quantity = 1,
                OrderListId = 1,
                OrderList = new OrderList
                {
                    Id = 1,
                    OrderDateTime = DateTime.Now
                },
                ProductId = 1,
                Product = new Product
                {
                    Id = 1,
                    BreedName = "Corgi",
                    Price = 1,
                    Weight = 1,
                    Gender = "F",
                    Description = "test123"
                }
            });
            orderItem.Add(new OrderItem
            {
                Id = 2,
                Price = 1,
                Quantity = 1,
                OrderListId = 2,
                OrderList = new OrderList
                {
                    Id = 2,
                    OrderDateTime = DateTime.Now
                },
                ProductId = 2,
                Product = new Product
                {
                    Id = 2,
                    BreedName = "Corgi",
                    Price = 1,
                    Weight = 1,
                    Gender = "F",
                    Description = "test123"
                }
            });
            _orderItemsRepository.Setup(a => a.GetAll()).ReturnsAsync(orderItem);
            // Act
            var result = await _sut.GetAll();
            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<OrderItemResponse>>(result);
        }
        [Fact]
        public async Task GetAll_ShouldReturnEmptyListOfOrderItemsResponse_WhenNoOrderItemsExists()
        {
            // Arrange
            List<OrderItem> orderItems = new();
            _orderItemsRepository.Setup(a => a.GetAll()).ReturnsAsync(orderItems);
            // Act
            var result = await _sut.GetAll();
            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<OrderItemResponse>>(result);
        }
        // -----------------------------------------------------------------------------------------------------------------------
        // GetById Tests
        [Fact]
        public async Task GetById_ShouldReturnOrderItemsResponse_WhenOrderItemExists()
        {
            // Arrange
            int orderItemId = 1;
            OrderItem orderItem = new OrderItem
            {
                Id = orderItemId,
                Price = 1,
                Quantity = 1,
                OrderListId = 1,
                OrderList = new OrderList
                {
                    Id = 1,
                    OrderDateTime = DateTime.Now
                },
                ProductId = 1,
                Product = new Product
                {
                    Id = 1,
                    BreedName = "Corgi",
                    Price = 1,
                    Weight = 1,
                    Gender = "F",
                    Description = "test123"
                }
            };
            _orderItemsRepository.Setup(a => a.GetById(It.IsAny<int>())).ReturnsAsync(orderItem);
            // Act
            var result = await _sut.GetById(orderItemId);
            // Assert
            Assert.NotNull(result);
            Assert.IsType<OrderItemResponse>(result);
            Assert.Equal(orderItem.Id, result.OrderList.OrderListId);
            //Assert.Equal(orderItem.Id, result.OrderList.Id);
            Assert.Equal(orderItem.ProductId, result.Products.ProductId);
        }
        [Fact]
        public async Task GetById_ShouldReturnNull_WhenOrderItemDoesNotExist()
        {
            // Arrange
            int orderItemId = 1;
            _orderItemsRepository.Setup(a => a.GetById(It.IsAny<int>())).ReturnsAsync(() => null);
            // Act
            var result = await _sut.GetById(orderItemId);
            // Assert
            Assert.Null(result);
        }
        // -----------------------------------------------------------------------------------------------------------------------
        // Create Tests
        [Fact]
        public async Task Create_ShouldReturnOrderItemResponse_WhenCreateIsSuccess()
        {
            // Arrange
            NewOrderItem newOrderItem = new NewOrderItem
            {
                Price = 1,
                Quantity = 1,
                OrderListId = 1,
                ProductId = 1
            };
            int orderItemId = 1;
            OrderItem orderItem = new OrderItem
            {
                Id = orderItemId,
                Price = 1,
                Quantity = 1,
                OrderList = new OrderList
                {
                    Id = 1,
                    OrderDateTime = DateTime.Now
                },
                Product = new Product
                {
                    Id = 1,
                    BreedName = "vovse",
                    Price = 1,
                    Weight = 1,
                    Gender = "F",
                    Description = "Test123"
                }
            };
            _orderItemsRepository.Setup(a => a.Create(It.IsAny<OrderItem>())).ReturnsAsync(orderItem);
            // Act
            var result = await _sut.Create(newOrderItem);
            // Assert
            Assert.NotNull(result);
            Assert.IsType<OrderItemResponse>(result);
            Assert.Equal(orderItemId, result.Id);
            Assert.Equal(newOrderItem.Price, result.Price);
            Assert.Equal(newOrderItem.Quantity, result.Quantity);
            //Assert.Equal(newOrderItem., result.OrderList.Id);
            //Assert.Equal(newOrderItem.ProductId, result.Product.ProductId);
        }
        [Fact]
        public async Task Create_ShouldReturnNull_WhenCreatedOrderItemIsNull()
        {
            // Arrange
            NewOrderItem newOrderItem = new NewOrderItem
            {
                Price = 1,
                Quantity = 1,
                OrderListId = 1,
                ProductId = 1
            };
            _orderItemsRepository.Setup(a => a.Create(It.IsAny<OrderItem>())).ReturnsAsync(() => null);
            // Act
            var result = await _sut.Create(newOrderItem);
            // Assert
            Assert.Null(result);
        }
        // -----------------------------------------------------------------------------------------------------------------------
        // Create Tests
        [Fact]
        public async void Update_ShouldReturnUpdatedOrderItemResponse_WhenUpdateIsSuccess()
        {
            // Arrange
            int orderItemId = 1;
            UpdateOrderItem updateOrderItem = new UpdateOrderItem
            {
                Price = 1,
                Quantity = 1,
                OrderlistId = 1,
                ProductId = 1
            };
            OrderItem orderItem = new OrderItem
            {
                Id = 1,
                Price = 1,
                Quantity = 1,
                OrderList = new OrderList
                {
                    Id = 1,
                    OrderDateTime = DateTime.Now
                },
                Product = new Product
                {
                    Id = 1,
                    BreedName = "vovse",
                    Price = 1,
                    Weight = 1,
                    Gender = "F",
                    Description = "Test123"
                }
            };
            _orderItemsRepository.Setup(a => a.Update(It.IsAny<int>(), It.IsAny<OrderItem>())).ReturnsAsync(orderItem);
            // Act
            var result = await _sut.Update(orderItemId, updateOrderItem);
            // Assert
            Assert.NotNull(result);
            Assert.IsType<OrderItemResponse>(result);
            Assert.Equal(orderItemId, result.Id); 
            Assert.Equal(updateOrderItem.Price, result.Price);
            Assert.Equal(updateOrderItem.Quantity, result.Quantity);
            //Assert.Equal(updateOrderItem.OrderlistId, result.OrderList.Id);
            //Assert.Equal(updateOrderItem.ProductId, result.OrderList.Id);
        }
        [Fact]
        public async void Update_ShouldReturnNull_WhenOrderItemDoesNotExist()
        {
            int orderItemId = 1;
            // Arrange
            UpdateOrderItem updateOrderItem = new UpdateOrderItem
            {
                Price = 1,
                Quantity = 1,
                OrderlistId = 1,
                ProductId = 1
            };
            _orderItemsRepository.Setup(a => a.Update(It.IsAny<int>(), It.IsAny<OrderItem>())).ReturnsAsync(() => null);
            // Act
            var result = await _sut.Update(orderItemId, updateOrderItem);
            // Assert
            Assert.Null(result);
        }
    }
}


