using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using team_f_WebShop.API.Database.Entities;
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



            
        [HttpPost]
        public async Task<IActionResult> CreateProductController(Product product)
        {
            try
            {
                var newProduct = await _productService.CreateProductService(product);

                if (newProduct == null) 
                {
                    return BadRequest("Product ERROR.."); // code 400
                }
                return Ok(newProduct);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }



        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProductController([FromRoute] int productId, [FromBody] Product product) 
        {
            try
            {
                var updateProduct = await _productService.UpdateProductService(productId, product);
                if (updateProduct == null)
                {
                    return BadRequest("Update failed.."); // code 400
                }
                return Ok(updateProduct);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message); // code 500
            }
        }



        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProductController([FromRoute] int productId) 
        {
            try
            {
                var deleteProduct = await _productService.DeleteProductService(productId);

                if (deleteProduct == false) // <====================================
                {
                    return BadRequest("Delete Failed.."); // code 400
                }

                return Ok(deleteProduct); // code 200 success
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}
