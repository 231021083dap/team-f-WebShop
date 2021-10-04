using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrangularAPI.DTO.OrderItems.Requests
{
    public class NewOrderItem
    {
        // -----------------------------------------------------------------------------------------------------------------------
        [Required]
        [Range(1, int.MaxValue)]
        public int OrderListId { get; set; }
        // -----------------------------------------------------------------------------------------------------------------------
        [Required]
        [Range(1, int.MaxValue)]
        public int ProductId { get; set; }
        // -----------------------------------------------------------------------------------------------------------------------
        [Required]
        [Range(1, int.MaxValue)]
        public int Price { get; set; }
        // -----------------------------------------------------------------------------------------------------------------------
        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        // -----------------------------------------------------------------------------------------------------------------------
    }
}
