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


            return Products;
        }


    }
}
