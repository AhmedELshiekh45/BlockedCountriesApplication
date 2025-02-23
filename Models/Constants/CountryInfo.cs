using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DataAccessLayer.Constants
{
    public class CountryInfo
    {
        public NameInfo Name { get; set; }

        public string[] Capital { get; set; }

        public class NameInfo
        {
            public string Common { get; set; }
        }
    }
}

