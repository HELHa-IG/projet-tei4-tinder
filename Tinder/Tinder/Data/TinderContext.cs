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

        public DbSet<Tinder.Models.Discussion> Discussion { get; set; } = default!;

        public DbSet<Tinder.Models.Locality> Locality { get; set; }

        public DbSet<Tinder.Models.MatchLike>? MatchLike { get; set; }

        public DbSet<Tinder.Models.Questions>? Questions { get; set; }

        public DbSet<Tinder.Models.Users>? Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().HasOne(u => u.Locality).WithMany(l => l.Users).HasForeignKey(u => u.LocalityId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
