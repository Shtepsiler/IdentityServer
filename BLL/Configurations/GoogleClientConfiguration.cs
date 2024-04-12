using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Configurations
{
    public class GoogleClientConfiguration
    {
        private readonly IConfiguration configuration;

        public string GoogleClientID => configuration["GoogleClientID"];
        public string GoogleClientSecret => configuration["GoogleClientSecret"];
        public GoogleClientConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
    }
}
