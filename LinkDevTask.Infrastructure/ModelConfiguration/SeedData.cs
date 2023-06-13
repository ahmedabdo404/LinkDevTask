using LinkDevTask.Application.Consts;
using LinkDevTask.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDevTask.Infrastructure.ModelConfiguration
{
    internal static class SeedData
    {
        private static User Admin = new();
        private static User User = new();
        private static IdentityRole AdminRole = new(UserRoles.Admin) { NormalizedName = UserRoles.Admin.ToUpper() };
        private static IdentityRole UserRole = new(UserRoles.User) { NormalizedName = UserRoles.User.ToUpper() };
        internal static void SeedUsers(this EntityTypeBuilder<User> builder)
        {
            var pwd = "P@ssw0rd";

            Admin.FirstName = "admin";
            Admin.LastName = "admin";
            Admin.Email = "admin@test.com";
            Admin.NormalizedEmail = "admin@test.com".ToUpper();
            Admin.UserName = "admin";
            Admin.NormalizedUserName = "admin".ToUpper();
            Admin.PasswordHash = new PasswordHasher<User>().HashPassword(Admin, pwd);

            User.FirstName = "user";
            User.LastName = "user";
            User.Email = "user@test.com";
            User.NormalizedEmail = "user@test.com".ToUpper();
            User.UserName = "user";
            User.NormalizedUserName = "user".ToUpper();
            User.PasswordHash = new PasswordHasher<User>().HashPassword(User, pwd);

            builder.HasData(Admin, User);
        }

        internal static void SeedRoles(this EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(AdminRole, UserRole);
        }
        internal static void SeedUserRole(this EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            var adminUserRole = new IdentityUserRole<string>()
            {
                UserId = Admin.Id,
                RoleId = AdminRole.Id,
            };

            var userUserRole = new IdentityUserRole<string>()
            {
                UserId = User.Id,
                RoleId = UserRole.Id,
            };

            builder.HasData(adminUserRole, userUserRole);
        }
    }
}
