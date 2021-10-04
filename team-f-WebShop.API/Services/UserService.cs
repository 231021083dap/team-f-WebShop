using OrangularAPI.Database.Entities;
using OrangularAPI.DTO.Login.Requests;
using OrangularAPI.DTO.Users.Responses;
using OrangularAPI.Helpers;
using OrangularAPI.Repositories.Users;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrangularAPI.Services.UsersService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
       // private readonly IJwtUtils _jwtUtils;
        public UserService(IUserRepository userRepository/*, IJwtUtils jwtUtils*/)
        {
            _userRepository = userRepository;
         //   _jwtUtils = jwtUtils;
        }
        //public async Task<LoginResponse> Authenticate(LoginRequest login)
        //{
        //    Users user = await _userRepository.GetByEmail(login.Email);
        //    if (user == null) return null;
        //    if (user.password == login.Password)
        //    {
        //        LoginResponse response = new LoginResponse
        //        {
        //            Id = user.users_id,
        //            Email = user.email,
        //            Role = user.role,
        //            Token = _jwtUtils.GenerateJwtToken(user)
        //        };
        //        return response;
        //    }
        //    return null;
        //}
        private UserResponse userResponse(User user) // userResponse bliver brugt til GetById, Create & Update
        {
            return user == null ? null : new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role
            };
        }
        public async Task<List<UserResponse>> GetAll()
        {
            List<User> users = await _userRepository.GetAll();
            return users.Select(u => new UserResponse
            {
                Id = u.Id,
                Email = u.Email,
                Role = u.Role,
                OrderList = u.OrderList.Select(o => new UserOrderListResponse
                {
                    OrderListId = o.Id,
                    OrderDateTime = o.OrderDateTime
                }).ToList(),
                Addresses = u.Address.Select(a => new UserAddressResponse
                {
                    AddressId = a.Id,
                    Address = a.AddressName,
                    ZipCode = a.ZipCode,
                    CityName = a.CityName
                }).ToList()
            }).ToList();
        }
        public async Task<UserResponse> GetById(int userId)
        {
            User user = await _userRepository.GetById(userId);
            return userResponse(user);
        }
        public async Task<UserResponse> Create(NewUser newUser)
        {
            User user = new User
            {
                Email = newUser.Email,
                Password = newUser.Password,
                Role = Role.User // All users created through Register has the role of user
            };
            user = await _userRepository.Create(user);
            return userResponse(user);
        }
        public async Task<UserResponse> Update(int userId, UpdateUser updateUser)
        {
            User user = new User
            {
                Email = updateUser.Email,
                Password = updateUser.Password,
                Role = updateUser.Role
            };
            user = await _userRepository.Update(userId, user);
            return userResponse(user);
        }
        public async Task<bool> Delete(int userId)
        {
            var result = await _userRepository.Delete(userId);
            if (result != null) return true;
            else return false;
        }
    }
}
