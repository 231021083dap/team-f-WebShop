using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using team_f_WebShop.API.DTOs.Responses;
using team_f_WebShop.API.Repositories;

namespace team_f_WebShop.API.Services
{

    public interface IcategoryService
    {
        List<categoryResponse> GetAllcategory();
    }
    public class categoryService : IcategoryService
    {
        private readonly IcategoryRepository _categoryRepository;

        public categoryService(IcategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public List<categoryResponse> GetAllcategory()
        {
            List<categoryResponse> categorys = new();

            categorys.Add(new categoryResponse
            {
                Id = 1,
                categoryName = "Computer"
            });

            categorys.Add(new categoryResponse
            {
                Id = 2,
                categoryName = "Screen"
            });

            categorys.Add(new categoryResponse
            {
                Id = 3,
                categoryName = "Webcam"
            });

            categorys.Add(new categoryResponse
            {
                Id = 4,
                categoryName = "Printer"
            });

            categorys.Add(new categoryResponse
            {
                Id = 5,
                categoryName = "Tablet"
            });

            return categorys;
        }
    }
}
