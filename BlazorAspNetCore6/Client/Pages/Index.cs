using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
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
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "api/Stocks/StocksAsyncEnumerable");
            request.SetBrowserResponseStreamingEnabled(true);

            using HttpResponseMessage response = await Http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            response.EnsureSuccessStatusCode();

            var responseStream = await response.Content.ReadAsStreamAsync();

            IAsyncEnumerable<StockPriceDto> weatherForecasts = JsonSerializer.DeserializeAsyncEnumerable<StockPriceDto>(
     responseStream,
     new JsonSerializerOptions
     {
         PropertyNameCaseInsensitive = true,
         DefaultBufferSize = 120
     });

            await foreach (var item in weatherForecasts)
            {
                StockPricesIAsyncEnumerable.Add(item);
                StateHasChanged();
            }
        }


    }
}
