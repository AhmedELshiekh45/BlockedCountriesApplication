using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Constants.TemporalBlock;

namespace Services.TemporalBlockedCounteryServices
{
    public interface ITemporalBlockService
    {
        Task<bool> BlockCountryTemporarilyAsync(string countryCode, int durationMinutes);
        Task<List<TemporalBlockResponce>> GetTemporarilyBlockedCountriesAsync();
        Task<bool> IsCountryTemporarilyBlockedAsync(string countryCode);
    }
}
