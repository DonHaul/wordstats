using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PribV2.Models
{
    public class GlobalVar
    {

        [Key]
        //var name (Single entry name is DocCount)
        public string Name { get; set; }

        //var count (Single entry value will be number of documents processed )
        public int Value { get; set; }
    }
}