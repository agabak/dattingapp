using datingapp.api.Entities;
using datingapp.api.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace datingapp.api.Controllers
{

    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _repo;
        public UsersController(IUserRepository repository)
        {
            _repo = repository;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users = await _repo.GetUsersAsync();
            return  Ok(users);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<AppUser> GetUser(int id)
        {
            return await _repo.GetUserAsync(id);
        }
    }
}
