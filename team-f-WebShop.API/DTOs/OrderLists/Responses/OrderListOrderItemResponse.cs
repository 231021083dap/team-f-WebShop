﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace team_f_WebShop.API.DTO.OrderLists.Responses
{
    public class OrderListOrderItemResponse
    {

        public int OrderItemId { get; set; }
        public string ProductName { get; set; } // I stedet for at bruge ProductId, anbefaler Jack at bruge et navn
        public int Price { get; set; }
        public int Quantity { get; set; }

    }
}
