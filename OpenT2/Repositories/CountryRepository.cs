using Microsoft.EntityFrameworkCore;
using OpenT2.DataAccess;
using OpenT2.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace OpenT2.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        static ActivitySource activitySource = new ActivitySource("CountryRepo");

        private readonly IDataContext _context;
        public CountryRepository(IDataContext context)
        {
            _context = context;

        }
        public async Task<IEnumerable<Country1>> GetAll()
        {
            TimeDelay();
            
            using (Activity activity = activitySource.StartActivity("select.postgres.country1"))
            {
            Activity.Current?.AddEvent(new ActivityEvent("start select.postgres.country1"));
            activity?.SetTag("functionName", "SQLDB");
            Activity.Current?.AddBaggage("country.operation.id","select");
            return await _context.Countries.ToListAsync();
            }
            
        }

        public void TimeDelay()
        {
            using (Activity activity = activitySource.StartActivity("Timer"))
            {
            Activity.Current?.AddEvent(new ActivityEvent("TimerStart"));
            Activity.Current?.AddBaggage("http.method", "GET");
            System.Threading.Thread.Sleep(1000);
            activity?.AddEvent(new ActivityEvent("TimerEnd"));
            }
        }
        
    }
}


 
