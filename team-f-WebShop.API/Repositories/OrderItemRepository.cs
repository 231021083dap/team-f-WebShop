using Microsoft.EntityFrameworkCore;
using OrangularAPI.Database;
using OrangularAPI.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrangularAPI.Repositories.OrderItemsRepository
{

    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly OrangularProjectContext _context;
        public OrderItemRepository(OrangularProjectContext context)
        {
            _context = context;
        }
        public async Task<List<OrderItem>> GetAll()
        {
            return await _context.OrderItem
                .Include(a => a.Product)
                .Include(a => a.OrderList)
                .ToListAsync();
        }
        public async Task<OrderItem> GetById(int orderItemId)
        {
            return await _context.OrderItem
                   .Include(a => a.Product)
                   .Include(a => a.OrderList)
                   .FirstOrDefaultAsync(a => a.Id == orderItemId);
        }
        public async Task<OrderItem> Create(OrderItem order_Item)
        {
            _context.OrderItem.Add(order_Item);
            await _context.SaveChangesAsync();
            return order_Item;
        }
        public async Task<OrderItem> Update(int orderItemId, OrderItem order_Item)
        {
            OrderItem updateOrderItem = await _context.OrderItem.FirstOrDefaultAsync(a => a.Id == orderItemId);
            if (updateOrderItem != null)
            {
                if (order_Item.Price != 0) updateOrderItem.Price = order_Item.Price;
                if (order_Item.Quantity != 0) updateOrderItem.Quantity = order_Item.Quantity;
                // if (order_Item.Id != 0) updateOrderItem.OrderListIdxxx = order_Item.OrderListIdxxx;
                // if (order_Item.ProductId != 0) updateOrderItem.ProductId = order_Item.ProductId;
                await _context.SaveChangesAsync();
            }
            return updateOrderItem;
        }
    }

}
