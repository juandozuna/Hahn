namespace Hahn.ApplicationProcess.February2021.Data.Repositories
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Domain.Models;
    using Domain.Repositories;
    using Newtonsoft.Json;

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
        public CountryRepository()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(baseUrl);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Country>> GetAllCountries()
        {
            var foundData = await _client.GetStringAsync("/all");

            var converted = JsonConvert.DeserializeObject<IEnumerable<Country>>(foundData);

            return converted;
        }

        /// <inheritdoc />
        public async Task<bool> IsCountryValid(string countryName)
        {
            var foundData = await _client.GetStringAsync($"/name/{countryName}");

            return !(foundData.Contains(@"status") && foundData.Contains("404"));
        }
    }
}