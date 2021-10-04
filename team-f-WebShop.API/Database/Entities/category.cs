using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace team_f_WebShop.API.Database.Entities
{
    public class category
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(32)")]
        public string categoryName { get; set; }
        public List<Product> Products { get; set; } = new();
    }
}
