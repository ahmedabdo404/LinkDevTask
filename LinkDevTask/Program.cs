using FluentValidation;
using FluentValidation.AspNetCore;
using LinkDevTask.Application.Servcices.Implementations;
using LinkDevTask.Application.Servcices.Interfaces;
using LinkDevTask.Domain.DataAccess;
using LinkDevTask.Domain.Models;
using LinkDevTask.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region MVC

builder.Services.AddControllersWithViews();
builder.Services.AddSession(s => s.IdleTimeout = TimeSpan.FromMinutes(30));

#endregion

#region Identity

builder.Services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

builder.Services.Configure<SecurityStampValidatorOptions>(options => options.ValidationInterval = TimeSpan.Zero);

#endregion

#region DB

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

#endregion

#region Packages

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

#endregion

#region Serivces
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IUserJobService, UserJobService>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
#endregion

var app = builder.Build();

#region PipeLine

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStatusCodePagesWithRedirects("/PageNotFound");

app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

#endregion
