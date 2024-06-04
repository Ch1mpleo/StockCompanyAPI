using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Repository;
using Repository.DTOs.Comment;
using Repository.Entities;
using Repository.Helpers;
using Repository.Interfaces;
using Services.Extensions;
using Services.Mappers;

namespace Porfolio_API.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepo _commentRepo;
        private readonly IStockRepo _stockRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFMPService _fmpService;
        public CommentController(ICommentRepo commentRepo, IStockRepo stockRepo, UserManager<AppUser> userManager, IFMPService fmpService)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
            _userManager = userManager;
            _fmpService = fmpService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObjectComment query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comments = await _commentRepo.GetAllAsync(query);
            var commentDTO = comments.Select(c => c.ToCommentDTO());
            return Ok(commentDTO);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await _commentRepo.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            var commentDTO = comment.ToCommentDTO();
            return Ok(commentDTO);
        }
        [HttpPost("{symbol:alpha}")]
        public async Task<IActionResult> Create([FromRoute] string symbol, CreateCommentDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

/*            if (!await _stockRepo.StockExist(symbol))
            {
                return BadRequest("Stock not found !!!");
            }*/

            //1. Validate xem stock có tồn tại trong thực tế ko 
            //2. If not -> BadRequest 
            //3. If yes -> CreateAsync stock đó
            var stock = await _stockRepo.GetBySymbolAsync(symbol);
            if (stock == null)
            {
                stock = await _fmpService.FindStockBySymbolAsync(symbol);
                if (stock == null)
                {
                    return BadRequest("Stock does not exists");
                }
                else
                {
                    await _stockRepo.CreateAsync(stock);
                }
            }

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var comment = dto.ToCommentFromCreate(stock.Id);

            comment.AppUserId = appUser.Id;

            await _commentRepo.CreateAsync(comment);
            return CreatedAtAction(nameof(GetById), new { id = comment.Id }, comment.ToCommentDTO());
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await _commentRepo.UpdateAsync(id, dto.ToCommentFromUpdate(id));
            if (comment == null)
            {
                return NotFound("Comment not found !!!");
            }
            return Ok(comment.ToCommentDTO());
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Remove([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await _commentRepo.DeleteAsync(id);
            if (comment == null)
            {
                return NotFound("Comment is not exist !!!");
            }
            return Ok(comment);
        }
    } 
}
