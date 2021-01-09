using AutoMapper;
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
        private readonly IMapper _mapper;
        public UsersController(IUserRepository repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            return Ok(await _repo.GetMembersAsync());
        }

        [Authorize]
        [HttpGet("{username}")]
        public async Task<MemberDto> GetUser(string username)
        {
            return await _repo.GeMemberByUsernameAsync(username);

        }
    }
}
