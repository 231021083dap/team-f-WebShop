using Microsoft.EntityFrameworkCore;
using OrangularAPI.Database;
using OrangularAPI.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrangularAPI.Repositories.Users
{
    public class UserRepository : IUserRepository
    {

        private readonly OrangularProjectContext _context;
        public UserRepository(OrangularProjectContext Context)
        {
            _context = Context;
        }
        public async Task<List<User>> GetAll()
        {
            return await _context.User
                .Include(a => a.Address)
                .Include(a => a.OrderList)
                .ToListAsync();
        }
        public async Task<User> GetById(int userId) 
        {
            return await _context.User
                .Include(a => a.Address)
                .Include(b => b.OrderList)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }
        public async Task<User> GetByEmail(string email)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<User> Create(User user)
        {
            if (user.Email != null && user.Password != null)
            {
                if (_context.User.Any(u => u.Id == user.Id)) throw new Exception("Nice try, userId " + user.Id + " already Exists");
                if (_context.User.Any(u => u.Email == user.Email)) throw new Exception("Email " + user.Email + " is already taken");
                _context.User.Add(user);
                await _context.SaveChangesAsync();
                return user;
            }
            throw new Exception("You must enter an email and a password to create a user");
        }
        public async Task<User> Update(int userId, User user)
        {
            User updateUser = await _context.User.FirstOrDefaultAsync(u => u.Id == userId);
            if (updateUser != null)
            {
                if (_context.User.Any(u => u.Id != userId && u.Email == user.Email)) throw new Exception("Email " + user.Email + " is already taken");
                if (user.Email != null) updateUser.Email = user.Email;
                if (user.Password != null) updateUser.Password = user.Password;
                if (user.Role != 0) updateUser.Role = user.Role;
                await _context.SaveChangesAsync();
            }
            return updateUser;
        }
        public async Task<User> Delete(int userId)
        {
            User deleteUser = await _context.User.FirstOrDefaultAsync(u => u.Id == userId);
            if (deleteUser != null)
            {
                _context.User.Remove(deleteUser);
                await _context.SaveChangesAsync();
            }
            return deleteUser;
        }
    }
}
