using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace team_f_WebShop.API.DTO.OrderLists.Requests
{
    public class NewOrderList
    {
        // -----------------------------------------------------------------------------------------------------------------------
        [Required]
        [Range(1, int.MaxValue)]
        public int UserId { get; set; }
        // -----------------------------------------------------------------------------------------------------------------------
        [Required]
        public DateTime OrderDateTime { get; set; }
        // -----------------------------------------------------------------------------------------------------------------------
    }
}
