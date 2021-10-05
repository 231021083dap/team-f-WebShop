using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OrangularAPI.Database.Entities
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string AddressName { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public int ZipCode { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string CityName { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
