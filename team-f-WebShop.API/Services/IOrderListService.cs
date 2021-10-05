using team_f_WebShop.API.DTO.OrderLists.Requests;
using team_f_WebShop.API.DTO.OrderLists.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace team_f_WebShop.API.Services.OrderListServices
{
    public interface IOrderListService
    {
        Task<List<OrderListResponse>> GetAll();
        Task<OrderListResponse> GetById(int orderlistId);
        Task<OrderListResponse> Create(NewOrderList newOrderList);
        Task<OrderListResponse> Update(int orderListId, UpdateOrderList updateOrderList);
        Task<bool> Delete(int orderListId);
    }
}
