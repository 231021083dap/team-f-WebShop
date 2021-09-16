using Microsoft.AspNetCore.Http;
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
    public class categoryController : ControllerBase
    {
        private readonly IcategoryService _categoryservice;

        public categoryController(IcategoryService categoryservice)
        {
            _categoryservice = categoryservice;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                List<categoryResponse> categorys = _categoryservice.GetAllcategory();

                if (categorys == null)
                {
                    return Problem("Got no data, not even an empty list, this is unexpected");
                }

                if (categorys.Count == 0)
                {
                    return NoContent();
                }

                return Ok(categorys);
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
