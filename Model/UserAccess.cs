using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FidelityTest.Model
{
    public class UserAccess
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }

    public class Tokens
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
