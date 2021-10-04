using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using team_f_WebShop.API.Database.Entities;
using team_f_WebShop.API.DTOs.Requests;
using team_f_WebShop.API.DTOs.Responses;
using team_f_WebShop.API.Services;

namespace team_f_WebShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }




        // GET ALL
        [HttpGet] 
        public async Task<IActionResult> GetAllProductController()
        {
            try
            {
                List<ProductResponse> Products = await _productService.GetAllProductService();

                if (Products == null)
                {
                    return Problem("Got no data, not even an empty list, this is unexpected"); // code 500
                }

                if (Products.Count == 0)
                {
                    return NoContent(); // code 204
                }

                return Ok(Products);  // code 200
            }

            catch (Exception ex)
            { 
                return Problem(ex.Message); // code 500
            }
        }


        // GET BY ID
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetByIdProductController([FromRoute] int productId)
        {
            try
            {
                ProductResponse products = await _productService.GetByIdProductService(productId);

                if (products == null)
                {
                    return NotFound();  // code 204
                }
                return Ok(products); // code 200

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);  // code 500
            }
        }


        // GET PRODUCT BY CATEGORY
        [HttpGet("category/{Id}")]
        public async Task<IActionResult> GetByCategoryIdProductController([FromRoute] int Id)
        {
            try
            {
                ProductResponse products = await _productService.GetByCategoryIdProductService(Id);

                if (products == null)
                {
                    return NotFound();  // code 204
                }
                return Ok(products); // code 200

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);  // code 500
            }
        }


        // CREATE
        [HttpPost]
        public async Task<IActionResult> CreateProductController([FromBody] NewProduct newProduct)
        {
            try
            {
                ProductResponse product = await _productService.CreateProductService(newProduct);

                if (product == null) 
                {
                    return Problem("Product ERROR..");   // code 500
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        // UPDATE
        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProductController([FromRoute] int productId, [FromBody] UpdateProduct updateProduct) 
        {
            try
            {
                ProductResponse product = await _productService.UpdateProductService(productId, updateProduct);


                if (product == null)
                {
                    return Problem("Product was not updated, something went wrong");
                }
                return Ok(product);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message); // code 500
            }
        }


        // DELETE
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProductController([FromRoute] int productId) 
        {
            try
            {
                var deleteProduct = await _productService.DeleteProductService(productId);

                if (!deleteProduct) 
                {
                    return Problem("Product was not deleted, something went wrong");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
