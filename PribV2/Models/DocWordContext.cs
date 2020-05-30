using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PribV2.Models
{
    public class DocWordContext : DbContext
    {
        public DocWordContext(DbContextOptions<DocWordContext> options)
            : base(options)
        {
        }

        //set with all the words
        public DbSet<DocWord> DocWords { get; set; }

        //DB with single entry to store number of documents analysed
        public DbSet<GlobalVar> Globals { get; set; }
    }
}
