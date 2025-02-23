using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories.BlockedAttemps
{
  

    public class BlockedAttemptsRepo : IBlockdAttempsRepo
    {
        private static readonly ConcurrentBag<BlockedAttempt> _attempts = new();

        public Task AddAttemptAsync(BlockedAttempt attempt)
        {
            _attempts.Add(attempt);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<BlockedAttempt>> GetAttemptsAsync(int pageNumber, int pageSize)
        {
            var attempts = _attempts
                .OrderByDescending(a => a.Timestamp) // Order by latest attempts
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return Task.FromResult(attempts);
        }
    }
}
