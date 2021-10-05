﻿using Microsoft.EntityFrameworkCore;
using team_f_WebShop.API.Database;
using team_f_WebShop.API.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace team_f_WebShop.API.Repositories.OrderListsRepository
{
    public class OrderListRepository : IOrderListRepository
    {
        private readonly WebShopProjectContext _context;

        public OrderListRepository(WebShopProjectContext context)
        {
            _context = context;
        }
        public async Task<List<OrderList>> GetAll()
        {
            return await _context.OrderList
              .Include(o => o.User)
              .ToListAsync();
        }
        public async Task<OrderList> GetById(int OrderListId)
        {
            return await _context.OrderList.FirstOrDefaultAsync(a => a.Id == OrderListId);
        }

        public async Task<OrderList> Create(OrderList order_Lists)
        {
            _context.OrderList.Add(order_Lists);
            await _context.SaveChangesAsync();
            return order_Lists;
        }
        public async Task<OrderList> Update(int OrderListId, OrderList order_Lists)
        {
            OrderList updateOrder_Lists = await _context.OrderList.FirstOrDefaultAsync(a => a.Id == OrderListId);
            if (updateOrder_Lists != null)
            {
                updateOrder_Lists.User = order_Lists.User;
                await _context.SaveChangesAsync();
            }
            return updateOrder_Lists;
        }
        public async Task<OrderList> Delete(int OrderListId)
        {
            OrderList order_Lists = await _context.OrderList.FirstOrDefaultAsync(a => a.Id == OrderListId);
            if (order_Lists != null)
            {
                _context.OrderList.Remove(order_Lists);
                await _context.SaveChangesAsync();
            }
            return order_Lists;
        }






    }
}
