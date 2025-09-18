using Microsoft.EntityFrameworkCore;
using VSC.Toolsy.Repositories.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString =
    builder
    .Configuration
    .GetConnectionString("DefaultConnection") ?? throw new Exception(" null in connectionString");

//dotnet ef migrations add InitialCreate --context ApplicationDbContext
//dotnet ef database update --context ApplicationDbContext


builder.Services.AddDbContext<ApplicationDbContext>(
    options =>
    options
    .UseMySql(connectionString,
        ServerVersion.AutoDetect(connectionString))
    .LogTo(Console.WriteLine, LogLevel.Information)
    .EnableSensitiveDataLogging()

    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
