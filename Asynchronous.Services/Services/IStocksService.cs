using Project.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsynchronousWebApiProject.Services
{
    public interface IStocksService
    {
        Task AddStocksToDB();

        Task<string> GetAllStockPricesFromFile();

        IAsyncEnumerable<StockPrice> GetAllStockPricesA();

        Task<string> GetAllStockPricesFromFile4();

        Task AddStocksToDB1234();

        List<StockPrice> GetStocksFromDBSynchronous();

        Task<List<StockPrice>> GetStocksFromDBAsynchronous();

        IAsyncEnumerable<StockPrice> GetAllStocksFromDBIAsyncEnumerable();
    }
}
