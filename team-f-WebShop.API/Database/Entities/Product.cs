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
        public int ProductId { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string Name { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "nvarchar(32)")]

        public string Desciption { get; set; }
    }
}
