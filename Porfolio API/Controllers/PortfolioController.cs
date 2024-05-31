using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Repository.Interfaces;
using Repository.Repositories;
using Services.Extensions;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Porfolio_API.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepo _stockRepo;
        private readonly IPortfolioRepo _portfolioRepo;
        public PortfolioController(UserManager<AppUser> userManager, IStockRepo stockRepo, IPortfolioRepo portfolioRepo)
        {
            _userManager = userManager;
            _stockRepo = stockRepo;
            _portfolioRepo = portfolioRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            //Sử dụng class User để lấy thông tin từ claims
            var userName = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(userName);
            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);
            return Ok(userPortfolio);
        }
    }
}
