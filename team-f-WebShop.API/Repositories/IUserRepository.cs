using OrangularAPI.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrangularAPI.Repositories.Users
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll();
        Task<User> GetById(int userId);
        Task<User> GetByEmail(string email);
        Task<User> Create(User user);
        Task<User> Update(int userId, User user);
        Task<User> Delete(int userId);
    }
}
