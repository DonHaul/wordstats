using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PribV2.Models
{
    public class DocStats
    {
        
        public long DocumentCount { get; set; }
        
        public Dictionary<string, WordStats> Statistics;

    }
}
