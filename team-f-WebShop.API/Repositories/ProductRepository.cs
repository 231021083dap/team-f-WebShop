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
        Task<Product> GetByCategoryIdProductRepository(int Id);
    }



    public class ProductRepository : IProductRepository
    {
        private readonly WebShopProjectContext _context;

        public ProductRepository(WebShopProjectContext context)
        {
            _context = context;
        }



        // GET ALL
        public async Task<List<Product>> GetAllProductRepository()
        {
            return await _context.Product
                .Include(a => a.category) //Includes type  
                .ToListAsync();
        }


        // GET BY ID
        public async Task<Product> GetByIdProductRepository(int productId)
        {
            return await _context.Product
                .Include(a => a.category)
                .FirstOrDefaultAsync(a => a.ProductId == productId);
        }


       
        //GET PRODUCT BY TYPE
        public async Task<Product> GetByCategoryIdProductRepository(int Id)  
        {
            return await _context.Product
                .Where(a => a.Id == Id)
                .Include(a => a.category)
                .FirstOrDefaultAsync(a => a.Id == Id);
        }
        

        // CREATE
        public async Task<Product> CreateProductRepository(Product product)
        {
            _context.Product.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }


        // UPDATE
        public async Task<Product> UpdateProductRepository(int productId, Product product)
        {
            Product updateProduct = await _context.Product.FirstOrDefaultAsync(a => a.ProductId == productId);
            if (updateProduct != null)
            {
                updateProduct.Name = product.Name;
                updateProduct.Price = product.Price;
                updateProduct.Quantity = product.Quantity;
                updateProduct.Description = product.Description;
                await _context.SaveChangesAsync();
            }
            return updateProduct;
        }


        // DELETE
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
