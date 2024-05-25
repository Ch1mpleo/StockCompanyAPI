using Microsoft.AspNetCore.Mvc;
using Services.Mappers;
using Repository;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Repository.DTOs.Stock;
using Repository.Helpers;

namespace Porfolio_API.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepo _stockRepo;
        public StockController(ApplicationDBContext context, IStockRepo stockRepo)
        {
            _stockRepo = stockRepo;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObjectStock query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stocks = await _stockRepo.GetAllAsync(query);
            var stockDTO = stocks.Select(s => s.ToStockDTO());
            return Ok(stockDTO);
        }

        [HttpGet("{id:int}")]
        //sử dụng httpGet như trên sẽ giúp .net tự abstract được id và truyền vào param bên dưới
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stock = await _stockRepo.GetByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            var stockDTO = stock.ToStockDTO();
            return Ok(stockDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = dto.ToCreateStockRequestDTO();
            await _stockRepo.CreateAsync(stockModel);
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDTO());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = await _stockRepo.UpdateAsync(id, dto);
            if (stockModel == null)
            {
                return NotFound();
            }
            return Ok(stockModel.ToStockDTO());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = await _stockRepo.DeleteAsync(id);
            if (stockModel == null)
            { 
                return NotFound();
            }
            return NoContent();
        }
    }
}
