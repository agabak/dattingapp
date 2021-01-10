using AutoMapper;
using datingapp.api.DTOs;
using datingapp.api.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
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


        [HttpGet("{username}")]
        public async Task<MemberDto> GetUser(string username)
        {
            return await _repo.GeMemberByUsernameAsync(username);

        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberDto)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _repo.GetUserByUserNameAsync(username);

            _mapper.Map(memberDto, user);
            _repo.Update(user);

            if (await _repo.SaveAllAsync()) return NoContent();

            return BadRequest("Fail to update");
        }
    }
}
