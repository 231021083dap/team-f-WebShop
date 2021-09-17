using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using team_f_WebShop.API.Database.Entities;
using team_f_WebShop.API.DTOs.Responses;
using team_f_WebShop.API.Repositories;

namespace team_f_WebShop.API.Services
{
    public interface IProductService
    {
        Task<List<ProductResponse>> GetAllProductsService();
        Task<ProductResponse> GetByIdProductsService(int productId);
        Task<ProductResponse> CreateProductsService(Product product);
        Task<ProductResponse> UpdateProductsService(int productId, Product product);
        Task<bool> DeleteProductsService(int productId);
    }


    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }



        public async Task<List<ProductResponse>> GetAllProductsService()
        {
            List<Product> products = await _productRepository.GetAllProductsRepository();
            return products.Select(a => new ProductResponse
            {
                ProductId = a.ProductId,
                Name = a.Name,
                Price = a.Price,
                Quantity = a.Quantity,
                Desciption = a.Desciption

            }).ToList();
        }


        /*
        public async Task<ProductResponse> GetByIdProductsService(int productId)
        {
            Product product = await _productRepository.GetById(productId);
            return author == null ? null : new AuthorResponse
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                MiddleName = author.MiddleName,
                Books = author.Books.Select(b => new AuthorBookResponse
                {
                    Id = b.Id,
                    Title = b.Title,
                    Pages = b.Pages
                }).ToList()
            };
        }
        */
        public Task<ProductResponse> GetByIdProductsService(int productId)
        {
            throw new NotImplementedException();
        }


        public async Task<ProductResponse> CreateProductsService(Product product)
        {
            throw new NotImplementedException();
        }


        public async Task<ProductResponse> UpdateProductsService(int productId, Product product)
        {
            throw new NotImplementedException();
        }


        public async Task<bool> DeleteProductsService(int productId)
        {
            throw new NotImplementedException();
        }

    }
}
