using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Repository.DTOs.Account;
using Repository.Entities;

namespace Porfolio_API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        //Sử dụng thằng UserManager của JWT
        private readonly UserManager<AppUser> _userManager;
        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
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
                        return Ok("User created !!!");
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
