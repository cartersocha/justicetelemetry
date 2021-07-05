using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenT2.Models;
using OpenT2.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace OpenT2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        static ActivitySource activitySource = new ActivitySource("Justice&CarterAPI-Job");

        private readonly IJobRepository _jobRepository;
        public JobController(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobs()
        {
            using (Activity activity = activitySource.StartActivity("JobAll"))
            {

                var jobs = await _jobRepository.GetAll();

                
                return Ok(jobs);
            }
        }

         [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetSpecificJob(int id)
        {
            var job = await _jobRepository.Get(id);
            if(job == null)
                return NotFound();
    
            return Ok(job);
        }

   
    }
}