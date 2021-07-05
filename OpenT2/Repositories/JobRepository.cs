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
        public JobRepository(IDataContext context)
        {
            _context = context;

        }
        public async Task<IEnumerable<Job>> GetAll()
        {
            Activity.Current?.AddEvent(new ActivityEvent("JobAllCalled"));
            Activity.Current?.AddBaggage("http.method", "GET");
            return await _context.Jobs.ToListAsync();
        }
    }
}