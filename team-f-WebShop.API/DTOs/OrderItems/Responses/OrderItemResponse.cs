using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace team_f_WebShop.API.DTO.OrderItems.Responses
{
    public class OrderItemResponse
    {
        public int Id { get; set; }

        public int Price { get; set; }
        
        public int Quantity { get; set; }

        public OrderItemProductResponse Products { get; set; }

        public OrderItemOrderListResponse OrderList { get; set; }
    }
}
