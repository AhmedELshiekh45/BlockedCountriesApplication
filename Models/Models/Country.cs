﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Countery
{
    public class Country
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string? Capital { get; set; }
    }
}
