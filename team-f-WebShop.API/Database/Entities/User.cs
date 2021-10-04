using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OrangularAPI.Helpers;


namespace OrangularAPI.Database.Entities
    {
    public class User
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string Email { get; set; }



        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string Password { get; set; }



        [Required]
        public Role Role { get; set; }



        public List<OrderList> OrderList { get; set; }



        public List<Address> Address { get; set; }
    }
}
