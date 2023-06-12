using LinkDevTask.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace LinkDevTask.Infrastructure.ModelConfiguration
{
    public class UserJobConfig : IEntityTypeConfiguration<UserJob>
    {
        public void Configure(EntityTypeBuilder<UserJob> builder)
        {
            builder.HasKey(uj => new { uj.UserId, uj.JobId });

            builder.HasOne(uj => uj.User)
            .WithMany(u => u.UserJob)
            .HasForeignKey(uj => uj.UserId);

            builder.HasOne(uj => uj.Job)
            .WithMany(j => j.UserJob)
            .HasForeignKey(uj => uj.JobId);
        }
    }
}
