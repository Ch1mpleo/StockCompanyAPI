﻿using Microsoft.AspNetCore.Authorization;
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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            //Sử dụng claim để lấy username như trên 
            var userName = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(userName);
            var stock = await _stockRepo.GetBySymbolAsync(symbol);

            if (stock == null) return BadRequest("Stock not found !!!");

            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);

            //Validate xem có stock bị trùng ko (ko cho tạo stock đã có) 
            if (userPortfolio.Any(e => e.Symbol.ToLower() == symbol.ToLower()))
                return BadRequest("Can not add same stock to portfolio !!!");

            //Nếu đã pass các validate thì create 
            var portfolioModel = new Portfolio
            {
                StockId = stock.Id,
                AppUserId = appUser.Id,
            };

            await _portfolioRepo.CreateAsync(portfolioModel);
            if (portfolioModel == null)
            {
                return StatusCode(500, "Could not create");
            } else
            {
                return Created();
            }
        }
    }
}
