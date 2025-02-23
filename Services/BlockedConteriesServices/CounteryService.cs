using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.BlockedCountries;
using Services.LocationService;

namespace Services.Conteries
{
    public class CountryService : IBlockedCountryService
    {
        private readonly IBlockedCountriesRepository _blockedCountriesRepository;
        private readonly ILocationService _locationService;

        public CountryService(IBlockedCountriesRepository blockedCountriesRepository, ILocationService locationService)
        {
            _blockedCountriesRepository = blockedCountriesRepository;
            this._locationService = locationService;
        }

        public async Task BlockAsync(string countryCode)
        {
            if (!await _blockedCountriesRepository.IsBlockedAsync(countryCode))
            {
                var countery = await _locationService.GetCountryByCodeAsync(countryCode);
                if (countery != null)
                {

                    await _blockedCountriesRepository.AddAsync(countery);
                }
            }
            return;
        }

        public async Task UnblockAsync(string countryCode)
        {
            if (await _blockedCountriesRepository.IsBlockedAsync(countryCode))
            {
                await _blockedCountriesRepository.RemoveAsync(countryCode);
            }
        }

        public async Task<bool> IsBlockedAsync(string countryCode)
        {
            return await _blockedCountriesRepository.IsBlockedAsync(countryCode);
        }

        public async Task<List<Country>> GetAllBlockedAsync(int pageNumber, int pageSize, Expression<Func<Country, bool>> predicate = null)
        {
            var query = await _blockedCountriesRepository.GetAllAsync();

            // Apply filtering if predicate is provided
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            // Apply pagination
            var result = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return result;
        }
    }
}
