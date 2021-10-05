using team_f_WebShop.API.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace team_f_WebShop.API.Repositories.OrderListsRepository
{
    public interface IOrderListRepository
    {
        Task<List<OrderList>> GetAll();
        Task<OrderList> GetById(int OrderListId);
        Task<OrderList> Create(OrderList order_Lists);
        Task<OrderList> Update(int OrderListId, OrderList order_Lists);
        Task<OrderList> Delete(int OrderListId);
    }
}
