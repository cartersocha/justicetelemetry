using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenT2.Models;
using OpenT2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenT2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;
        public CountryController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetProducts()
        {
            var products = await _countryRepository.GetAll();
            return Ok(products);
        }
    }
}
