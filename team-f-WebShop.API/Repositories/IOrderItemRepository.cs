using Microsoft.EntityFrameworkCore;
using OrangularAPI.Database;
using OrangularAPI.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrangularAPI.Repositories.OrderItemsRepository
{
    public interface IOrderItemRepository
    {
        Task<List<OrderItem>> GetAll();
        Task<OrderItem> GetById(int orderItemId);
        Task<OrderItem> Create(OrderItem order_Items);
        Task<OrderItem> Update(int orderItemId, OrderItem order_Item);
    }
}