using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadLater5.JWT
{
    public class ValidateJwt
    {
        private IConfiguration _config { get; set; }

        public ValidateJwt(IConfiguration configuration)
        {
            _config = configuration;
        }
        public bool Validate(string token)
        {
            if (token == _config.GetValue<string>("JwtConfig:Secret-Key"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
