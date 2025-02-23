using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace Services.AttempsServices
{
    public interface IBlockedAttepmsService
    {
        Task LogAttemptAsync(string ipAddress, string countryCode, bool isBlocked, string userAgent);
        Task<IEnumerable<BlockedAttempt>> GetAttemptsAsync(int pageNumber, int pageSize);
    }
}
