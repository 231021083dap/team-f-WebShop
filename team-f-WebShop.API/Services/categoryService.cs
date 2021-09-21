using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using team_f_WebShop.API.Database.Entities;
using team_f_WebShop.API.DTOs.Requests;
using team_f_WebShop.API.DTOs.Responses;
using team_f_WebShop.API.Repositories;

namespace team_f_WebShop.API.Services
{

    public interface IcategoryService
    {
        Task<List<categoryResponse>> GetAllcategory();
        Task<categoryResponse> GetById(int categoryId);
        Task<categoryResponse> Create(NewCategory newCategory);
        Task<categoryResponse> Update(int categoryId, UpdateCategory updatecategory);
        Task<bool> Delete(int categoryId);

    }
    public class categoryService : IcategoryService
    {
        private readonly IcategoryRepository _categoryRepository;

        public categoryService(IcategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<categoryResponse>> GetAllcategory()
        {
            List<category> categorys = await _categoryRepository.GetAll();

            return categorys.Select(a => new categoryResponse
            {
                Id = a.Id,
                categoryName = a.categoryName
            }).ToList();
        }

        public async Task<categoryResponse> GetById(int categoryId)
        {
            category category = await _categoryRepository.GetById(categoryId);

            return category == null ? null : new categoryResponse
            {
                Id = category.Id,
                categoryName = category.categoryName
            };
        }

        public async Task<categoryResponse> Create(NewCategory newCategory)
        {
            category category = new category
            {
                categoryName = newCategory.categoryName
            };

            category = await _categoryRepository.Create(category);

            return category == null ? null : new categoryResponse
            {
                Id = category.Id,
                categoryName = category.categoryName
            };
        }

        public async Task<categoryResponse> Update(int categoryId, UpdateCategory updatecategory)
        {
            category category = new category
            {
                categoryName = updatecategory.categoryName
            };

            category = await _categoryRepository.Update(categoryId, category);

            return category == null ? null : new categoryResponse
            {
                Id = category.Id,
                categoryName = category.categoryName
            };
        }

        public async Task<bool> Delete(int categoryId)
        {
            var result = await _categoryRepository.Delete(categoryId);
            return true;
        }
    }
}
