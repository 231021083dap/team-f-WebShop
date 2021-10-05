using Microsoft.EntityFrameworkCore;
using team_f_WebShop.API.Database;
using team_f_WebShop.API.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace team_f_WebShop.API.Repositories.OrderItemsRepository
{
    public interface IOrderItemRepository
    {
        Task<List<OrderItem>> GetAll();
        Task<OrderItem> GetById(int orderItemId);
        Task<OrderItem> Create(OrderItem order_Items);
        Task<OrderItem> Update(int orderItemId, OrderItem order_Item);
    }
}