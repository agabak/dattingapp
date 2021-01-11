using AutoMapper;
using datingapp.api.DTOs;
using datingapp.api.Entities;
using datingapp.api.Extensions;
using datingapp.api.Interfaces;
using datingapp.api.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly IPhotoService _photoService;

        public UsersController(IUserRepository repository,
            IMapper mapper, IPhotoService photoService)
        {
            _repo = repository;
            _mapper = mapper;
            _photoService = photoService;
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
          
            var user = await _repo.GetUserByUserNameAsync(User.GetUsername());

            _mapper.Map(memberDto, user);
            _repo.Update(user);

            if (await _repo.SaveAllAsync()) return NoContent();

            return BadRequest("Fail to update");
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            var user = await _repo.GetUserByUserNameAsync(User.GetUsername());
            var result = await _photoService.AddPhotoAsync(file);
            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            if (user.Photos.Count == 0) photo.IsMain = true;
            user.Photos.Add(photo);
            if (await _repo.SaveAllAsync()) return _mapper.Map<PhotoDto>(photo);

            return BadRequest("Problem adding photo");

        }
    }
}
