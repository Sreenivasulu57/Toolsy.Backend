using Microsoft.EntityFrameworkCore;
using VSC.Toolsy.Common.Interfaces;
using VSC.Toolsy.Repositories.Data;
using VSC.Toolsy.Repositories.Interfaces;
using VSC.Toolsy.Services;
using VSC.Toolsy.Repositories.implementation;

namespace VSC.Toolsy.Server.Extensions
{
    public static class ServiceExtention
    {
        public static void RegisterServices(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IAdminService, AdminService>();


            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();


            string connectionString = configurationManager
                .GetConnectionString("DefaultConnection") ?? throw new Exception(" null in connectionString");

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                );

        }
    }
}
