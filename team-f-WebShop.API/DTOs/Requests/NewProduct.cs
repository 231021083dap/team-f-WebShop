using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace team_f_WebShop.API.DTOs.Requests
{
    public class NewProduct
    {
        [Required]
        [MinLength(1, ErrorMessage = "Product name cant be less then 1 char")]
        [StringLength(128, ErrorMessage = "Product name must be less then 128 chars")]
        public string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Price { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [MinLength(1, ErrorMessage = "Product description cant be less then 1 char")]
        [StringLength(256, ErrorMessage = "Product description must be less then 256 chars")]
        public string Description { get; set; }

        [ForeignKey("category.Id")]
        public int Id { get; set; }
    }
}
