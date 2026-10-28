using Microsoft.EntityFrameworkCore;
using ThonInventory.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

var supabaseConnectionString = builder.Configuration.GetConnectionString("SupabaseConnection");

if (string.IsNullOrWhiteSpace(supabaseConnectionString) || supabaseConnectionString.Contains("CHANGE_ME", StringComparison.OrdinalIgnoreCase))
{
    throw new InvalidOperationException(
        "SupabaseConnection er ikke konfigurert. Oppdater ThonInventory/appsettings.Local.json med databasepassordet.");
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(supabaseConnectionString));

builder.Services.AddCors(options =>
    options.AddPolicy("Frontend", policy =>
        policy.WithOrigins(
                "http://localhost:3000",
                "http://127.0.0.1:3000",
                "http://localhost:3001",
                "http://127.0.0.1:3001")
            .AllowAnyHeader()
            .AllowAnyMethod()));

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("Frontend");
app.UseAuthorization();
app.MapControllers();

app.Run();
