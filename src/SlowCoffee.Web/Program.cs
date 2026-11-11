using SlowCoffee.Infrastructure.Data;
using SlowCoffee.Infrastructure;
var builder=WebApplication.CreateBuilder(args); builder.Services.AddControllersWithViews(); builder.Services.AddInfrastructure(builder.Configuration);
var app=builder.Build(); if(!app.Environment.IsDevelopment()){app.UseExceptionHandler("/Home/Error");app.UseHsts();} app.UseHttpsRedirection();app.UseStaticFiles();app.UseRouting();app.MapControllerRoute("default","{controller=Home}/{action=Index}/{id?}");if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await DbInitializer.InitializeAsync(db);
}
app.Run();
