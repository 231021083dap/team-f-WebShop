using System;
using System.Collections.Generic;

namespace OrangularAPI.DTO.OrderLists.Responses
{
    public class OrderListResponse
    {
        public int Id { get; set; }

        public DateTime OrderDateTime { get; set; }

        public List<OrderListOrderItemResponse> OrderListOrderItem { get; set; }

        public OrderListUserResponse User { get; set; }


    }
}
