using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenT2.Models
{
    [Keyless]
    public class Country
    {
        public Country()
        {

        }

        public Country(string country_id, string country_name, int region_id)
        {
            CountryId = country_id;
            CountryName = country_name;
            RegionId = region_id;
        }
        public string CountryId { get; set; }

        public string CountryName { get; set; }

        public int RegionId { get; set; }
    }
}
