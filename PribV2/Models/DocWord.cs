using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PribV2.Models
{
    public class DocWord
    {
        public DocWord(string name, int count)
        {
            Name = name;
            Count = count;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
