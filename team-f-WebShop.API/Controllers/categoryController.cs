using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using team_f_WebShop.API.DTOs.Requests;
using team_f_WebShop.API.DTOs.Responses;
using team_f_WebShop.API.Services;

namespace team_f_WebShop.API.Controllers
{
    // api-api
    
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<categoryResponse> categorys = await _categoryservice.GetAllcategory();

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

        [HttpGet("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int categoryId)
        {
            try
            {
                categoryResponse category = await _categoryservice.GetById(categoryId);

                if (category == null)
                {
                    return NotFound();
                }

                return Ok(category);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] NewCategory newCategory)
        {
            try
            {
                categoryResponse category = await _categoryservice.Create(newCategory);

                if (category == null)
                {
                    return Problem("Category was not Created, something went wrong");
                }

                return Ok(category);
            }

            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int categoryId, [FromBody] UpdateCategory updateCategory)
        {
            try
            {
                categoryResponse category = await _categoryservice.Update(categoryId, updateCategory);

                if (category == null)
                {
                    return Problem("Category was not updated, something went wrong");
                }

                return Ok(category);
            }

            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpDelete("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int categoryId)
        {
            try
            {
                bool result = await _categoryservice.Delete(categoryId);

                if (!result)
                {
                    return Problem("Category was not deleted, something went wrong");
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
