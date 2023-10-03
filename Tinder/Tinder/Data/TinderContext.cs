using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tinder.Models;

namespace Tinder.Data
{
    public class TinderContext : DbContext
    {
        public TinderContext (DbContextOptions<TinderContext> options)
            : base(options)
        {
        }

        public DbSet<Tinder.Models.Question> Question { get; set; } = default!;

        public DbSet<Tinder.Models.Locality>? Locality { get; set; }
    }
}
