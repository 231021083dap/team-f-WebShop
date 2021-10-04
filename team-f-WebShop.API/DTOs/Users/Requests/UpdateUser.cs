using OrangularAPI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrangularAPI.DTO.Login.Requests
{
    public class UpdateUser
    {
        [Required]
        [StringLength(255, ErrorMessage = "Email must be less than 255 chars")]
        public string Email { get; set; }
        [Required]
        [StringLength(255, ErrorMessage = "Username must be less than 255 chars")]
        public string Password { get; set; }
        [Required]
        public Role Role { get; set; }
    }
}
