namespace Hahn.ApplicationProcess.February2021.Domain.Repositories
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    /// <summary>
    /// Represents the country contract
    /// </summary>
    public interface ICountryRepository
    {
        /// <summary>
        /// Gets a list of all countries
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Country>> GetAllCountries();

        /// <summary>
        /// Check if country is valid   
        /// </summary>
        /// <param name="countryName"></param>
        /// <returns></returns>
        Task<bool> IsCountryValid(string countryName);
    }
}