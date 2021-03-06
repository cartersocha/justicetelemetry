using OpenT2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenT2.Repositories
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country1>> GetAll();
    }
}
