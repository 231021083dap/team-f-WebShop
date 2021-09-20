using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace team_f_WebShop.API.DTOs.Requests
{
    public class UpdateCategory
    {
        [Required]
        [StringLength(32, ErrorMessage = "categoryName must be less than 32 char")]
        [MinLength(1, ErrorMessage = "categoryName must contain atleast 1 char")]
        public string categoryName { get; set; }
    }
}
