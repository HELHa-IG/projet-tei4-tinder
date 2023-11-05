using Microsoft.EntityFrameworkCore;
using Tinder.Models;

namespace Tinder.Data
{
    public class TinderContext : DbContext
    {
        public TinderContext(DbContextOptions<TinderContext> options)
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

            modelBuilder.Entity<Users>()
               .HasMany(u => u.Questions)
               .WithOne(q => q.User)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Users>()
                .HasMany(u => u.MatchLike01)
                .WithOne(q => q.User01)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Users>()
                .HasMany(u => u.MatchLike02)
                .WithOne(q => q.User02)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Discussion>()
                .HasOne(d => d.User01)
                .WithMany(u => u.Discussion01)
                .HasForeignKey(d => d.IdUser01)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Discussion>()
                .HasOne(d => d.User02)
                .WithMany(u => u.Discussion02)
                .HasForeignKey(d => d.IdUser02)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
