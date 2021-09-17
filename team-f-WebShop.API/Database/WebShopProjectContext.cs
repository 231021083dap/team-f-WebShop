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
        public WebShopProjectContext(){}

        public WebShopProjectContext(DbContextOptions<WebShopProjectContext> options): base(options){}

        public DbSet<category> category { get; set; }

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
        }

    }
}
