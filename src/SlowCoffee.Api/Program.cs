using Microsoft.OpenApi.Models;
using SlowCoffee.Infrastructure;
using SlowCoffee.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.SwaggerDoc("v1", new OpenApiInfo
{
    Title = "Slow Coffee API", Version = "v1",
    Description = "API REST para integração entre o painel administrativo e o aplicativo mobile."
}));
builder.Services.AddCors(options => options.AddPolicy("MobileClient", policy =>
    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await DbInitializer.InitializeAsync(db);
}
app.UseHttpsRedirection();
app.UseCors("MobileClient");
app.MapControllers();
app.MapGet("/health", () => Results.Ok(new { status = "healthy", service = "slowcoffee-api", timestamp = DateTimeOffset.UtcNow }));
app.Run();
