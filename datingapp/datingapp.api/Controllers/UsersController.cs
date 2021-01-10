using datingapp.api.DTOs;
using datingapp.api.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace datingapp.api.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _repo;

        public UsersController(IUserRepository repository)
        {
            _repo = repository;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            return Ok(await _repo.GetMembersAsync());
        }

       
        [HttpGet("{username}")]
        public async Task<MemberDto> GetUser(string username)
        {
            return await _repo.GeMemberByUsernameAsync(username);

        }
    }
}
