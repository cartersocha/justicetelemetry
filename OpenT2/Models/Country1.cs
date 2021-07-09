using Microsoft.EntityFrameworkCore;

namespace OpenT2.Models
{
    [Keyless]
    public class Country1
    {
        public Country1()
        {

        }

        public Country1(string country_id, string country_name, int region_id)
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
