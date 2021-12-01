using Microsoft.AspNetCore.Components;
using Project.Data.Entities;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorAspNetCore6.Client.Pages
{
    public partial class Index
    {
        [Inject]
        private HttpClient Http { get; set; }


        private List<StockPriceDto> StockPricesSynchronous { get; set; } = new List<StockPriceDto>();

        private List<StockPriceDto> StockPricesAsynchronous { get; set; } = new List<StockPriceDto>();


        private ICollection<StockPriceDto> StockPricesIAsyncEnumerable { get; set; } = new List<StockPriceDto>();



        private async void GetFromDBSynchronous()
        {
            StockPricesSynchronous = await Http.GetFromJsonAsync<List<StockPriceDto>>("api/Stocks/StocksSynchronous");

        }

        private async void GetFromDBAsynchronous()
        {
            StockPricesAsynchronous = await Http.GetFromJsonAsync<List<StockPriceDto>>("api/Stocks/StocksAsynchronous");
        }


        private async void GetFromDBAsyncEnumerable()
        {
            using HttpResponseMessage response = await Http.GetAsync("api/Stocks/StocksAsyncEnumerable", HttpCompletionOption.ResponseHeadersRead);

            response.EnsureSuccessStatusCode();

            var responseStream = await response.Content.ReadAsStreamAsync();

            IAsyncEnumerable<StockPriceDto> weatherForecasts = JsonSerializer.DeserializeAsyncEnumerable<StockPriceDto>(
     responseStream,
     new JsonSerializerOptions
     {
         PropertyNameCaseInsensitive = true,
         DefaultBufferSize = 20
     });

            await foreach (var item in weatherForecasts)
            {
                StockPricesIAsyncEnumerable.Add(item);
                StateHasChanged();
            }
        }


    }
}
