using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Countery;

namespace Services.LocationService
{
    public interface ILocationService
    {
        Task<Location> GetCountryByIpAsync(string ipAddress);
        Task<Country> GetCountryByCodeAsync(string countryCode);
    }
}
