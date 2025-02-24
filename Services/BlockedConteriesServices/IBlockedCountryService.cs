using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Countery;
namespace Services.Conteries
{
    public interface IBlockedCountryService
    {
        Task BlockAsync(string countryCode);
        Task UnblockAsync(string countryCode);
        Task<List<Country>> GetAllBlockedAsync(int pageNumber, int pageSize, Expression<Func<Country, bool>> predicate = null); 
        Task<bool> IsBlockedAsync(string countryCode);
    }
}
