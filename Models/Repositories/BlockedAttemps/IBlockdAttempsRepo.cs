using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories.BlockedAttemps
{
    public interface IBlockdAttempsRepo
    {

        Task AddAttemptAsync(BlockedAttempt attempt);
        Task<IEnumerable<BlockedAttempt>> GetAttemptsAsync(int pageNumber, int pageSize);

    }
}
