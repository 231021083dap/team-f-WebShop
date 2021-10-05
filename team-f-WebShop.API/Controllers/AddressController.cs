using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrangularAPI.DTO.Addresses.Requests;
using OrangularAPI.DTO.Addresses.Responses;
using OrangularAPI.Services.AddressServices;

namespace OrangularAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            // _authorService far værdien authoerService
            _addressService = addressService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<AddressResponse> address = await _addressService.GetAll();

                if (address == null)
                {
                    string problem = "Got no data, not even an empty list, this is unexpected";
                    return Problem(problem);
                }

                if (address.Count == 0) return NoContent();

                return Ok(address);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        // GetById
        [HttpGet("{addressId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int addressId)
        {
            try
            {
                AddressResponse address = await _addressService.GetById(addressId);

                if (address == null)
                {
                    return NotFound();
                }

                return Ok(address);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // Create
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] NewAddress newAddress)
        {
            try
            {
                AddressResponse address = await _addressService.Create(newAddress);

                if (address == null)
                {
                    return Problem("Address was not created, something went wrong");
                }

                return Ok(address);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        // Update
        [HttpPut("{addressId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int addressId, [FromBody] UpdateAddress updateAddress)
        {
            try
            {
                AddressResponse addressResponse = await _addressService.Update(addressId, updateAddress);

                if (addressResponse == null)
                {
                    return Problem("Address was not updated, something went wrong");
                }

                return Ok(addressResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // Delete
        [HttpDelete("{addressId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int addressId)
        {
            try
            {
                bool result = await _addressService.Delete(addressId);

                if (!result)
                {
                    return Problem("Address was not deleted, something went wrong");
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
