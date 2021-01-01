using datingapp.api.DTOs;
using datingapp.api.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace datingapp.api.Controllers
{

    public class AccountController : BaseApiController
    {
        private readonly IUserRepository _repo;
        public AccountController(IUserRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto register)
        {
            UserDto user = null;
            try
            {
                 user = await _repo.RegisterAsync(register);
            }
            catch (Exception ex)
            {
                 return BadRequest(ex.Message);
            }
            return user;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto login)
        {
            UserDto user = null;
            try
            {
                 user = await _repo.LoginAsync(login);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }

            return user;
        } 
    }
}
