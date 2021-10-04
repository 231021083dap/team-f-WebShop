
using OrangularAPI.Database.Entities;
using OrangularAPI.DTO.OrderItems.Requests;
using OrangularAPI.DTO.OrderItems.Responses;
using OrangularAPI.Repositories.OrderItemsRepository;
using OrangularAPI.Repositories.OrderListsRepository;
using OrangularAPI.Repositories.ProductsRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrangularAPI.Services.OrderItemServices
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IOrderListRepository _orderListRepository;
        private readonly IProductRepository _productRepository;

        public OrderItemService(
            IOrderItemRepository orderItemRepository, 
            IOrderListRepository orderListRepository, 
            IProductRepository productRepository
            )
        {
            _orderItemRepository = orderItemRepository;
            _orderListRepository = orderListRepository;
            _productRepository = productRepository;
        }
        public async Task<List<OrderItemResponse>> GetAll()
        {
            List<OrderItem> orderItems = await _orderItemRepository.GetAll();
            return orderItems.Select(o => new OrderItemResponse
            {
                Id = o.Id,
                Price = o.Price,
                Quantity = o.Quantity
                //,OrderItemOrderListResponse = new OrderItemOrderListResponse
                //{
                //    OrderListId = o.OrderList.Id,
                //    OrderDateTime = o.OrderList.OrderDateTime
                //},
                //OrderItemProductResponse = new OrderItemProductResponse
                //{
                //    ProductId = o.Product.Id,
                //    BreedName = o.Product.BreedName,
                //    Price = o.Product.Price,
                //    Weight = o.Product.Weight,
                //    Gender = o.Product.Gender,
                //    Description = o.Product.Description
                //}
            }).ToList();
        }
        public async Task<OrderItemResponse> GetById(int orderItemsId)
        {
            OrderItem orderItem = await _orderItemRepository.GetById(orderItemsId);
            return orderItem == null ? null : new OrderItemResponse
            {
                Id = orderItem.Id,
                Price = orderItem.Price,
                Quantity = orderItem.Quantity,
                OrderList = new OrderItemOrderListResponse
                {
                    OrderListId = orderItem.OrderList.Id,
                    OrderDateTime = orderItem.OrderList.OrderDateTime
                },
                Products = new OrderItemProductResponse
                {
                    ProductId = orderItem.Product.Id,
                    BreedName = orderItem.Product.BreedName,
                    Price = orderItem.Product.Price,
                    Weight = orderItem.Product.Weight,
                    Gender = orderItem.Product.Gender,
                    Description = orderItem.Product.Description
                }
            };
        }
        public async Task<OrderItemResponse> Create(NewOrderItem newOrderItem)
        {
            OrderItem orderItem = new OrderItem
            {
                Price = newOrderItem.Price,
                Quantity = newOrderItem.Quantity,
                OrderListId = newOrderItem.OrderListId,
                ProductId = newOrderItem.ProductId
            };

            orderItem = await _orderItemRepository.Create(orderItem);
            if (orderItem == null) return null;
            else
            {
                await _orderListRepository.GetById(orderItem.Id);
                await _productRepository.GetById(orderItem.ProductId);
                return new OrderItemResponse
                {
                    Id = orderItem.Id,
                    Price = orderItem.Price,
                    Quantity = orderItem.Quantity,
                    OrderList = new OrderItemOrderListResponse
                    {
                        OrderListId = orderItem.OrderList.Id,
                        OrderDateTime = orderItem.OrderList.OrderDateTime
                    },
                    Products = new OrderItemProductResponse
                    {
                        ProductId = orderItem.Product.Id,
                        BreedName = orderItem.Product.BreedName,
                        Price = orderItem.Product.Price,
                        Weight = orderItem.Product.Weight,
                        Gender = orderItem.Product.Gender,
                        Description = orderItem.Product.Description
                    }
                };
            }
        }
       public async Task<OrderItemResponse> Update(int orderItemsId, UpdateOrderItem updateOrderItem)
        {
            OrderItem orderItem = new OrderItem
            {
                Price = updateOrderItem.Price,
                Quantity = updateOrderItem.Quantity,
                OrderListId = updateOrderItem.OrderlistId,
                ProductId = updateOrderItem.ProductId
            };
            orderItem = await _orderItemRepository.Update(orderItemsId, orderItem);
            if (orderItem == null) return null;
            else
            {
                await _orderListRepository.GetById(orderItem.Id);
                await _productRepository.GetById(orderItem.ProductId);
                return new OrderItemResponse
                {
                     Id = orderItem.Id,
                    Price = orderItem.Price,
                    Quantity = orderItem.Quantity,
                    OrderList = new OrderItemOrderListResponse
                    {
                        OrderListId = orderItem.OrderList.Id,
                        OrderDateTime = orderItem.OrderList.OrderDateTime
                    },
                    Products = new OrderItemProductResponse
                    {
                        ProductId = orderItem.Product.Id,
                        BreedName = orderItem.Product.BreedName,
                        Price = orderItem.Product.Price,
                        Weight = orderItem.Product.Weight,
                        Gender = orderItem.Product.Gender,
                        Description = orderItem.Product.Description
                    }
                };
            }
        }

        public Task<OrderItemResponse> Delete(int orderItemId)
        {
            throw new NotImplementedException();
        }
    }
}
