using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenT2.Models;
using OpenT2.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OpenT2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        static ActivitySource activitySource = new ActivitySource("Justice&CarterAPI-Country");

        private readonly ICountryRepository _countryRepository;
        public CountryController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country1>>> GetCountries()
        {
            using (Activity activity = activitySource.StartActivity("CountryAll"))
            {
                var countries = await _countryRepository.GetAll();
                
                return Ok(countries);
            }
        }
    }
}
