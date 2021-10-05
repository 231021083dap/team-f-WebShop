using OrangularAPI.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrangularAPI.DTO.Addresses.Responses;
using OrangularAPI.DTO.Addresses.Requests;
using OrangularAPI.Repositories.AddressesRepository;
using OrangularAPI.Repositories.Users;

namespace OrangularAPI.Services.AddressServices
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IUserRepository _userRepository;

        public AddressService(IAddressRepository addressesRepository, IUserRepository userRepository)
        {
            _addressRepository = addressesRepository;
            _userRepository = userRepository;
        }
        
        // Retunere en liste med alle adresser
        public async Task<List<AddressResponse>> GetAll()
        {
            // Henter alle addresser fra database
            List<Address> address = await _addressRepository.GetAll();

            // Retuner listen med addresses
            return address == null ? null : address.Select(
            a => new AddressResponse
            {
                Id = a.Id,
                Address = a.AddressName,
                ZipCode = a.ZipCode,
                CityName = a.CityName,
                User = new AddressUserResponse
                {
                    UserId = a.User.Id,
                    Email = a.User.Email,
                    Role = a.User.Role
                }
            }).ToList();
        }

        public async Task<AddressResponse> GetById(int addressId)
        {
            Address address = await _addressRepository.GetById(addressId);

            return address == null ? null : new AddressResponse
            {
                Id = address.Id,
                Address =  address.AddressName,
                ZipCode =  address.ZipCode,
                CityName = address.CityName,
                User = null
            };
        }

        public async Task<AddressResponse> Create(NewAddress newAddress)
        {
            Address address = new Address
            {
                UserId = newAddress.UserId,
                AddressName = newAddress.Address,
                ZipCode = newAddress.ZipCode,
                CityName = newAddress.CityName,
                
            };

            address = await _addressRepository.Create(address);
            await _userRepository.GetById(address.UserId);

            return address == null ? null : new AddressResponse
            {
                Id =  address.Id,
                Address =  address.AddressName,
                ZipCode =  address.ZipCode,
                CityName = address.CityName,
                User = new AddressUserResponse
                {
                    UserId = address.User.Id,
                    Email = address.User.Email,
                    Role = address.User.Role
                }
            };
        }

        public async Task<AddressResponse> Update(int input_address_id, UpdateAddress input_address)
        {
            Address address = new Address
            {
                // UserId = input_address.UserId,
                AddressName = input_address.Address,
                ZipCode = input_address.ZipCode,
                CityName = input_address.CityName
            };

            address = await _addressRepository.Update(input_address_id, address);

            return address == null ? null : new AddressResponse
            {
                Id =  address.Id,
                Address =  address.AddressName,
                ZipCode =  address.ZipCode,
                CityName = address.CityName,
                User = null
            };
        }

        public async Task<bool> Delete(int input_address_id)
        {
            Boolean result = await _addressRepository.Delete(input_address_id);

            return result;
        }
    }
}
