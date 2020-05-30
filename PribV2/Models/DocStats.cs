using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PribV2.Models
{
    public class DocStats
    {
        //number of documents received
        public int DocumentCount { get; set; }
        
        //dict with single word statistics
        public Dictionary<string, WordStats> Statistics { get; set; }


    }
}
