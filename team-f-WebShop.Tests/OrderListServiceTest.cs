using Moq;
using OrangularAPI.Database.Entities;
using OrangularAPI.DTO.OrderLists.Requests;
using OrangularAPI.DTO.OrderLists.Responses;
using OrangularAPI.Repositories.OrderListsRepository;
using OrangularAPI.Repositories.Users;
using OrangularAPI.Services.OrderListServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OrangularTests.OrderListsTest
{


    public class OrderListServiceTests
    {

        private readonly OrderListService _sut;
        private readonly Mock<IOrderListRepository> _orderListsRepository = new();
        private readonly Mock<IUserRepository> _userRepository = new();

        public OrderListServiceTests()
        {
            _sut = new OrderListService(_orderListsRepository.Object, _userRepository.Object);
        }
        [Fact]
        public async void GetAll_ShouldReturnListOfOrderListResponses_WhenOrderListExist()
        {
            // Arrange
            List<OrderList> orderList = new();
            orderList.Add(new OrderList
            {
                Id = 1,
                OrderDateTime = DateTime.Parse("2021-12-21 12:55:00"),
                User = new()

            });

            orderList.Add(new OrderList
            {
                Id = 2,
                OrderDateTime = DateTime.Parse("2021-12-23 12:55:00"),
                User = new()

            });

            _orderListsRepository
                .Setup(a => a.GetAll())
                .ReturnsAsync(orderList);

            // Act
            var result = await _sut.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<OrderListResponse>>(result);
        }

        [Fact]
        public async void GetAll_ShouldReturnEmptyListOfOrderListResponse_WhenNoOrderListExists()
        {
            // Arrange
            List<OrderList> orderList = new();

            _orderListsRepository
                .Setup(a => a.GetAll())
                .ReturnsAsync(orderList);

            // Act
            var result = await _sut.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<OrderListResponse>>(result);
        }

        [Fact]
        public async void GetById_ShouldReturnAnOrderListResponse_WhenOrderListExists()
        {
            // Arrange
            int orderListId = 1;

            OrderList orderList = new OrderList
            {
                Id = orderListId,
                OrderDateTime = DateTime.Parse("2020-12-21 12:57:00")
            };

            _orderListsRepository
                .Setup(a => a.GetById(It.IsAny<int>()))
                .ReturnsAsync(orderList);

            // Act
            var result = await _sut.GetById(orderListId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OrderListResponse>(result);
            Assert.Equal(orderList.Id, result.Id);
            Assert.Equal(orderList.OrderDateTime, result.OrderDateTime);
        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhenOrderListDoesNotExist()
        {
            // Arrange
            int OrderListId = 1;

            _orderListsRepository
                .Setup(a => a.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.GetById(OrderListId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Create_ShouldReturnOrderListResponse_WhenCreateIsSuccess()
        {
            // Arrange
            NewOrderList newOrderList = new NewOrderList
            {
                OrderDateTime = DateTime.Parse("2020-12-21 12:57:00")
            };

            int OrderListId = 1;

            OrderList orderList = new OrderList
            {
                Id = OrderListId,
                OrderDateTime = DateTime.Parse("2020-12-21 12:57:00")
            };

            _orderListsRepository
                .Setup(a => a.Create(It.IsAny<OrderList>()))
                .ReturnsAsync(orderList);

            // Act
            var result = await _sut.Create(newOrderList);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OrderListResponse>(result);
            Assert.Equal(orderList.Id, result.Id);
            Assert.Equal(orderList.OrderDateTime, result.OrderDateTime);

        }

        [Fact]
        public async void Update_ShouldReturnUpdatedOrderListResponse_WhenUpdateIsSuccess()
        {
            // Arrange
            UpdateOrderList updateOrderList = new UpdateOrderList
            {
                OrderDateTime = DateTime.Parse("2020-12-21 12:57:00")
            };

            int OrderListId = 1;

            OrderListResponse orderListResponse = new OrderListResponse
            {
                Id = OrderListId,
                OrderDateTime = DateTime.Parse("2020-12-21 12:57:00")
            };

            OrderList orderList = new OrderList
            {
                Id = OrderListId,
                OrderDateTime = DateTime.Parse("2020-12-21 12:57:00")
            };

            _orderListsRepository
                .Setup(a => a.Update(It.IsAny<int>(), It.IsAny<OrderList>()))
                .ReturnsAsync(orderList);

            // Act
            var result = await _sut.Update(OrderListId, updateOrderList);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OrderListResponse>(result);
            Assert.Equal(orderList.Id, result.Id);
            Assert.Equal(orderList.OrderDateTime, result.OrderDateTime);
        }

        [Fact]
        public async void Update_ShouldReturnNull_WhenOrderListDoesNotExist()
        {
            // Arrange
            UpdateOrderList updateOrderList = new UpdateOrderList
            {
                OrderDateTime = DateTime.Parse("2020-12-21 12:57:00")
            };

            int OrderListId = 1;

            _orderListsRepository
                .Setup(a => a.Update(It.IsAny<int>(), It.IsAny<OrderList>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.Update(OrderListId, updateOrderList);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Delete_ShouldReturnTrue_WhenDeleteIsSuccess()
        {
            // Arrange
            int OrderListId = 1;

            OrderList orderList = new OrderList
            {
                Id = OrderListId,
                OrderDateTime = DateTime.Parse("2020-12-21 12:57:00")
            };

            _orderListsRepository
                .Setup(a => a.Delete(It.IsAny<int>()))
                .ReturnsAsync(orderList);

            // Act
            var result = await _sut.Delete(OrderListId);

            // Assert
            Assert.True(result);
        }
    }

}
