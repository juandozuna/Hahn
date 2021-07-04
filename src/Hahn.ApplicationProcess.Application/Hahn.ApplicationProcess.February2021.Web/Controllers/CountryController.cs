using Microsoft.AspNetCore.Mvc;

namespace Hahn.ApplicationProcess.February2021.Web.Controllers
{
    using System.Threading.Tasks;
    using Domain.Repositories;

    /// <summary>
    /// Represents the country controller
    /// </summary>
    [ApiController]
    [Route("country")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;

        public CountryController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCountries()
        {
            var result = await _countryRepository.GetAllCountries();

            return Ok(result);
        }

        [HttpGet("name")]
        public async Task<IActionResult> IsCountryValid([FromQuery] string countryName)
        {
            var result = await _countryRepository.IsCountryValid(countryName);

            return Ok(result);
        }
    }
}