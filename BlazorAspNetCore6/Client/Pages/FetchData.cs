using BlazorAspNetCore6.Shared;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Project.Data.Entities;
using System.Text.Json;

namespace BlazorAspNetCore6.Client.Pages
{
    public partial class FetchData
    {
        private List<WeatherForecast> forecasts = new List<WeatherForecast>();
        private string a = "";


        private async Task GetFromDBAsyncEnumerable()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "weatherforecast/weather-forecast");
            request.SetBrowserResponseStreamingEnabled(true);

            using HttpResponseMessage response = await Http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            response.EnsureSuccessStatusCode();

            var responseStream = await response.Content.ReadAsStreamAsync();

            IAsyncEnumerable<WeatherForecast> weatherForecasts = JsonSerializer.DeserializeAsyncEnumerable<WeatherForecast>(
     responseStream,
     new JsonSerializerOptions
     {
         PropertyNameCaseInsensitive = true,
         DefaultBufferSize = 128
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
