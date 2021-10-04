using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrangularAPI.DTO.Addresses.Responses
{
    public class AddressResponse
    {
        public int Id { get; set; }

        public string Address { get; set; }

        public int ZipCode { get; set; }

        public string CityName { get; set; }

        public AddressUserResponse User { get; set; }
    }
}
