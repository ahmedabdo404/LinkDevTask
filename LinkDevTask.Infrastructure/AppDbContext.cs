using LinkDevTask.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LinkDevTask.Infrastructure
{
    public class AppDbContext : IdentityDbContext<User>
    {
        #region ctor
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        #endregion

        #region DbSets
        public virtual DbSet<Job> Jobs { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<UserJob> UserJobs { get; set; } = null!;
        #endregion

        #region Configs
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        #endregion

    }
}
