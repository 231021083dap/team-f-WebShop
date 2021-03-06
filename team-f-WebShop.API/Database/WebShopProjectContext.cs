using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using team_f_WebShop.API.Database.Entities;

namespace team_f_WebShop.API.Database
{
    public class WebShopProjectContext : DbContext
    {

        public WebShopProjectContext() { }
        public WebShopProjectContext(DbContextOptions<WebShopProjectContext> options) : base(options) { }


        // Product/category Table
        public DbSet<Product> Product { get; set; }
        public DbSet<category> category { get; set; }




        // 2 Products ADDED before creating DATABASE
        // 2 Categories ADDED before creating DATABASE
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            
             modelBuilder.Entity<category>().HasData(
                new category
                {
                    Id = 1,
                    categoryName = "Computer"
                },
                new category
                {
                    Id = 2,
                    categoryName = "Screen"
                });  
            
            modelBuilder.Entity<Product>().HasData(

                new Product
                {
                    ProductId = 1,
                    Name = "GIGABYTE FI32U",
                    Price = 8575,
                    Quantity = 6,
                    Desciption = "LED-skærm"
                },

                new Product
                {
                    ProductId = 2,
                    Name = "GIGABYTE M28U",
                    Price = 5999,
                    Quantity = 13,
                    Desciption = "3840 x 2160 (4K)"
                });
        }

    }
}
