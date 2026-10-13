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

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();
app.MapControllers();

app.Run();
