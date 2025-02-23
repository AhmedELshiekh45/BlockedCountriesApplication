using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Constants.TemporalBlock;

namespace DataAccessLayer.Repositories.TemporalBlockCountries
{
    public class TemporalBlockCountryRepo : ITemporalBlockCountryRepo
    {
        private static readonly ConcurrentDictionary<string, TemporalBlockResponce> _temporalBlocks = new();

        public Task<bool> AddTemporalBlockAsync(string countryCode, DateTime expiryTime)
        {
            return Task.FromResult(_temporalBlocks.TryAdd(countryCode, new TemporalBlockResponce
            {
                CountryCode = countryCode,
                ExpiryTime = expiryTime
            }));
        }

        public Task RemoveExpiredBlocksAsync()
        {
            var expiredBlocks = _temporalBlocks.Where(b => b.Value.ExpiryTime <= DateTime.UtcNow).ToList();
            foreach (var block in expiredBlocks)
            {
                _temporalBlocks.TryRemove(block.Key, out _);
            }
            return Task.CompletedTask;
        }

        public Task<IQueryable<TemporalBlockResponce>> GetTemporalBlocksAsync()
        {
            return Task.FromResult(_temporalBlocks.Values.AsQueryable());
        }

        public Task<bool> IsCountryTemporarilyBlockedAsync(string countryCode)
        {
            return Task.FromResult(_temporalBlocks.ContainsKey(countryCode));
        }
    }
}
