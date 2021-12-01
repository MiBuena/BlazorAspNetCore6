using BlazorAspNetCore6.Shared;
using Project.Data.Entities;
using System.Text.Json;

namespace BlazorAspNetCore6.Client.Pages
{
    public partial class FetchData
    {
        private List<WeatherForecast> forecasts = new List<WeatherForecast>();
        private string a = "";


        private async void GetFromDBAsyncEnumerable()
        {
            using HttpResponseMessage response = await Http.GetAsync("api/Stocks/StocksAsyncEnumerable", HttpCompletionOption.ResponseHeadersRead);

            response.EnsureSuccessStatusCode();

            var responseStream = await response.Content.ReadAsStreamAsync();

            IAsyncEnumerable<WeatherForecast> weatherForecasts = JsonSerializer.DeserializeAsyncEnumerable<WeatherForecast>(
     responseStream,
     new JsonSerializerOptions
     {
         PropertyNameCaseInsensitive = true,
         DefaultBufferSize = 2
     });

            await foreach (var item in weatherForecasts)
            {
                forecasts.Add(item);
                StateHasChanged();
            }
        }

        public async Task GetLines()
        {
            var responseStream = await Http.GetStreamAsync($"weatherforecast/lines"); // buffers for 10 s
            var linesAsync = JsonSerializer.DeserializeAsyncEnumerable<string>(responseStream, new JsonSerializerOptions() { DefaultBufferSize = 1});
            await foreach (var line in linesAsync)
            {
                Console.WriteLine(line);
            }
        }
    }
}
