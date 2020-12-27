using datingapp.api.DTOs;
using datingapp.api.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace datingapp.api.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<UserDto> RegisterAsync(RegisterDto register);
        Task<UserDto> LoginAsync(LoginDto login);
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserAsync(int id);
    }
}
