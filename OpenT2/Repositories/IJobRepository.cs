using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenT2.Repositories
{
    public interface IJobRepository
    {
        Task<IEnumerable<Job>> GetAll();

        Task<Job> Get(int id);
    }
}