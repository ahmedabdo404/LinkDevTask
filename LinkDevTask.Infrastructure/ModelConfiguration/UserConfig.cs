using LinkDevTask.Application.Consts;
using LinkDevTask.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkDevTask.Infrastructure.ModelConfiguration
{
    internal class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", schema: "Identity")
                .SeedUsers();

            builder.Property(u => u.Email).IsRequired();
            builder.Property(u => u.FirstName).IsRequired();
            builder.Property(u => u.LastName).IsRequired();
            builder.Property(u => u.SkillsAsString).IsRequired(false);
            builder.Ignore(u => u.Skills);
        }

        internal class IdentityRoleConfig : IEntityTypeConfiguration<IdentityRole>
        {
            public void Configure(EntityTypeBuilder<IdentityRole> builder)
            {
                builder.ToTable("Roles", schema: "Identity")
                .SeedRoles();

            }
        }

        internal class IdentityUserClaimConfig : IEntityTypeConfiguration<IdentityUserClaim<string>>
        {
            public void Configure(EntityTypeBuilder<IdentityUserClaim<string>> builder) =>
                builder.ToTable("UserClaims", schema: "Identity");
        }

        internal class IdentityRoleClaimConfig : IEntityTypeConfiguration<IdentityRoleClaim<string>>
        {
            public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> builder) =>
                builder.ToTable("RoleClaims", schema: "Identity");
        }

        internal class IdentityUserLoginConfig : IEntityTypeConfiguration<IdentityUserLogin<string>>
        {
            public void Configure(EntityTypeBuilder<IdentityUserLogin<string>> builder)
            {
                builder.HasKey(e => e.UserId);
                builder.ToTable("UserLogins", schema: "Identity");
            }
        }

        internal class IdentityUserRoleConfig : IEntityTypeConfiguration<IdentityUserRole<string>>
        {
            public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
            {
                builder.HasKey(e => new { e.UserId, e.RoleId });
                builder.ToTable("UserRoles", schema: "Identity")
                    .SeedUserRole();
            }
        }

        internal class IdentityUserTokenConfig : IEntityTypeConfiguration<IdentityUserToken<string>>
        {
            public void Configure(EntityTypeBuilder<IdentityUserToken<string>> builder)
            {
                builder.HasKey(e => e.UserId);
                builder.ToTable("UserTokens", schema: "Identity");
            }
        }
    }
}
