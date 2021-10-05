using team_f_WebShop.API.DTO.OrderItems.Requests;
using team_f_WebShop.API.DTO.OrderItems.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace team_f_WebShop.API.Services.OrderItemServices
{
    public interface IOrderItemService
    {
        Task<List<OrderItemResponse>> GetAll();
        Task<OrderItemResponse> GetById(int orderItemId);
        Task<OrderItemResponse> Create(NewOrderItem newOrderItem);
        Task<OrderItemResponse> Update(int orderItemId, UpdateOrderItem updateOrderItem);
        Task<OrderItemResponse> Delete(int orderItemId);
    }
}