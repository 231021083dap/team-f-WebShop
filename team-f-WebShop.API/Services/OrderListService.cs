using team_f_WebShop.API.Database.Entities;
using team_f_WebShop.API.DTO.OrderLists.Requests;
using team_f_WebShop.API.DTO.OrderLists.Responses;
using team_f_WebShop.API.Repositories.OrderListsRepository;
using team_f_WebShop.API.Repositories.Users;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace team_f_WebShop.API.Services.OrderListServices
{
    public class OrderListService : IOrderListService
    {
        private readonly IOrderListRepository _orderListRepository;
        private readonly IUserRepository _userRepository;

        public OrderListService(IOrderListRepository orderListRepository, IUserRepository userRepository)
        {
            _orderListRepository = orderListRepository;
            _userRepository = userRepository;
        }
        public async Task<List<OrderListResponse>> GetAll()
        {
            List<OrderList> orderList = await _orderListRepository.GetAll();

            return orderList.Select(a => new OrderListResponse
            {
                Id = a.Id,
                OrderDateTime = a.OrderDateTime,
                User = new OrderListUserResponse
                {
                    UserId = a.User.Id,
                    Email = a.User.Email,
                    Role = a.User.Role
                }
            }).ToList();
        }
        public async Task<OrderListResponse> GetById(int orderListId)
        {
            OrderList orderList = await _orderListRepository.GetById(orderListId);
            return orderList == null ? null : new OrderListResponse
            {
                Id = orderList.Id,
                OrderDateTime = orderList.OrderDateTime

            };
        }
        public async Task<OrderListResponse> Create(NewOrderList newOrderList)
        {
            OrderList orderList = new OrderList
            {
                // UserIdxxx = newOrderList.UserId,
                OrderDateTime = newOrderList.OrderDateTime

            };

            orderList = await _orderListRepository.Create(orderList);

            return orderList == null ? null : new OrderListResponse
            {
                Id = orderList.Id,
                OrderDateTime = orderList.OrderDateTime

            };
        }

        public async Task<OrderListResponse> Update(int orderListId, UpdateOrderList updateOrderList)
        {
            OrderList orderList = new OrderList
            {
                OrderDateTime = updateOrderList.OrderDateTime,
                // UserIdxxx = updateOrderList.UserId,
            };

            orderList = await _orderListRepository.Update(orderListId, orderList);

            return orderList == null ? null : new OrderListResponse
            {
                Id = orderListId,
                OrderDateTime = orderList.OrderDateTime
            };
        }
        public async Task<bool> Delete(int orderlistId)
        {
            var result = await _orderListRepository.Delete(orderlistId);
            return (result != null);
        }

    }
}
