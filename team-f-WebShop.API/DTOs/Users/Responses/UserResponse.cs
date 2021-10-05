﻿using team_f_WebShop.API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace team_f_WebShop.API.DTO.Users.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

        public List<UserOrderListResponse> OrderList { get; set; } = new();

        public List<UserAddressResponse> Addresses { get; set; } = new();


    }
}
