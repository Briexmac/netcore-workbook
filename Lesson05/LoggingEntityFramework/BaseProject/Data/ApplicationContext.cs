using BaseProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BaseProject.Data
{
    public class ApplicationContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;

        public ApplicationContext(DbContextOptions<ApplicationContext> options, ILoggerFactory loggerFactory) : base(options)
        {
            _loggerFactory = loggerFactory;
            _loggerFactory.AddDebug();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseLoggerFactory(_loggerFactory);
        }

        public DbSet<User> Users { get; set; }
    }
}
