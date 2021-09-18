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
        IEnumerable<category> GetAll();
    }
    public class categoryRepository : IcategoryRepository
    {
        private readonly WebShopProjectContext _context;

        public categoryRepository(WebShopProjectContext context)
        {
            _context = context;
        }
        public IEnumerable<category> GetAll()
        {
            return _context.category;
        }
    }
}
