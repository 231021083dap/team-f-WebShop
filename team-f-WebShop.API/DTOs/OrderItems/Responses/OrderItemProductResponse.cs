using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrangularAPI.DTO.OrderItems.Responses
{
    public class OrderItemProductResponse
    {
        public int ProductId { get; set; }
        public string BreedName { get; set; }
        public int Price { get; set; }
        public int Weight { get; set; }
        public string Gender { get; set; }
        public string Description { get; set; }
    }
}
