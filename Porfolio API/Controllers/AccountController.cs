using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Repository.DTOs.Account;
using Repository.Entities;
using Repository.Interfaces;

namespace Porfolio_API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        //Sử dụng thằng UserManager của JWT
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Password sẽ nhận 2 đầu vào - userName và password 
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == dto.EmailOrUsername || u.UserName == dto.EmailOrUsername);
            if (user == null)
            {
                return Unauthorized("Username or email not found !!!");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized("Invalid password.");
            }

            //Đã login thành công thì return data từ user model
            return Ok( new NewUserDTO
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            });
        }


        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var appUser = new AppUser
                {
                    UserName = dto.UserName,
                    Email = dto.Email,
                };
                //Sử dụng CreateAsync() của _userManager và truyền vào user đã tạo
                var createUser = await _userManager.CreateAsync(appUser, dto.Password);
                if (createUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok(new NewUserDTO
                        {
                            UserName = appUser.UserName,
                            Email = appUser.Email,
                            Token = _tokenService.CreateToken(appUser)
                        });
                    } else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                } else
                {
                    return StatusCode(500, createUser.Errors);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
