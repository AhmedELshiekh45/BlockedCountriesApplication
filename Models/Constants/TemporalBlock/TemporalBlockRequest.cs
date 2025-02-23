using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Constants.TemporalBlock
{
    public class TemporalBlockRequest
    {
        public string CountryCode { get; set; }
        public int DurationMinutes { get; set; }
    }
}
