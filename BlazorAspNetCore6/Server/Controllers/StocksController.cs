using AsynchronousWebApiProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsynchronousWebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IStocksService _stocksService;

        public StocksController(IStocksService stocksService)
        {
            this._stocksService = stocksService;
        }


        [HttpGet("StocksSynchronous")]
        public ActionResult<List<StockPrice>> GetAllStocksFromDBSynchronous()
        {
            var a = _stocksService.GetStocksFromDBSynchronous();

            return Ok(a);
        }

        [HttpGet("StocksAsynchronous")]
        public async Task<ActionResult<IEnumerable<StockPrice>>> GetAllStocksFromDBAsynchronous()
        {
            var a = await _stocksService.GetStocksFromDBAsynchronous();

            return Ok(a);
        }


        [HttpGet("StocksAsyncEnumerable")]
        public async IAsyncEnumerable<StockPrice> GetAllStocksFromDBIAsyncEnumerable()
        {
            var a = _stocksService.GetAllStocksFromDBIAsyncEnumerable();

            await foreach (var product in a)
            {
                await Task.Delay(2000);
                yield return product;
            }
        }

        [HttpGet("stocks1234")]
        public async Task<ActionResult<string>> GetAllStocksFromDB1234()
        {
            await _stocksService.AddStocksToDB1234();

            return Ok();
        }

        [HttpGet("stocksA")]
        public async Task<ActionResult<string>> GetAllStocksFromDB1()
        {
            var a = await _stocksService.GetAllStockPricesFromFile();

            return Ok(a);
        }

        [HttpGet("stocksB")]
        public async IAsyncEnumerable<StockPrice> GetAllStocksFromDB2()
        {
            var enumerator = _stocksService.GetAllStockPricesA();

            await foreach (var price in enumerator)
            {
                yield return price;
            }
        }

        [HttpGet("stocksM")]
        public async Task<ActionResult<string>> GetAllStocksFromDB4()
        {
            var a = await _stocksService.GetAllStockPricesFromFile4();

            return Ok(a);
        }


        [HttpPost("stocks")]
        public async Task<ActionResult> AddStockPricesToDb()
        {
            await _stocksService.AddStocksToDB();

            return Ok();
        }
    }
}
