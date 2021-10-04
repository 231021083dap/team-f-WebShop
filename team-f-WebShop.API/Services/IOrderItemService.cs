using OrangularAPI.DTO.OrderItems.Requests;
using OrangularAPI.DTO.OrderItems.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrangularAPI.Services.OrderItemServices
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