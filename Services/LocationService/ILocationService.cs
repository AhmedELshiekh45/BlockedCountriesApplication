using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace Services.LocationService
{
    public interface ILocationService
    {
        Task<DataAccessLayer.Models.Country> GetCountryByIpAsync(string ipAddress);
        Task<DataAccessLayer.Models.Country> GetCountryByCodeAsync(string countryCode);
    }
}
