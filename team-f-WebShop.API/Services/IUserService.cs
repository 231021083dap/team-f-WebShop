using OrangularAPI.DTO.Login.Requests;
using OrangularAPI.DTO.Users.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrangularAPI.Services.UsersService
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