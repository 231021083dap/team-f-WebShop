using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrangularAPI.DTO.OrderItems.Requests;
using OrangularAPI.DTO.OrderItems.Responses;
using OrangularAPI.Services.OrderItemServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orangular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;
        public OrderItemController(IOrderItemService OrderItemService)
        {
            _orderItemService = OrderItemService;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<OrderItemResponse> orderItem = await _orderItemService.GetAll();

                if (orderItem == null) return Problem("Got nothing... Unexpected book problem");
                if (orderItem.Count == 0) return NoContent();
                return Ok(orderItem);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        // getbyid
        [HttpGet("{orderItemId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int orderItemId)
        {
            try
            {
                OrderItemResponse orderItem = await _orderItemService.GetById(orderItemId);
                if (orderItem == null) return NotFound();
                return Ok(orderItem);
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
        public async Task<IActionResult> Create([FromBody] NewOrderItem newOrderItem)
        {
            try
            {
                OrderItemResponse OrderItem = await _orderItemService.Create(newOrderItem);
                if (OrderItem == null) return Problem("Returned null, orderItem was not created");
                return Ok(OrderItem);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        [HttpPut("{orderItemId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(
            [FromRoute] int orderItemId, [FromBody] UpdateOrderItem UpdateOrderItem)
        {
            try
            {
                OrderItemResponse orderItem = await _orderItemService.Update(orderItemId, UpdateOrderItem);
                if (orderItem == null) return Problem("Returned null, orderItem was not updated");
                return Ok(orderItem);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
