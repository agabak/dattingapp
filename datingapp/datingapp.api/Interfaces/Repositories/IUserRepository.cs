using datingapp.api.DTOs;
using datingapp.api.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace datingapp.api.Interfaces.Repositories
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByIdAsync(int id);
        Task<AppUser> GetUserByUserNameAsync(string username);
        Task<IEnumerable<MemberDto>> GetMembersAsync();
        Task<MemberDto> GeMemberByUsernameAsync(string username);
    }
}
