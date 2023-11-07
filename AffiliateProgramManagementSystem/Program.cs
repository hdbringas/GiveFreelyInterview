using AffiliateProgramManagementSystem.Data;
using AffiliateProgramManagementSystem.Exceptions;
using AffiliateProgramManagementSystem.Services;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConsole();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AffiliateProgramDbContext>(options =>
{
    // Hardcoded connection string to keep things simple.  
    var folder = Environment.SpecialFolder.LocalApplicationData;
    var path = Environment.GetFolderPath(folder);
    var dbPath = Path.Join(path, "affiliateprogrammanagementsystem.db");

    options.UseSqlite($"Data Source={dbPath}");
});

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IAffiliateService, AffiliateService>();
var app = builder.Build();


using (var serviceScope = app.Services.CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<AffiliateProgramDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ApiExceptionHandler>();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }