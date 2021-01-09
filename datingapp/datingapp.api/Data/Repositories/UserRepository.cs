using AutoMapper;
using AutoMapper.QueryableExtensions;
using datingapp.api.DTOs;
using datingapp.api.Entities;
using datingapp.api.Interfaces;
using datingapp.api.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace datingapp.api.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public UserRepository(
          DataContext context,ITokenService tokenService, IMapper mapper)
        {
            _context = context;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users
                                 .Include(x => x.Photos)
                                .ToListAsync();
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users
                                 .Include(x => x.Photos)
                                 .FirstOrDefaultAsync(u => u.Id == id);
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0; 
        }

        public async Task<AppUser> GetUserByUserNameAsync(string username)
        {
            return await _context.Users
                                 .Include(x => x.Photos)
                                 .SingleOrDefaultAsync(u => u.UserName.ToLower() == username.ToLower());
        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            return await _context.Users
                                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                                .ToListAsync();
        }

        public async Task<MemberDto> GeMemberByUsernameAsync(string username)
        {
            return await _context.Users
                                 .Where(u => u.UserName.ToLower() == username.ToLower())
                                 .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                                 .SingleOrDefaultAsync(); 
        }
    }
}
