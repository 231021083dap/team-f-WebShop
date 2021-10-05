using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrangularAPI.DTO.OrderLists.Requests;
using OrangularAPI.DTO.OrderLists.Responses;
using OrangularAPI.Services.OrderListServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrangularAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderListController : ControllerBase
    {
        private readonly IOrderListService _orderListService;

        public OrderListController(IOrderListService orderListsService)
        {
            _orderListService = orderListsService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<OrderListResponse> OrderList = await _orderListService.GetAll();

                if (OrderList == null)
                {
                    return Problem("Got no data, not even an empty list, this is unexpected");
                }

                if (OrderList.Count == 0)
                {
                    return NoContent();
                }

                return Ok(OrderList);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{OrderListId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int OrderListId)
        {
            try
            {
                OrderListResponse Order_Lists = await _orderListService.GetById(OrderListId);

                if (Order_Lists == null)
                {
                    return NotFound();
                }

                return Ok(Order_Lists);
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
        public async Task<IActionResult> Create([FromBody] NewOrderList newOrderList)
        {
            try
            {
                OrderListResponse Order_Lists = await _orderListService.Create(newOrderList);

                if (Order_Lists == null)
                {
                    return Problem("Product was not created, something went wrong");
                }

                return Ok(Order_Lists);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{OrderListId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int OrderListId, [FromBody] UpdateOrderList updateOrder_Lists)
        {
            try
            {
                OrderListResponse Order_Lists = await _orderListService.Update(OrderListId, updateOrder_Lists);

                if (Order_Lists == null)
                {
                    return Problem("Product was not updated, something went wrong");
                }

                return Ok(Order_Lists);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete("{OrderListId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int OrderListId)
        {
            try
            {
                bool result = await _orderListService.Delete(OrderListId);

                if (!result)
                {
                    return Problem("Order_Lists was not deleted, something went wrong");
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
