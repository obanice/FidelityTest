using FidelityTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FidelityTest.Repository
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(UserAccess users);

    }
}
