using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SavingsAccountWebAPI.DTOs;
using SavingsAccountWebAPI.Model;
using SavingsAccountWebAPI.Services.Interface;

namespace SavingsAccountWebAPI.Controllers
{
    [ApiController]
    [Route("api/Users")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;

        public UserController(IUserRepository userRepository, IPasswordService passwordService, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _tokenService = tokenService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> CreateUser(UserRequestDTO request)
        {
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = _passwordService.HashPassword(request.Password)
            };

            if (!await _userRepository.IsValidEmail(user.Email))
            {
                return BadRequest("Incorrect Email!");
            }

            if (await _userRepository.CheckIfEmailExists(user.Email))
            {
                return BadRequest("Email already exists!");
            }

            var result = await _userRepository.Create(user);
            var registeredUser = result.Adapt<UserResponseDTO>();
            var token = _tokenService.CreateUserToken(registeredUser);

            return Ok(new AuthResponseDTO(user.Id, token));
        }


        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser(UserLoginRequestDTO request)
        {
            var user = await _userRepository.GetUserByEmail(request.Email);
            var password = request.Password;
            if (user == null)
            {
                return NotFound("Incorrect Email or Password!");
            }
            if (user.DeletedAt != null)
            {
                return NotFound("Incorrect Email or Password!");
            }
            if (!_passwordService.VerifyPassword(password, user.Password))
            {
                return BadRequest("Incorrect Email or Password!");
            }

            var loginUser = user.Adapt<UserResponseDTO>();
            var token = _tokenService.CreateUserToken(loginUser);

            return Ok(new AuthResponseDTO(user.Id, token));

        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userRepository.GetAll();
            return Ok(result.Adapt<List<UserResponseDTO>>());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var targetUser = await _userRepository.GetById(id);

            if (targetUser == null)
            {
                return NotFound("User not found!");
            }
            return Ok(targetUser.Adapt<UserResponseDTO>());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, UserRequestDTO request)
        {
            var targetUser = await _userRepository.GetById(id);

            if (targetUser == null)
            {
                return NotFound("User not found!");
            }

            targetUser = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = _passwordService.HashPassword(request.Password)
            };

            return Ok(await _userRepository.Update(id, targetUser));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            var targetUser = await _userRepository.GetById(id);

            if (targetUser == null)
            {
                return NotFound("User not found!");
            }

            await _userRepository.DeleteById(id);

            return Ok("User deleted successfully!");
        }

    }
}
