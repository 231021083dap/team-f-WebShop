using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using team_f_WebShop.API.DTOs.Responses;

namespace team_f_WebShop.API.Services
{
    public interface IProductService
    {
        List<ProductResponse> GetAllProducts();
    }



    public class ProductService : IProductService
    {
        public List<ProductResponse> GetAllProducts()
        {
            List<ProductResponse> Products = new();



            // Product 1
            Products.Add(new ProductResponse
            {
                ProductId = 1,
                Name = "GIGABYTE FI32U",
                Price = 8575,
                Quantity = 6,
                Desciption = "LED-skærm"
            });


            // Product 2
            Products.Add(new ProductResponse
            {
                ProductId = 2,
                Name = "GIGABYTE M28U",
                Price = 5999,
                Quantity = 13,
                Desciption = "3840 x 2160 (4K)"
            });



            return Products;
        }
    }
}
