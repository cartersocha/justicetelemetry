using Microsoft.AspNetCore.Mvc;
using OpenT2.Repositories;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using OpenTelemetry;
using OpenTelemetry.Context.Propagation;
using System;

namespace OpenT2.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
         private readonly string url = "https://opentelemetryapis.azurewebsites.net/api/DistributedTracing?code=/eyznUFV3urIXDTpatxQan17VGR/A8FbYQPNmOIff/mcA4H7/FBhHg==";
         private static readonly TextMapPropagator Propagator = new TraceContextPropagator();

        static ActivitySource activitySource = new ActivitySource("JobController");

        private readonly ILogger<JobController> logger;

        private readonly IJobRepository _jobRepository;
        public JobController(ILogger<JobController> logger, IJobRepository jobRepository)
        {
            this.logger = logger;
            this._jobRepository = jobRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobs()
        {
            using (Activity activity = activitySource.StartActivity("JobFunctionCall"))
            {
                    ActivityContext contextToInject = default;
                    if (activity != null)
                    {
                        contextToInject = activity.Context;
                    }
                    else if (Activity.Current != null)
                    {
                        contextToInject = Activity.Current.Context;
                    }
                    HttpClient client = new HttpClient();

                    Propagator.Inject(new PropagationContext(contextToInject, Baggage.Current), client, this.HeaderValueSetter);

                    client.DefaultRequestHeaders.Add("X-Version","1");
                    var response = await client.GetAsync(url);
                    var text = await response.Content.ReadAsStringAsync();

                    
            }

            using (Activity activity = activitySource.StartActivity("JobAll"))
            {

                var jobs = await _jobRepository.GetAll();

                
                return Ok(jobs);
            }
        }

         [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetSpecificJob(int id)
        {
            using (Activity activity = activitySource.StartActivity("JobOne"))
            {      
                Activity.Current?.AddBaggage("Controller","JobFinder");           
                var job = await _jobRepository.Get(id);
                if(job == null)
                    return NotFound();
                
                this.logger.LogInformation(
                    "Job id generated {id}: {job}",
                    job.JobId,
                    job.JobTitle);

                Activity.Current?.AddBaggage("Title",job.JobTitle);

                return Ok(job);
            }
        }

        private  IEnumerable<string> HeaderValueGetter (HttpResponseMessage request, string name)
        {
            if (request.Headers.TryGetValues(name, out var values))
            {
                return values;
            }

            return null;
        }

        private void HeaderValueSetter (HttpClient request, string name, string value)
        {
            request.DefaultRequestHeaders.Add(name, value);
        }

    }
}