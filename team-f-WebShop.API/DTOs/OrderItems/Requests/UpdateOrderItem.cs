using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrangularAPI.DTO.OrderItems.Requests
{
    public class UpdateOrderItem
    {
        // -----------------------------------------------------------------------------------------------------------------------
        [Required]
        [Range(1, int.MaxValue)]
        public int OrderlistId { get; set; }
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
