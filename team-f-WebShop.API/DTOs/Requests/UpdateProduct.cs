using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace team_f_WebShop.API.DTOs.Requests
{
    public class UpdateProduct
    {
        [Required]
        [MinLength(1, ErrorMessage = "Product name cant be less then 1 char")]
        [StringLength(64, ErrorMessage = "Product name must be less then 64 chars")]
        public string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Price { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [MinLength(1, ErrorMessage = "Product description cant be less then 1 char")]
        [StringLength(128, ErrorMessage = "Product description must be less then 128 chars")]
        public string Desciption { get; set; }
    }
}
