using Microsoft.EntityFrameworkCore;
using VSC.Toolsy.Repositories.Data;

namespace VSC.Toolsy.Server.Extensions
{
    public static class ServiceExtention
    {
        public static void RegisterServices(this IServiceCollection services, ConfigurationManager configurationManager)
        {

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
