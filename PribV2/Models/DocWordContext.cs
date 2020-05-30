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

        public DbSet<DocWord> DocWords { get; set; }
    }
}
