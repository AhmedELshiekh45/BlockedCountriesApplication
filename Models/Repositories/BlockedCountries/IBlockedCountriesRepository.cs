using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories.BlockedCountries
{
    public interface IBlockedCountriesRepository
    {
        Task AddAsync(Country country);
        Task RemoveAsync(string countryCode);
        Task<IQueryable<Country>> GetAllAsync();
        Task<bool> IsBlockedAsync(string countryCode);
        
    }
}
