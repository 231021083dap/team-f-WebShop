using OrangularAPI.Controllers;
using OrangularAPI.DTO.OrderLists.Requests;
using OrangularAPI.DTO.OrderLists.Responses;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using OrangularAPI.Services.OrderListServices;
using OrangularAPI.Database.Entities;
using OrangularAPI.Helpers;

namespace OrangularTests.OrderListsTest
{
    public class OrderListControllerTests
    {
        private readonly OrderListController _sut;
        private readonly Mock<IOrderListService> _orderListService = new();

        public OrderListControllerTests()
        {
            _sut = new OrderListController(_orderListService.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_whenDataExists()
        {
            // Arrange
            List<OrderListResponse> orderList = new();
            orderList.Add(new OrderListResponse
            {
                Id = 1,
                OrderDateTime = DateTime.Parse("2021-12-21 12:55:00"),
                User = new OrderListUserResponse {}
            });
            orderList.Add(new OrderListResponse
            {
                Id = 2,
                OrderDateTime = DateTime.Parse("2021-12-21 12:55:00"),
                User = new OrderListUserResponse { }

            });

            _orderListService
                .Setup(s => s.GetAll())
                .ReturnsAsync(orderList);
            // Act
            var result = await _sut.GetAll();

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);

        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode204_whenNoDataExists()
        {
            // Arrange
            List<OrderListResponse> order_Listss = new();

            _orderListService
                .Setup(s => s.GetAll())
                .ReturnsAsync(order_Listss);

            // Act
            var result = await _sut.GetAll();

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);

        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_whenNullIsReturnedFromService()
        {
            // Arrange
            _orderListService
                .Setup(s => s.GetAll())
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.GetAll();

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);

        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_whenExceptionIsRaised()
        {
            // Arrange
            _orderListService
                .Setup(s => s.GetAll())
                .ReturnsAsync(() => throw new Exception("This is an exception"));

            // Act
            var result = await _sut.GetAll();

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);

        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode200_WhenDataExists()
        {
            // Arrange
            int OrderListId = 1;
            OrderListResponse order_Lists = new OrderListResponse
            {
                Id = OrderListId,
                OrderDateTime = DateTime.Parse("2021-12-21 12:55:00")
            };

            _orderListService
                .Setup(s => s.GetById(It.IsAny<int>()))
                .ReturnsAsync(order_Lists);

            // Act
            var result = await _sut.GetById(OrderListId);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode404_Whenorder_ListsDoesNotExist()
        {
            // Arrange
            int OrderListId = 1;

            _orderListService
                .Setup(s => s.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);
            // Act
            var result = await _sut.GetById(OrderListId);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            _orderListService
                .Setup(s => s.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            // Act
            var result = await _sut.GetById(1);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode200_WhenDataIsCreated()
        {
            // Arrange
            int OrderListId = 1;
            NewOrderList NewOrder_Lists = new NewOrderList
            {
                OrderDateTime = DateTime.Parse("2021-12-21 12:55:00")
            };

            OrderListResponse order_Lists = new OrderListResponse
            {
                Id = OrderListId,
                OrderDateTime = DateTime.Parse("2021-12-21 12:55:00")
            };

            _orderListService
                .Setup(s => s.Create(It.IsAny<NewOrderList>()))
                .ReturnsAsync(order_Lists);

            // Act
            var result = await _sut.Create(NewOrder_Lists);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            NewOrderList NewOrder_Lists = new NewOrderList
            {
                OrderDateTime = DateTime.Parse("2021-12-21 12:55:00")
            };

            _orderListService
                .Setup(s => s.Create(It.IsAny<NewOrderList>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            // Act
            var result = await _sut.Create(NewOrder_Lists);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenDataIsSaved()
        {
            // Arrange
            int OrderListId = 1;
            UpdateOrderList updateorder_Lists = new UpdateOrderList
            {
                OrderDateTime = DateTime.Parse("2021-12-21 12:55:00")
            };

            OrderListResponse order_Lists = new OrderListResponse
            {
                Id = OrderListId,
                OrderDateTime = DateTime.Parse("2021-12-21 12:55:00")
            };

            _orderListService
                .Setup(s => s.Update(It.IsAny<int>(), It.IsAny<UpdateOrderList>()))
                .ReturnsAsync(order_Lists);

            // Act
            var result = await _sut.Update(OrderListId, updateorder_Lists);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int OrderListId = 1;
            UpdateOrderList updateorder_Lists = new UpdateOrderList
            {
                OrderDateTime = DateTime.Parse("2021-12-21 12:55:00")
            };

            _orderListService
                .Setup(s => s.Update(It.IsAny<int>(), It.IsAny<UpdateOrderList>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            // Act
            var result = await _sut.Update(OrderListId, updateorder_Lists);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode204_Whenorder_ListsIsDeleted()
        {
            // Arrange
            int OrderListId = 1;

            _orderListService
                .Setup(s => s.Delete(It.IsAny<int>()))
                .ReturnsAsync(true);

            // Act
            var result = await _sut.Delete(OrderListId);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int OrderListId = 1;

            _orderListService
                .Setup(s => s.Delete(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            // Act
            var result = await _sut.Delete(OrderListId);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}
