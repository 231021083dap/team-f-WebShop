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
    }
}
