using Bogus;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Helpers;

namespace Porfolio_API.Controllers
{
    [Route("api/dummydata/stock")]
    [ApiController]
    public class DummyDataController : ControllerBase
    {
        private readonly DummyDataGenerator _dummy;
        public DummyDataController(DummyDataGenerator dummy)
        {
            _dummy = dummy;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var stocks = _dummy.GenerateStocksWithComments(10,3).ToList();
            return Ok(stocks);
        }

        /*[HttpGet("id")]
        public IActionResult GetById([FromRoute] int id)
        {
            var stocks = _dummy.GenerateStock();
            var stock = stocks.First(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock);
        } */
    }
}
