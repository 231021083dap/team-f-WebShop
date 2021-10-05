using Microsoft.EntityFrameworkCore;
using team_f_WebShop.API.Database;
using team_f_WebShop.API.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace team_f_WebShop.APIg.Repositories.AddressesRepository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly WebShopProjectContext _context;

        // Bruges af AddressesRespositoryTests - xunit
        public AddressRepository(WebShopProjectContext context)
        {
            _context = context;
        }

        public async Task<List<Address>> GetAll()
        {
            // Retunere en liste af alle objekter af typen Addresses
            return await _context.Address
            .Include(users => users.User) // henter user objekt i forhold til foreign key
            .ToListAsync();
        }

        public async Task<Address> GetById(int addressesId)
        {
            // retunere et objekt af typen Addresses med id'et addressesId
            return await _context.Address
                .FirstOrDefaultAsync(lambdaVar => lambdaVar.Id == addressesId);
        }

        public async Task<Address> Create(Address addresses)
        {
            // Retunere samme input xD
            _context.Address.Add(addresses);
            await _context.SaveChangesAsync();
            return addresses;
        }

        public async Task<Address> Update(int updateTargetAddressId, Address updateThisAddress)
        {
            Address updatedAddress = await _context.Address
                .FirstOrDefaultAsync(address => address.Id == updateTargetAddressId);

            if (updatedAddress != null)
            {
                // updatedAddress.UserId = updateThisAddress.UserId;
                updatedAddress.AddressName = updateThisAddress.AddressName;
                updatedAddress.ZipCode = updateThisAddress.ZipCode;
                await _context.SaveChangesAsync();

                return updatedAddress;
            }

            return null;

        }
    
        public async Task<bool> Delete(int addressId)
        {

            Address address = await _context.Address.FirstOrDefaultAsync(
                address => address.Id == addressId);

            if (address != null)
            {
                _context.Address.Remove(address);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
