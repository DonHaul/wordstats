using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PribV2.Models
{
    public class WordStats
    {
        public WordStats(int query, int total)
        {
            Query = query;
            Total = total;
        }

        //words on the queried document
        public int Query { get; set; }

        //word ocurrences on the database
        public int Total { get; set; }
    }
}
