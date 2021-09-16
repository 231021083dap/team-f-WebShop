using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using team_f_WebShop.API.Database;
using team_f_WebShop.API.Database.Entities;

namespace team_f_WebShop.API.Repositories
{

    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsRepository();
        Task<Product> GetByIdProductsRepository(int productId);
        Task<Product> CreateProductsRepository(Product product);
        Task<Product> UpdateProductsRepository(int productId, Product product);
        Task<Product> DeleteProductsRepository(int productId);
    }



    public class ProductRepository : IProductRepository
    {
        private readonly WebShopProjectContext _context;

        public ProductRepository(WebShopProjectContext context)
        {
            _context = context;
        }




        public async Task<List<Product>> GetAllProductsRepository()
        {
            return await _context.Product
                .ToListAsync();
        }


        public async Task<Product> GetByIdProductsRepository(int productId)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> CreateProductsRepository(Product product)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> UpdateProductsRepository(int productId, Product product)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> DeleteProductsRepository(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
