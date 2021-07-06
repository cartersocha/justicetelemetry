using Microsoft.EntityFrameworkCore;
using OpenT2.DataAccess;
using OpenT2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System;


namespace OpenT2.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        static ActivitySource activitySource = new ActivitySource("CountryRepo");

        private static readonly ActivitySource timer = new ActivitySource(
        "Timer");
        private readonly IDataContext _context;
        public CountryRepository(IDataContext context)
        {
            _context = context;

        }
        public async Task<IEnumerable<Country1>> GetAll()
        {
           
                TimeDelay();
                
            
            using (Activity activity = activitySource.StartActivity("CountryList"))
            {
            Activity.Current?.AddEvent(new ActivityEvent("CountryListStart"));
            Activity.Current?.AddBaggage("http.method", "GET");
            return await _context.Countries.ToListAsync();
            
            }
            
        }

        public void TimeDelay()
        {
            using (Activity activity = timer.StartActivity("Timer"))
            {
            Activity.Current?.AddEvent(new ActivityEvent("TimerStart"));
            Activity.Current?.AddBaggage("http.method", "GET");
            System.Threading.Thread.Sleep(1000);
            activity?.AddEvent(new ActivityEvent("TimerEnd"));
            }
        }
        
    }
}


 
