using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenT2.Models
{
    public class Job
    {
        public Job()
        {

        }

        public Job(int jobid, string jobtitle, int minsalary, int maxsalary)
        {
            job_id = jobid;
            job_title = jobtitle;
            min_salary = minsalary;
            max_salary = maxsalary;
        }
        public int job_id { get; set; }

        public string job_title { get; set; }

        public int min_salary { get; set; }

        public int max_salary { get; set; }
    }
}
