using Microsoft.EntityFrameworkCore;
using OpenT2.Models;
using System.Threading;
using System.Threading.Tasks;

namespace OpenT2.DataAccess
{
    public interface IDataContext
    {
        DbSet<Country1> Countries { get; init; }
        //DbSet<Job> Jobs { get; init; }

        DbSet<Job> Jobs { get; init; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
