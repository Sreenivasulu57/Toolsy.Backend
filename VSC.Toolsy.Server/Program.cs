using VSC.Toolsy.Server.Extensions;
using VSC.Toolsy.Server.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterServices(builder.Configuration);

WebApplication app = builder.Build();

app.UseApiDefaults();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
