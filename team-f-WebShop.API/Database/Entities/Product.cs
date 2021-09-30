using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace team_f_WebShop.API.Database.Entities
{
    public class Product
    {
        [Key]
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(128)")]
        public string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Price { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Column(TypeName = "nvarchar(256)")]
        public string Desciption { get; set; }

        [ForeignKey("category.Id")]
        public int Id { get; set; }
    }
}
