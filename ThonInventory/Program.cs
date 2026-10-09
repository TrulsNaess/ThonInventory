using Microsoft.EntityFrameworkCore;
using ThonInventory.Data;

var builder = WebApplication.CreateBuilder(args);

var supabaseConnectionString = builder.Configuration.GetConnectionString("SupabaseConnection")
    ?? throw new InvalidOperationException("Connection string 'ConnectionStrings:SupabaseConnection' is not configured.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(supabaseConnectionString));

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
