using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using team_f_WebShop.API.Database.Entities;
using team_f_WebShop.API.DTOs.Requests;
using team_f_WebShop.API.DTOs.Responses;
using team_f_WebShop.API.Repositories;

namespace team_f_WebShop.API.Services
{
    public interface IProductService
    {
        Task<List<ProductResponse>> GetAllProductService();
        Task<ProductResponse> GetByIdProductService(int productId);
        Task<ProductResponse> CreateProductService(NewProduct newProduct);
        Task<ProductResponse> UpdateProductService(int productId, UpdateProduct updateproduct);
        Task<bool> DeleteProductService(int productId);
    }


    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }




        public async Task<List<ProductResponse>> GetAllProductService()
        {
            List<Product> products = await _productRepository.GetAllProductRepository();
            return products.Select(a => new ProductResponse
            {
                ProductId = a.ProductId,
                Name = a.Name,
                Price = a.Price,
                Quantity = a.Quantity,
                Desciption = a.Desciption
            }).ToList();
        }


        public async Task<ProductResponse> GetByIdProductService(int productId)
        {
            Product product = await _productRepository.GetByIdProductRepository(productId);
            return product == null ? null : new ProductResponse
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                Desciption = product.Desciption
            };
        }


        public async Task<ProductResponse> CreateProductService(NewProduct newProduct)
        {
            Product product = new Product
            {
                Name = newProduct.Name,
                Price = newProduct.Price,
                Quantity = newProduct.Quantity,
                Desciption = newProduct.Desciption
            };

            product = await _productRepository.CreateProductRepository(product);

            return product == null ? null : new ProductResponse
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                Desciption = product.Desciption
            };
        }


        public async Task<ProductResponse> UpdateProductService(int productId, UpdateProduct updateProduct)
        {
            Product product = new Product
            {
                Name = updateProduct.Name,
                Price = updateProduct.Price,
                Quantity = updateProduct.Quantity,
                Desciption = updateProduct.Desciption
            };

            product = await _productRepository.UpdateProductRepository(productId, product);

            return product == null ? null : new ProductResponse
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                Desciption = product.Desciption
            };
        }


        public async Task<bool> DeleteProductService(int productId)
        {
            var result = await _productRepository.DeleteProductRepository(productId);
            return (result != null);   // true
        }

    }
}
