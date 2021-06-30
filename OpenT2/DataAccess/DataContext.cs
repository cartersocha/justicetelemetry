using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OpenT2.DataAccess;
using OpenT2.Models;

namespace OpenT2.Models
{
    public class DataContext : Microsoft.EntityFrameworkCore.DbContext, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public Microsoft.EntityFrameworkCore.DbSet<Country> Countries { get; set; }
    }
}
