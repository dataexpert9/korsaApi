﻿using System;
using AppModel.DTOs;
using Microsoft.Extensions.Configuration;

namespace Wasalee.JwtHelpers
{
    public static class JwtExtensions
    {
        public static void GenerateToken(this UserDTO user, IConfiguration configuration)
        {
            try
            {
                var token = new JwtTokenBuilder()
                                .AddSecurityKey(JwtSecurityKey.Create(configuration.GetValue<string>("JwtSecretKey")))
                                .AddIssuer(configuration.GetValue<string>("JwtIssuer"))
                                .AddAudience(configuration.GetValue<string>("JwtAudience"))
                                .AddExpiry(30)
                                .AddClaim("Id", user.Id.ToString())
                                .AddRole("User")
                                .Build();

                user.Token = token.Value;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void GenerateToken(this DriverDTO driver, IConfiguration configuration)
        {
            try
            {
                var token = new JwtTokenBuilder()
                                .AddSecurityKey(JwtSecurityKey.Create(configuration.GetValue<string>("JwtSecretKey")))
                                .AddIssuer(configuration.GetValue<string>("JwtIssuer"))
                                .AddAudience(configuration.GetValue<string>("JwtAudience"))
                                .AddExpiry(30)
                                .AddClaim("Id", driver.Id.ToString())
                                .AddRole("Driver")
                                .Build();

                driver.Token = token.Value;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static void GenerateToken(this AdminDTO admin, IConfiguration configuration)
        {
            try
            {
                var token = new JwtTokenBuilder()
                                .AddSecurityKey(JwtSecurityKey.Create(configuration.GetValue<string>("JwtSecretKey")))
                                .AddIssuer(configuration.GetValue<string>("JwtIssuer"))
                                .AddAudience(configuration.GetValue<string>("JwtAudience"))
                                .AddExpiry(30)
                                .AddClaim("Id", admin.Id.ToString())
                                .AddRole("Admin")
                                .Build();

                admin.Token = new TokenDTO();
                admin.Token.access_token = token.Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
