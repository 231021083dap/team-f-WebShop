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
            List<ProductResponse> Products = _productService.GetAllProducts();

            return Ok(Products);
        }
    }
}
