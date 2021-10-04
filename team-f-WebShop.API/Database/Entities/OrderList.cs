using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace OrangularAPI.Database.Entities
{
    public class OrderList
    {
        [Key]
        public int Id { get; set; }



        [Required]
        public DateTime OrderDateTime { get; set; }


        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }



        public List<OrderItem> OrderItem { get; set; } = new();
    }
}
