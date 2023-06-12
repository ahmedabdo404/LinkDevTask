using FluentValidation.AspNetCore;
using FluentValidation;
using LinkDevTask.Domain.DataAccess;
using LinkDevTask.Domain.Models;
using LinkDevTask.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LinkDevTask.Application.Servcices.Implementations;
using LinkDevTask.Application.Servcices.Interfaces;

namespace LinkDevTask.WebApp.StratupRegisters
{
    public static class StartupRegisters
    {

        public static WebApplicationBuilder RegisterAllServices(this WebApplicationBuilder builder)
        {
            return RegisterMVCServices(builder)
                    .RegisterAuthServices()
                    .RegisterDataBaseServices()
                    .RegisterPackagesServices()
                    .RegisterApplicationServices();
        }

        private static WebApplicationBuilder RegisterMVCServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllersWithViews();
            builder.Services.AddSession(s => s.IdleTimeout = TimeSpan.FromMinutes(30));
            return builder;
        }
        private static WebApplicationBuilder RegisterAuthServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.Configure<SecurityStampValidatorOptions>(options => options.ValidationInterval = TimeSpan.Zero);
            return builder;
        }
        private static WebApplicationBuilder RegisterDataBaseServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
            builder.Configuration.GetConnectionString("DbConnection")));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            return builder;
        }
        private static WebApplicationBuilder RegisterPackagesServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();

            return builder;
        }
        private static WebApplicationBuilder RegisterApplicationServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<IUserJobService, UserJobService>();
            builder.Services.AddScoped<IJobService, JobService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            return builder;
        }
    }
}
