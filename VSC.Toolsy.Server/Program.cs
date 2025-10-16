using VSC.Toolsy.Server.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterServices(builder.Configuration);
builder.Services.AddHttpContextAccessor();


WebApplication app = builder.Build();

app.UseApiDefaults();

app.UseCors("AllowAngular");

if (app.Environment.IsDevelopment())
{

    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
