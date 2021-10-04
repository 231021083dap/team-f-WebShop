using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using Orangular.Controllers;
using OrangularAPI.DTO.OrderItems.Requests;
using OrangularAPI.DTO.OrderItems.Responses;
using OrangularAPI.Services.OrderItemServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OrangularTests.OrderItemsTest
{
    public class OrderItemsControllerTests
    {
        private readonly OrderItemController _sut;
        private readonly Mock<IOrderItemService> _orderItemService = new();
        public OrderItemsControllerTests()
        {
            _sut = new OrderItemController(_orderItemService.Object);
        }
        // -----------------------------------------------------------------------------------------------------------------------
        // GetAll Tests
        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenDataExists()
        {
            // arrange
            List<OrderItemResponse> orderItem = new();
            orderItem.Add(new OrderItemResponse
            {
                Id = 1,
                Price = 1,
                Quantity = 1,
                OrderList = new OrderItemOrderListResponse { },
                Products = new OrderItemProductResponse { }
            });
            _orderItemService.Setup(s => s.GetAll()).ReturnsAsync(orderItem);
            // act
            var result = await _sut.GetAll();
            // assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }
        [Fact]
        public async void GetAll_ShouldReturnStatusCode204_WhenNoDataExists()
        {
            // Arrange 
            List<OrderItemResponse> orderItem = new();
            _orderItemService.Setup(s => s.GetAll()).ReturnsAsync(orderItem);

            // Act
            var result = await _sut.GetAll();

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);
        }
        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenNullIsReturnedFromService()
        {
            // Arrange 
            List<OrderItemResponse> book = new();
            _orderItemService.Setup(s => s.GetAll()).ReturnsAsync(() => null);

            // Act
            var result = await _sut.GetAll();

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
        // -----------------------------------------------------------------------------------------------------------------------
        // GetById Tests
        [Fact]
        public async void GetById_ShouldReturnStatusCode200_WhenDataExists()
        {
            // Arrange
            int orderItemId = 1;
            OrderItemResponse orderItem = new OrderItemResponse
            {
                OrderList = new OrderItemOrderListResponse { },
                Products = new OrderItemProductResponse { }
            };
            _orderItemService.Setup(s => s.GetById(It.IsAny<int>())).ReturnsAsync(orderItem);
            // Act
            var result = await _sut.GetById(orderItemId);
            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }
        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhenOrderItemDoesNotExist()
        {
            // Arrange
            int orderItemId = 1;
            _orderItemService.Setup(s => s.GetById(It.IsAny<int>())).ReturnsAsync(() => null);
            // Act
            var result = await _sut.GetById(orderItemId);
            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);
        }
        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenExeptionIsRaised()
        {
            // Arrange
            _orderItemService.Setup(s => s.GetById(It.IsAny<int>())).ReturnsAsync(() => throw new Exception("This is an exception"));
            // Act
            var result = await _sut.GetById(1);
            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
        // -----------------------------------------------------------------------------------------------------------------------
        // Create Tests
        [Fact]
        public async void Create_ShouldReturnStatusCode200_WhenDataIsCreated()
        {
            // Arrange
            NewOrderItem newOrderItem = new NewOrderItem
            {
                Price = 1,
                Quantity = 1,
                OrderListId = 1,
                ProductId = 1
            };
            OrderItemResponse orderItem = new OrderItemResponse
            {
                Id = 1,
                Price = 1,
                Quantity = 1,
                OrderList = new OrderItemOrderListResponse
                {
                },
                Products = new OrderItemProductResponse
                {
                }
            };
            _orderItemService.Setup(s => s.Create(It.IsAny<NewOrderItem>())).ReturnsAsync(orderItem);
            // Act
            var result = await _sut.Create(newOrderItem);
            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }
        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            NewOrderItem newOrderItem = new NewOrderItem
            {
                Price = 1,
                Quantity = 1,
                OrderListId = 1,
                ProductId = 1
            };
            _orderItemService.Setup(s => s.Create(It.IsAny<NewOrderItem>())).ReturnsAsync(() => throw new Exception("This is an exception :)"));
            // Act
            var result = await _sut.Create(newOrderItem);
            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
        // -----------------------------------------------------------------------------------------------------------------------
        // Update Tests
        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenDataIsSaved()
        {
            // Arrange
            int orderItemId = 1;
            UpdateOrderItem updateOrderItem = new UpdateOrderItem
            {
            };
            OrderItemResponse orderItem = new OrderItemResponse
            {
                OrderList = new OrderItemOrderListResponse { },
                Products = new OrderItemProductResponse { }
            };
            _orderItemService.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<UpdateOrderItem>())).ReturnsAsync(orderItem);
            // Acts
            var result = await _sut.Update(orderItemId, updateOrderItem);
            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }
        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int orderItemId = 1;
            UpdateOrderItem updateOrderItem = new UpdateOrderItem
            {
            };
            OrderItemResponse orderItem = new OrderItemResponse
            {
                OrderList = new OrderItemOrderListResponse { },
                Products = new OrderItemProductResponse { }
            };
            _orderItemService.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<UpdateOrderItem>())).ReturnsAsync(() => throw new Exception("This is an exception :)"));
            // Acts
            var result = await _sut.Update(orderItemId, updateOrderItem);
            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}
