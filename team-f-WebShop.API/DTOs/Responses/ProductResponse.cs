using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace team_f_WebShop.API.DTOs.Responses
{
    public class ProductResponse
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string Desciption { get; set; }
    }
}
