using datingapp.api.DTOs;
using datingapp.api.Entities;
using datingapp.api.Interfaces;
using datingapp.api.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace datingapp.api.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public AccountRepository(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }
        public async Task<UserDto> LoginAsync(LoginDto login)
        {
            var user = await _context.Users
                                     .SingleOrDefaultAsync<AppUser>
                                     (u => u.UserName == login.Username.ToLower());

            if (user == null) throw new Exception("Invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) throw new Exception("Invalid password");
            }

            return new UserDto { Username = user.UserName, Token = _tokenService.CreateToken(user) };
        }

        public async Task<UserDto> RegisterAsync(RegisterDto register)
        {
            if (await IsUserExit(register.Username)) throw new Exception("user already exist in our system");

            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                UserName = register.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.Password)),
                PasswordSalt = hmac.Key
            };
            await _context.AddAsync<AppUser>(user);
            await _context.SaveChangesAsync();
            return new UserDto { Username = user.UserName, Token = _tokenService.CreateToken(user) };
        }

        private async Task<bool> IsUserExit(string username)
        {
            return await _context.Users.AnyAsync(u => u.UserName == username.ToLower());
        }
    }
}
