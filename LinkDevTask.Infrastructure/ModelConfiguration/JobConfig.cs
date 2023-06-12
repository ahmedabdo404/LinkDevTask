using LinkDevTask.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkDevTask.Infrastructure.ModelConfiguration
{
    internal class JobConfig : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(job => job.Responsibilities).IsRequired(false);
            builder.Property(job => job.MaximumApplications).IsRequired(false);
            builder.Property(job => job.Picture).IsRequired(false);
            builder.Property(job => job.Skills).IsRequired(false);

            builder.HasOne(job => job.Category)
                .WithMany(cat => cat.Jobs)
                .HasForeignKey(job => job.CategoryId);
        }
    }
}
