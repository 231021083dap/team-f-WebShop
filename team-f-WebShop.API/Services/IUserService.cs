using team_f_WebShop.API.DTO.Login.Requests;
using team_f_WebShop.API.DTO.Users.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace team_f_WebShop.API.Services.UsersService
{

    public interface IUserService
    {
       // Task<LoginResponse> Authenticate(LoginRequest login);
        Task<List<UserResponse>> GetAll();
        Task<UserResponse> GetById(int userId);
        Task<UserResponse> Create(NewUser newUser);
        Task<UserResponse> Update(int userId, UpdateUser updateUser);
        Task<bool> Delete(int userId);
    }
}