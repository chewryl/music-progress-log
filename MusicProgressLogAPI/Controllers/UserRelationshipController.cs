using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicProgressLogAPI.CustomActionFilters;
using MusicProgressLogAPI.Models.Domain;
using MusicProgressLogAPI.Models.DTO;
using MusicProgressLogAPI.Repositories.Interfaces;

namespace MusicProgressLogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRelationshipController : ControllerBase
    {
        private readonly IRepository<UserRelationship> _repository;
        private readonly IMapper _mapper;

        public UserRelationshipController(IRepository<UserRelationship> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _repository.GetAllAsync();
            return Ok(users.Select(x => _mapper.Map<UserRelationshipDto>(x)).ToList());
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound(id);
            }
            return Ok(user);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] UserRelationshipDto userRelationshipDto)
        {
            var users = await _repository.GetAllAsync();
            if (users.Any(u => u.UserName == userRelationshipDto.UserName)) 
            {
                return StatusCode(409, $"UserName '{userRelationshipDto.UserName}' already exists in database.");
            }

            return Ok(await _repository.CreateAsync(_mapper.Map<UserRelationship>(userRelationshipDto)));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedUserId = await _repository.DeleteAsync(id);
            if (deletedUserId == null)
            {
                return NotFound(id);
            }

            return Ok(deletedUserId);
        }
    }
}
