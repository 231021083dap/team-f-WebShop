using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using team_f_WebShop.API.DTOs.Responses;
using team_f_WebShop.API.Services;

namespace team_f_WebShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {

        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }



        [HttpGet]
        public IActionResult GetAll()
        {

            try
            {
                List<ProductResponse> Products = _productService.GetAllProducts();

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



        // ____________________________________________________________________________________________________

        [HttpGet]
        public IActionResult GetById()
        {
            List<ProductResponse> Products = _productService.GetAllProducts();

            try
            {
                if (Products == null)
                {
                    return Problem("Got no data, not even an empty list, this is unexpected"); // code 500
                }


                if (Products.Count == 0)
                {
                    return NoContent(); // code 204
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message); // code 500
            }


            return Ok(Products);  // code 200
        }
    }
}
