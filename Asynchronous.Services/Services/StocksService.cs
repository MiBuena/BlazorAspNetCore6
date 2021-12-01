using Microsoft.EntityFrameworkCore;
using Project.Data.Entities;
using Project.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AsynchronousWebApiProject.Services
{
    public class StocksService : IStocksService
    {
        private readonly IRepository<StockPrice> _stockRepository;

        public StocksService(IRepository<StockPrice> stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public List<StockPrice> GetStocksFromDBSynchronous()
        {
            var stockPrices =  _stockRepository
                .All()
                .Take(100000)
                .ToList();

            return stockPrices;
        }

        public async Task<List<StockPrice>> GetStocksFromDBAsynchronous()
        {
            var stockPrices = await _stockRepository
                .All()
                .ToListAsync();

            return stockPrices;
        }


        public async IAsyncEnumerable<StockPrice> GetAllStocksFromDBIAsyncEnumerable()
        {
            var stockPrices = await _stockRepository
                .All()
                .Take(10)
                .ToListAsync();

            foreach (var stockPrice in stockPrices)
            {
                yield return stockPrice;
            }
        }


        public async Task AddStocksToDB1234()
        {
            var stockPrices = await _stockRepository.All().ToListAsync();
        }

        public async Task<string> GetAllStockPricesFromFile4()
        {

            var lines = await File.ReadAllTextAsync("StockPrices_Small.csv");


            return lines;

        }

        public async Task<string> GetAllStockPricesFromFile()
        {
            using (var stream = new StreamReader(File.OpenRead("StockPrices_Small.csv")))
            {
                var lines = new List<string>();

                string line;
                while ((line = await stream.ReadLineAsync()) != null)
                {
                    if (lines.Count > 100000)
                    {
                        break;
                    }
                    lines.Add(line);
                }

                return string.Join(',', lines); ;
            }
        }

        public async IAsyncEnumerable<StockPrice> GetAllStockPricesA()
        {
            await Task.Delay(500);

            yield return new StockPrice { Ticker = "MSFT", Change = 0.5m };

            await Task.Delay(500);

            yield return new StockPrice { Ticker = "MSFT", Change = 0.2m };

            await Task.Delay(500);

            yield return new StockPrice { Ticker = "GOOG", Change = 0.3m };

            await Task.Delay(500);

            yield return new StockPrice { Ticker = "GOOG", Change = 0.8m };

            await Task.Delay(500);

            yield return new StockPrice { Ticker = "MSFT", Change = 0.5m };

            await Task.Delay(500);

            yield return new StockPrice { Ticker = "MSFT", Change = 0.2m };

            await Task.Delay(500);

            yield return new StockPrice { Ticker = "GOOG", Change = 0.3m };

            await Task.Delay(500);

            yield return new StockPrice { Ticker = "GOOG", Change = 0.8m };
            await Task.Delay(500);

            yield return new StockPrice { Ticker = "MSFT", Change = 0.5m };

            await Task.Delay(500);

            yield return new StockPrice { Ticker = "MSFT", Change = 0.2m };

            await Task.Delay(500);

            yield return new StockPrice { Ticker = "GOOG", Change = 0.3m };

            await Task.Delay(500);

            yield return new StockPrice { Ticker = "GOOG", Change = 0.8m };
        }


        public async Task AddStocksToDB()
        {
            var stockPrices = await GetStockPrices();

            _stockRepository.AddRange(stockPrices);

            await _stockRepository.SaveChangesAsync();
        }

        private async Task<List<StockPrice>> GetStockPrices()
        {
            var lines = await File.ReadAllLinesAsync("StockPrices_Small.csv");

            lines = await File.ReadAllLinesAsync("StockPrices_Small.csv");

            lines = await File.ReadAllLinesAsync("StockPrices_Small.csv");


            lines = await File.ReadAllLinesAsync("StockPrices_Small.csv");

            lines = await File.ReadAllLinesAsync("StockPrices_Small.csv");


            lines = await File.ReadAllLinesAsync("StockPrices_Small.csv");

            lines = await File.ReadAllLinesAsync("StockPrices_Small.csv");


            lines = await File.ReadAllLinesAsync("StockPrices_Small.csv");

            lines = await File.ReadAllLinesAsync("StockPrices_Small.csv");


            lines = await File.ReadAllLinesAsync("StockPrices_Small.csv");

            lines = await File.ReadAllLinesAsync("StockPrices_Small.csv");


            lines = await File.ReadAllLinesAsync("StockPrices_Small.csv");

            lines = await File.ReadAllLinesAsync("StockPrices_Small.csv");

            lines = await File.ReadAllLinesAsync("StockPrices_Small.csv");

            lines = await File.ReadAllLinesAsync("StockPrices_Small.csv");


            lines = await File.ReadAllLinesAsync("StockPrices_Small.csv");

            lines = await File.ReadAllLinesAsync("StockPrices_Small.csv");


            lines = await File.ReadAllLinesAsync("StockPrices_Small.csv");

            lines = await File.ReadAllLinesAsync("StockPrices_Small.csv");

            lines = await File.ReadAllLinesAsync("StockPrices_Small.csv");

            lines = await File.ReadAllLinesAsync("StockPrices_Small.csv");


            lines = await File.ReadAllLinesAsync("StockPrices_Small.csv");

            lines = await File.ReadAllLinesAsync("StockPrices_Small.csv");


            lines = await File.ReadAllLinesAsync("StockPrices_Small.csv");

            lines = await File.ReadAllLinesAsync("StockPrices_Small.csv");

            lines = await File.ReadAllLinesAsync("StockPrices_Small.csv");

            lines = await File.ReadAllLinesAsync("StockPrices_Small.csv");


            lines = await File.ReadAllLinesAsync("StockPrices_Small.csv");

            lines = await File.ReadAllLinesAsync("StockPrices_Small.csv");


            lines = await File.ReadAllLinesAsync("StockPrices_Small.csv");

            lines = await File.ReadAllLinesAsync("StockPrices_Small.csv");


            var linesWithoutHeader = lines.Skip(1);

            var stockPrices = new List<StockPrice>();

            foreach (var item in linesWithoutHeader)
            {
                stockPrices.Add(FromCSV(item));
            }

            return stockPrices;
        }

        private StockPrice FromCSV(string text)
        {
            var segments = text.Split(',');

            for (var i = 0; i < segments.Length; i++)
            {
                segments[i] = segments[i].Trim('\'', '"');
            }

            var price = new StockPrice
            {
                Ticker = segments[0],
                TradeDate = DateTime.ParseExact(segments[1], "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture),
                Volume = Convert.ToInt32(segments[6], CultureInfo.InvariantCulture),
                Change = Convert.ToDecimal(segments[7], CultureInfo.InvariantCulture),
                ChangePercent = Convert.ToDecimal(segments[8], CultureInfo.InvariantCulture),
            };

            return price;
        }
    }
}
