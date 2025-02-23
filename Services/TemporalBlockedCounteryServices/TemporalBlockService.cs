using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Constants.TemporalBlock;
using DataAccessLayer.Repositories.TemporalBlockCountries;

namespace Services.TemporalBlockedCounteryServices
{


    public class TemporalBlockService : ITemporalBlockService
    {
        private readonly ITemporalBlockCountryRepo _temporalBlockRepository;

        public TemporalBlockService(ITemporalBlockCountryRepo temporalBlockRepository)
        {
            _temporalBlockRepository = temporalBlockRepository;
        }

        public async Task<bool> BlockCountryTemporarilyAsync(string countryCode, int durationMinutes)
        {
            var expiryTime = DateTime.UtcNow.AddMinutes(durationMinutes);
            return await _temporalBlockRepository.AddTemporalBlockAsync(countryCode, expiryTime);
        }

        public async Task<List<TemporalBlockResponce>> GetTemporarilyBlockedCountriesAsync()
        {
            var result = await _temporalBlockRepository.GetTemporalBlocksAsync();
            return result.ToList();
        }

        public async Task<bool> IsCountryTemporarilyBlockedAsync(string countryCode)
        {
            return await _temporalBlockRepository.IsCountryTemporarilyBlockedAsync(countryCode);
        }
    }
}
