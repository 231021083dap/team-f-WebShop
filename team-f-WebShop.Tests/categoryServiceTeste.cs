using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using team_f_WebShop.API.Database.Entities;
using team_f_WebShop.API.DTOs.Responses;
using team_f_WebShop.API.Repositories;
using team_f_WebShop.API.Services;
using Xunit;

namespace team_f_WebShop.Tests
{
    
    public class categoryServiceTeste
    {
        private readonly categoryService _sut;
        private readonly Mock<IcategoryRepository> _categoryRepository = new();

        public categoryServiceTeste()
        {
            _sut = new categoryService(_categoryRepository.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnListOfcatagoryResponses_WhencategorysExists() 
        {
            // Arrange
            List<category> categorys = new();

            categorys.Add(new category
            {
                Id = 1,
                categoryName = "Computer"
            });
            categorys.Add(new category
            {
                     Id = 2,
                     categoryName = "Screen"
            });

            _categoryRepository
                .Setup(a => a.GetAll())
                .ReturnsAsync(categorys);

            // Act

            var result = await _sut.GetAllcategory();

            // Assert

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<categoryResponse>>(result);
        }

    }
}
