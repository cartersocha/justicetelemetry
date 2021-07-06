using Microsoft.EntityFrameworkCore;
using OpenT2.DataAccess;
using OpenT2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OpenT2.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly IDataContext _context;

        static ActivitySource activitySource = new ActivitySource("JobRepo");

        private static readonly ActivitySource timer = new ActivitySource(
        "JobTimer");

        public JobRepository(IDataContext context)
        {
            _context = context;

        }
        public async Task<IEnumerable<Job>> GetAll()
        {
            return await _context.Jobs.ToListAsync();
        }

            public async Task<Job> Get(int id)
            {
                TimeDelay();
                
                using (Activity activity = activitySource.StartActivity("JobId"))
                {
                    activity?.SetTag("JobId", id);
                    Job foundJob = await _context.Jobs.FindAsync(id);
                    activity?.SetTag("JobTitle", foundJob.JobTitle);
                    return foundJob;
                }
             
           }
        
        public void TimeDelay()
        {
            using (Activity activity = timer.StartActivity("JobTimer"))
            {
            Activity.Current?.AddEvent(new ActivityEvent("TimerStart"));
            System.Threading.Thread.Sleep(1000);
            activity?.AddEvent(new ActivityEvent("TimerEnd"));
            }
        }
    }
}