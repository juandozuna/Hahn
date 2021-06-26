namespace Hahn.ApplicationProcess.February2021.Data.Repositories
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Domain.Models;
    using Domain.Repositories;
    using Newtonsoft.Json;
    using JsonSerializer = System.Text.Json.JsonSerializer;

    /// <summary>
    /// Implementation of <see cref="ICountryRepository"/>
    /// </summary>
    public sealed class CountryRepository : ICountryRepository
    {
        private const string baseUrl = "https://restcountries.eu/rest/v2";
        private readonly HttpClient _client;

        /// <summary>
        /// Creates new instance of <see cref="CountryRepository"/>
        /// </summary>
        public CountryRepository(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient();
            _client.BaseAddress = new Uri(baseUrl);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Country>> GetAllCountries()
        {
            var request = BuildRequest("/all");

            var response = await _client.SendAsync(request);

            if (!response.IsSuccessStatusCode) throw new InvalidOperationException("Unsuccessful Request");

             var responseStream = await response.Content.ReadAsStringAsync();
             var deserialized = JsonConvert.DeserializeObject<IEnumerable<Country>>(responseStream);

             return deserialized;
        }

        /// <inheritdoc />
        public async Task<bool> IsCountryValid(string countryName)
        {
            var request = BuildRequest($"/name/{countryName}");

            var response = await _client.SendAsync(request);

            var foundData = await response.Content.ReadAsStringAsync();

            return !(foundData.Contains(@"status") && foundData.Contains("404"));
        }

        private static HttpRequestMessage BuildRequest(string route)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/rest/v2{route}");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            return request;
        }
    }
}