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
        Task<List<Product>> GetAllProductRepository();
        Task<Product> GetByIdProductRepository(int productId);
        Task<Product> CreateProductRepository(Product product);
        Task<Product> UpdateProductRepository(int productId, Product product);
        Task<Product> DeleteProductRepository(int productId);
    }



    public class ProductRepository : IProductRepository
    {
        private readonly WebShopProjectContext _context;

        public ProductRepository(WebShopProjectContext context)
        {
            _context = context;
        }




        public async Task<List<Product>> GetAllProductRepository()
        {
            return await _context.Product
                .ToListAsync();
        }


        public async Task<Product> GetByIdProductRepository(int productId)
        {
            return await _context.Product
                .FirstOrDefaultAsync(a => a.ProductId == productId);
        }

        public async Task<Product> CreateProductRepository(Product product)
        {
            _context.Product.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProductRepository(int productId, Product product)
        {
            Product updateProduct = await _context.Product.FirstOrDefaultAsync(a => a.ProductId == productId);
            if (updateProduct != null)
            {
                updateProduct.Name = product.Name;
                updateProduct.Price = product.Price;
                updateProduct.Quantity = product.Quantity;
                updateProduct.Desciption = product.Desciption;
                await _context.SaveChangesAsync();
            }
            return updateProduct;
        }

        public async Task<Product> DeleteProductRepository(int productId)
        {
            Product product = await _context.Product.FirstOrDefaultAsync(a => a.ProductId == productId);
            if (product != null)
            {
                _context.Product.Remove(product);
                await _context.SaveChangesAsync();
            }
            return product;
        }
    }
}
