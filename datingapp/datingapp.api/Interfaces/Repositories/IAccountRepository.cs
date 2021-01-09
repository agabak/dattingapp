using datingapp.api.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace datingapp.api.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        Task<UserDto> RegisterAsync(RegisterDto register);
        Task<UserDto> LoginAsync(LoginDto login);
    }
}
