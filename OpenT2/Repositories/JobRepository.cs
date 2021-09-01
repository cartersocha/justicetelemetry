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
        private static readonly Meter MyMeter = new Meter("RollTide", "0.0.1");

        private readonly ILogger<JobRepository> logger;

        public JobRepository(IDataContext context,ILogger<JobRepository> logger)
        {
            _context = context;
            this.logger = logger;

        }

        public async Task<IEnumerable<Job>> GetAll()
        {
            await GenerateMetricsAsync();

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

        public async Task GenerateMetricsAsync()
        {
            int i = 1;
            var observableCounter = MyMeter.CreateObservableCounter<long>(
                "observable-counter",
                () =>
                {
                    var tag1 = new KeyValuePair<string, object>("tag1", "value1");
                    var tag2 = new KeyValuePair<string, object>("tag2", "value2");
                    return new List<Measurement<long>>()
                    {
                    // Report an absolute value (not an increment/delta value).
                    new Measurement<long>(i++ * 10, tag1, tag2),
                    };
                });

            await Task.Delay(10000);
        }
    }
}