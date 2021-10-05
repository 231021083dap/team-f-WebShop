using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using team_f_WebShop.API.Helpers;
using team_f_WebShop.API.Services.UsersService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace team_f_WebShop.API.Authorization
{
    
        public class JwtMiddleware
        {
            private readonly RequestDelegate _next;
            private readonly AppSettings _appSettings;

            public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
            {
                _next = next;
                _appSettings = appSettings.Value;
            }

            public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
            {
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var userId = jwtUtils.ValidateJwtToken(token);
                if (userId != null)
                {
                    // attach user to context on successful jwt validation
                    context.Items["User"] = await userService.GetById(userId.Value);
                }
                await _next(context);
            }
        }
    
}
