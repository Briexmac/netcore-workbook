using BaseProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BaseProject.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Issue>()
                .HasKey(nameof(Issue.Id));
            modelBuilder.Entity<Issue>()
                .Property(nameof(Issue.Status))
                .HasConversion(new EnumToNumberConverter<IssueStatus, int>())
                .HasDefaultValue(IssueStatus.Backlog);

        }

        public DbSet<Issue> Issues { get; set; }
    }
}
