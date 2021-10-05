using Microsoft.EntityFrameworkCore;
using team_f_WebShop.API.Database;
using team_f_WebShop.API.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace team_f_WebShop.API.Repositories.AddressesRepository
{
    public interface IAddressRepository
    {
        Task<List<Address>> GetAll();
        Task<Address> GetById(int addressesId);
        Task<Address> Create(Address addresses);
        Task<Address> Update(int addressesId, Address addresses);
        Task<bool> Delete(int addressesId);
    }
}