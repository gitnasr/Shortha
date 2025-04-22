using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Shortha.DTO;
using Shortha.Interfaces;
using Shortha.Models;
using Shortha.Providers;

namespace Shortha.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly JwtProvider _tokenProvider;
        private readonly IMapper _mapper;

        private readonly UserManager<AppUser> _userManager; 


        public UserController( IMapper mapper, UserManager<AppUser> _manager,
            JwtProvider jwtProvider)
        {
            _mapper = mapper;
            _userManager = _manager;
            _tokenProvider = jwtProvider;
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutRequestPayload logoutRequest)
        {
            // Invalidate the Token
           await _tokenProvider.BlacklistToken(logoutRequest.Token);
            return Ok(new { Message = "Logout successful." });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestPayload loginRequest)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(loginRequest.Username);
                if (user != null && await _userManager.CheckPasswordAsync(user, loginRequest.Password))
                {
                    // Generate JWT token
                    var token = _tokenProvider.GenerateToken(user);
                    // Return token in response
                    return Ok(new { Message = "Login successful.", Token = token });
                }
                else
                {
                    return Unauthorized(new ErrorResponse(HttpStatusCode.Unauthorized,"Invalid Email or Password has been Provided!"));
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<AppUser>(registerRequest);
                var result = await _userManager.CreateAsync(user, registerRequest.Password);
                if (result.Succeeded)
                {
                    // Generate JWT token
                    var token = _tokenProvider.GenerateToken(user);
                    // Return token in response
                    return Ok(new { Message = "User registered successfully.", Token= token });
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


    }
}
