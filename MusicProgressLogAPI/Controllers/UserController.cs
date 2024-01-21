using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        private readonly ITokenRepository _tokenRepository;
        private readonly IMapper _mapper;

        public UserController(
            IRepository<ApplicationUser> repository,
            UserManager<ApplicationUser> userManager,
            ITokenRepository tokenRepository,
            IMapper mapper)
        {
            _repository = repository;
            _userManager = userManager;
            _tokenRepository = tokenRepository;
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

        [HttpPost]
        [Route("login")]
        [ValidateModel]
        public async Task<IActionResult> Login([FromBody] UserRequestDto loginUserDto)
        {
            var user = await _userManager.FindByNameAsync(loginUserDto.UserName);
            if (user == null)
            {
                return NotFound($"Acount with username '{loginUserDto.UserName}' does not exist. Please register first");
            }

            if (await _userManager.CheckPasswordAsync(user, loginUserDto.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user) ?? new List<string>();
                var jwt = _tokenRepository.CreateJwt(user, userRoles.ToList());
                return Ok(new LoginResponseDto
                {
                    Token = jwt,
                });
            }

            return BadRequest("Log in failed.");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var users = await _repository.GetAllAsync();
            return Ok(users.Select(x => _mapper.Map<UserDto>(x)).ToList());
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize]
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
        [Authorize]
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
