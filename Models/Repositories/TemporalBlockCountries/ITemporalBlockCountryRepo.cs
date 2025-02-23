using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Constants.TemporalBlock;

namespace DataAccessLayer.Repositories.TemporalBlockCountries
{
    public interface ITemporalBlockCountryRepo
    {
        Task<bool> AddTemporalBlockAsync(string countryCode, DateTime expiryTime);
        Task RemoveExpiredBlocksAsync();
        Task<IQueryable<TemporalBlockResponce>> GetTemporalBlocksAsync();
        Task<bool> IsCountryTemporarilyBlockedAsync(string countryCode);
    }
}
