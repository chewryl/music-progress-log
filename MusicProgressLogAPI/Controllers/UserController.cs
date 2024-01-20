using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicProgressLogAPI.CustomActionFilters;
using MusicProgressLogAPI.Models.Domain;
using MusicProgressLogAPI.Models.DTO;
using MusicProgressLogAPI.Repositories.Interfaces;

namespace MusicProgressLogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository<ApplicationUser> _repository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserController(IRepository<ApplicationUser> repository, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _repository = repository;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("register")]
        [ValidateModel]
        public async Task<IActionResult> Register([FromBody] UserRequestDto registerUserDto)
        {
            var users = await _repository.GetAllAsync();
            if (users.Any(u => u.UserName == registerUserDto.UserName))
            {
                return StatusCode(409, $"UserName '{registerUserDto.UserName}' already exists in database.");
            }

            var identityUser = new ApplicationUser
            {
                UserName = registerUserDto.UserName,
            };

            var identityResult = await _userManager.CreateAsync(identityUser, registerUserDto.Password);

            if (identityResult.Succeeded)
            {
                return Ok($"User '{registerUserDto.UserName}' successfully registered. Please login.");
            }

            return StatusCode(500, "Registering user failed.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _repository.GetAllAsync();
            return Ok(users.Select(x => _mapper.Map<UserDto>(x)).ToList());
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

            return Ok(_mapper.Map<UserResponseDto>(user));
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
