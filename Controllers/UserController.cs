using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shortha.DTO;
using Shortha.Interfaces;
using Shortha.Models;
using System.Net;

namespace Shortha.Controllers
{
    [Route("api/user")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IJwtProvider _tokenProvider;
        private readonly IMapper _mapper;
        private readonly SubscriptionRepository _subscriptionRepository;

        private readonly UserManager<AppUser> _userManager;


        public UserController(IMapper mapper, UserManager<AppUser> _manager,
            IJwtProvider jwtProvider)
        {
            _mapper = mapper;
            _userManager = _manager;
            _tokenProvider = jwtProvider;
            _subscriptionRepository = subscriptionRepository;


        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutRequestPayload logoutRequest)
        {
            // Invalidate the Token though the blacklist system.
            await _tokenProvider.BlacklistToken(logoutRequest.Token);
            return Ok();
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestPayload loginRequest)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(loginRequest.Username);
                if (user != null && await _userManager.CheckPasswordAsync(user, loginRequest.Password))
                {


                    var RoleList = await _userManager.GetRolesAsync(user);
                    if (RoleList.Count == 0)
                    {
                        return Unauthorized(new ErrorResponse(HttpStatusCode.Unauthorized, "User has no role assigned!"));
                    }

                    // Check the user role

                    var userRole = RoleList.First();

                    // Generate JWT token

                    var token = _tokenProvider.GenerateToken(user, userRole);
                    // Return token in response
                    return Ok(new { Message = "Login successful.", Token = token });
                }
                else
                {
                    return Unauthorized(new ErrorResponse(HttpStatusCode.Unauthorized, "Invalid Email or Password has been Provided!"));
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestPayload registerRequest)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<AppUser>(registerRequest);
                var result = await _userManager.CreateAsync(user, registerRequest.Password);
                if (result.Succeeded)
                {
                    //Assgin Role to user
                    await _userManager.AddToRoleAsync(user, "Normal");
                    // Generate JWT token
                    var token = _tokenProvider.GenerateToken(user, "Normal");
                    // Return token in response
                    return Ok(new { Message = "User registered successfully.", Token = token });
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }



        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetMe()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            

            if (user == null)
                return NotFound(new ErrorResponse(HttpStatusCode.NotFound, "User not found."));
            Subscription? Subscription = await _subscriptionRepository.GetSubscriptionByUserId(userId);

            var subscription = await _subscriptionRepository.GetSubscriptionByUserId(userId);

            var userDto = _mapper.Map<UserResponse>(user);

            if (subscription != null)
            {
                userDto.Subscription = _mapper.Map<SubscriptionResponse>(subscription);
            }
            return Ok(userDto);
        }



    }
}
