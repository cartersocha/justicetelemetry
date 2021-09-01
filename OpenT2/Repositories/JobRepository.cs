using Microsoft.EntityFrameworkCore;
using OpenT2.DataAccess;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using System.Diagnostics.Metrics;

namespace OpenT2.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly IDataContext _context;

        static ActivitySource activitySource = new ActivitySource("JobRepo");
        private static readonly Meter MyMeter = new Meter("JobTide", "0.0.69");
        private readonly ILogger<JobRepository> logger;

        public JobRepository(IDataContext context,ILogger<JobRepository> logger)
        {
            _context = context;
            this.logger = logger;

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
                    Baggage.Current.SetBaggage("jobId", id.ToString());
                    Job foundJob = await _context.Jobs.FindAsync(id);
                    activity?.SetTag("JobTitle", foundJob.JobTitle);
                    Baggage.Current.SetBaggage("jobTitle",foundJob.JobTitle);

                    this.logger.LogInformation(
                    "Job id generated {id}: {job}",
                    "Carter",
                    "PM");

                    return foundJob;
                }
             
           }
        
        public void TimeDelay()
        {
            using (Activity activity = activitySource.StartActivity("JobTimer"))
            {
            Activity.Current?.AddEvent(new ActivityEvent("TimerStart"));
            System.Threading.Thread.Sleep(1000);
            activity?.AddEvent(new ActivityEvent("TimerEnd"));
            }
        }
    }
}