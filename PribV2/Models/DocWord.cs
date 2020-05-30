using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PribV2.Models
{
    //word entry on db
    public class DocWord
    {
        public DocWord(string name, int count)
        {
            Name = name;
            Count = count;
        }

        [Key]
        //word itself
        public string Name { get; set; }

        //number of ocurrences
        public int Count { get; set; }
    }
}
