using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using team_f_WebShop.API.Database;
using team_f_WebShop.API.Repositories;

namespace team_f_WebShop.Tests
{
    public class categoryRepositoryTests
    {
        private readonly categoryRepository _sut;
        private readonly WebShopProjectContext _context;
        private readonly DbContextOptions<WebShopProjectContext> options;

    }
}
