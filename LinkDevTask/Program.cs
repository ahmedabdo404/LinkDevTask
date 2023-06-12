using LinkDevTask.WebApp.StratupRegisters;

var builder = WebApplication.CreateBuilder(args);
var builderRegisters = StartupRegisters.RegisterAllServices(builder);

var app = builderRegisters.Build();

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


