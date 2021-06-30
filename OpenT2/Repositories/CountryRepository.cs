using Microsoft.EntityFrameworkCore;
using OpenT2.DataAccess;
using OpenT2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenT2.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly IDataContext _context;
        public CountryRepository(IDataContext context)
        {
            _context = context;

        }
        public async Task<IEnumerable<Country>> GetAll()
        {
            return await _context.Countries.ToListAsync();
        }
    }
}


 
