using Cleverbit.CodingTask.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Cleverbit.CodingTask.Data
{
    public class CodingTaskContext : DbContext
    {
        public CodingTaskContext(DbContextOptions<CodingTaskContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchResult> MatcheResults { get; set; }
        public DbSet<WinnerOfMatchView> WinnerOfMatchViews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable(nameof(User));
            modelBuilder.Entity<Match>().ToTable(nameof(Match));
            modelBuilder.Entity<MatchResult>().ToTable(nameof(MatchResult));
            modelBuilder.Entity<WinnerOfMatchView>().ToTable(nameof(WinnerOfMatchView));
        }
    }
}
