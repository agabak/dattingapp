using datingapp.api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace datingapp.api.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
