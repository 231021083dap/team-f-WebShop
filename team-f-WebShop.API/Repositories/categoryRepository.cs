using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using team_f_WebShop.API.Database;
using team_f_WebShop.API.Database.Entities;

namespace team_f_WebShop.API.Repositories
{
    public interface IcategoryRepository
    {
        Task<List<category>> GetAll();
        Task<category> GetById(int categoryId);
        Task<category> Create(category category);
        Task<category> Update(int categoryId, category category);
        Task<category> Delete(int categoryId);
    }
    public class categoryRepository : IcategoryRepository
    {
        private readonly WebShopProjectContext _context;

        public categoryRepository(WebShopProjectContext context)
        {
            _context = context;
        }
        public async Task<List<category>> GetAll()
        {
            return await _context.category.ToListAsync();
        }

        public async Task<category> GetById(int categoryId)
        {
            return await _context.category.FirstOrDefaultAsync(a => a.Id == categoryId);
        }

        public async Task<category> Create(category category)
        {
            _context.category.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<category> Update(int categoryId, category category)
        {
            category updatecategory = await _context.category.FirstOrDefaultAsync(a => a.Id == categoryId);
            if (updatecategory != null)
            {
                updatecategory.categoryName = category.categoryName;
                await _context.SaveChangesAsync();
            }
            return updatecategory;
        }

        public async Task<category> Delete(int categoryId)
        {
            category category = await _context.category.FirstOrDefaultAsync(a => a.Id == categoryId);
            if (category != null)
            {
                _context.category.Remove(category);
                await _context.SaveChangesAsync();
            }
            return category;
        }

    }
}
