using team_f_WebShop.API.Database.Entities;
using team_f_WebShop.API.DTO.Addresses.Responses;
using team_f_WebShop.API.DTO.Addresses.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace team_f_WebShop.API.Services.AddressServices
{
    public interface IAddressService
    {
        Task<List<AddressResponse>> GetAll();
        Task<AddressResponse> GetById(int addressId);
        Task<AddressResponse> Create(NewAddress address);
        Task<AddressResponse> Update(int addressId, UpdateAddress address);
        Task<Boolean> Delete(int addressId);
    }
}
