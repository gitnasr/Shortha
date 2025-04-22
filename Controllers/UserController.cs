using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shortha.DTO;
using Shortha.Interfaces;
using Shortha.Models;

namespace Shortha.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppUser _user;
        private readonly IMapper _mapper;

        private readonly UserManager<AppUser> _userManager; 


        public UserController(AppUser user, IMapper mapper, UserManager<AppUser> _manager)
        {
            _user = user;
            _mapper = mapper;
            _userManager = _manager;
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
                    return Ok(new { Message = "User registered successfully." });
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
