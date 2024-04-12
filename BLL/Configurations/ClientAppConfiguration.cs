using MassTransit;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Configurations
{
    public class ClientAppConfiguration
    {
        private readonly IConfiguration configuration;

        public ClientAppConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string Url => configuration["ClientUrl"];
        public string EmailConfirmationPath => configuration["ClientUrlEmailConfirmationPath"];
        public string ResetPasswordPath => configuration["ClientUrlEmailResetPasswordPath"];

        public string ResetPasswordMessage => configuration["ResetPasswordMessage"];

    }
}
