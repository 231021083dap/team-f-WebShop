using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace team_f_WebShop.API.DTOs.Responses
{
    public class categoryResponse
    {
        public int Id { get; set; }
        public string categoryName { get; set; }
        public categoryProductResponse Product { get; set; }

    }

    public class categoryProductResponse
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
    }
}
