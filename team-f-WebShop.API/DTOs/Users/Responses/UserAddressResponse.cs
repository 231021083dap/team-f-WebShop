﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace team_f_WebShop.API.DTO.Users.Responses
{
    public class UserAddressResponse
    {
        public int AddressId { get; set; }

        public string Address { get; set; }

        public int ZipCode { get; set; }

        public string CityName { get; set; }

    }
}
