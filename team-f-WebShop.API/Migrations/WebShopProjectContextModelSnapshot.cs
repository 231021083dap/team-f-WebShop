﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using team_f_WebShop.API.Database;

namespace team_f_WebShop.API.Migrations
{
    [DbContext(typeof(WebShopProjectContext))]
    partial class WebShopProjectContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("team_f_WebShop.API.Database.Entities.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Desciption")
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.ToTable("Product");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            Desciption = "LED-skærm",
                            Name = "GIGABYTE FI32U",
                            Price = 8575,
                            Quantity = 6
                        },
                        new
                        {
                            ProductId = 2,
                            Desciption = "3840 x 2160 (4K)",
                            Name = "GIGABYTE M28U",
                            Price = 5999,
                            Quantity = 13
                        });
                });

            modelBuilder.Entity("team_f_WebShop.API.Database.Entities.category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("categoryName")
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.ToTable("category");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            categoryName = "Computer"
                        },
                        new
                        {
                            Id = 2,
                            categoryName = "Screen"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
