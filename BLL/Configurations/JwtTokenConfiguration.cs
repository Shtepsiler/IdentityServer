﻿using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BLL.Configurations
{
    public class JwtTokenConfiguration
    {
        private readonly IConfiguration configuration;

        public string Issuer => configuration["JwtIssuer"];

        public string Audience => configuration["JwtAudience"];

        public  DateTime ExpirationDate => DateTime.UtcNow.AddHours(1);

        public SymmetricSecurityKey Key =>
            new(Encoding.UTF8.GetBytes(configuration["JwtSecurityKey"]));

        public SigningCredentials Credentials =>
            new(Key, SecurityAlgorithms.HmacSha256);

        public JwtTokenConfiguration(IConfiguration configuration) =>
            this.configuration = configuration;
    }
}
