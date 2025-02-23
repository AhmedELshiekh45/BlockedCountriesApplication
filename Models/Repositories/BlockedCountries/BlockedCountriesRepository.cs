using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories.BlockedCountries
{
    public class BlockedCountriesRepository : IBlockedCountriesRepository
    {
        private static ConcurrentDictionary<string, Country> _blockedCountries = new();

        public async Task AddAsync(Country country)
        {

           _blockedCountries.TryAdd(country.Code, country);
        }

        public async Task RemoveAsync(string countryCode)
        {
            _blockedCountries.TryRemove(countryCode, out _);
        }

        public async Task<IQueryable<Country>> GetAllAsync()
        {
            return _blockedCountries.Values.AsQueryable();
        }

        public async Task<bool> IsBlockedAsync(string countryCode)
        {
            return _blockedCountries.ContainsKey(countryCode);
        }

      
    }
}
