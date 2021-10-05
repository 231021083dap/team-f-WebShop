using Microsoft.EntityFrameworkCore;
using OrangularAPI.Database.Entities;
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
        public DbSet<Address> Address { get; set; }
        public DbSet<OrderItem> orderItem { get; set; }
        public DbSet<OrderList> orderLists { get; set; }
        public DbSet<User> User { get; set; }




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
                    Description = "LED-skærm",
                    Id = 1

                },

                new Product
                {
                    ProductId = 2,
                    Name = "GIGABYTE M28U",
                    Price = 5999,
                    Quantity = 13,
                    Description = "3840 x 2160 (4K)",
                    Id = 2
                });


            modelBuilder.Entity<Address>().HasData(
                new Address
                {
                    Id = 1,
                    AddressName = "Telegrafvej 9",
                    ZipCode = 2750,
                    CityName = "Ballerup",
                    UserId = 1,
                },
                new Address
                {
                    Id = 1,
                    AddressName = "Engblommevej 23",
                    ZipCode = 4040,
                    CityName = "Jyllinge",
                    UserId = 2,
                });

            modelBuilder.Entity<OrderItem>().HasData(            
                new OrderItem
                {
                    Id = 1,
                    Price = 5999,     
                    Quantity = 2,
                    OrderListId = 1,    
                    ProductId = 1
                });


            modelBuilder.Entity<OrderList>().HasData(
                new OrderList
                {
                    Id = 2,
                    OrderDateTime = DateTime.Now,
                    UserId = 2
                },
                new OrderList
                {
                    Id = 1,
                    OrderDateTime = DateTime.Now,
                    UserId = 1
                });


            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Email = "admin@admins.com",
                    Password = "Passw0rd",
                    Role = Role.Admin,
                },
                new User
                {
                    Id = 2,
                    Email = "user@users.com",
                    Password = "Passw0rd",
                    Role = Role.User
                });
        }

    }
}
