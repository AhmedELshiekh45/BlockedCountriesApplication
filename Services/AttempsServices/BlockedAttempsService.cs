using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.BlockedAttemps;
using DataAccessLayer.Repositories.BlockedCountries;

namespace Services.AttempsServices
{
    public class BlockedAttemptsService : IBlockedAttepmsService
    {
        private readonly IBlockdAttempsRepo _repository;

        public BlockedAttemptsService(IBlockdAttempsRepo repository)
        {
            _repository = repository;
        }

        public async Task LogAttemptAsync(string ipAddress, string countryCode, bool isBlocked, string userAgent)
        {
            var attempt = new BlockedAttempt
            {
                IpAddress = ipAddress,
                Timestamp = DateTime.UtcNow,
                CountryCode = countryCode,
                IsBlocked = isBlocked,
                UserAgent = userAgent
            };

            await _repository.AddAttemptAsync(attempt);
        }

        public async Task<IEnumerable<BlockedAttempt>> GetAttemptsAsync(int pageNumber, int pageSize)
        {
            return await _repository.GetAttemptsAsync(pageNumber, pageSize);
        }
    }
}

