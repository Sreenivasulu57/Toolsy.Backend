using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using VSC.Toolsy.Common.Enums;
using VSC.Toolsy.Common.Interfaces;
using VSC.Toolsy.LocalStorage;
using VSC.Toolsy.Repositories.Data;
using VSC.Toolsy.Repositories.implementation;
using VSC.Toolsy.Repositories.Implementation;
using VSC.Toolsy.Repositories.Interfaces;
using VSC.Toolsy.Services;

namespace VSC.Toolsy.Server.Extensions
{
    public static class ServiceExtention
    {
        public static void RegisterServices(this IServiceCollection services, ConfigurationManager configurationManager)
        {

            services.AddLogging(logging =>
             {
                 logging.AddConsole();
                 logging.SetMinimumLevel(LogLevel.Trace);// Trace, Debug, Info, Warn, Error, Critical
             });

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IOwnerService, OwnerService>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IToolRepository, ToolRepository>();
            services.AddScoped<IToolService, ToolService>();
            services.AddScoped<ISigningKeyRepository, SigningKeyRepository>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IStorageService, LocalStorageService>();
            services.AddScoped<IMediaService, MediaService>();


            string redisConnectionString = configurationManager["Redis:ConnectionString"] ?? throw new Exception("Redis ConnectionString Is Null");
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(redisConnectionString);

            services.AddSingleton<IConnectionMultiplexer>(redis);
            services.AddSingleton<IRedisCacheService, RedisCacheService>();

            services.AddHostedService<KeyRotationService>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular", policy =>
                {
                    policy
                        .WithOrigins("http://localhost:4200") // Angular dev server
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials(); // optional if you use cookies
                });
            });

            services.AddControllers();
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Toolsy API",
                    Version = "v1"
                });

                // Add JWT Bearer Authentication to Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter your bearer Token"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement

                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                }
                );
            });


            services.AddAuthentication(options =>
            {

                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configurationManager["Jwt:Issuer"],
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,

                    IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
                    {

                        var httpClient = new HttpClient();

                        var jwks = httpClient.GetStringAsync($"{configurationManager["Jwt:Issuer"]}/.well-known/jwks.json").Result;

                        var keys = new JsonWebKeySet(jwks);

                        return keys.Keys;
                    }
                };
            });

            services.AddAuthorization(options =>
            {

                options.AddPolicy(Policy.USER_ONLY.ToString(), policy =>
                {
                    policy.RequireRole(RoleRequire.User.ToString());
                });

                options.AddPolicy(Policy.OWNER_ONLY.ToString(), policy =>
                {
                    policy.RequireRole(RoleRequire.Owner.ToString());
                });

                options.AddPolicy(Policy.ADMIN_ONLY.ToString(), policy =>
                {
                    policy.RequireRole(RoleRequire.Admin.ToString());
                });

                options.AddPolicy(Policy.ADMIN_OR_OWNER.ToString(), policy =>
                {
                    policy.RequireRole(RoleRequire.Admin.ToString(), RoleRequire.Owner.ToString());
                });

                options.AddPolicy(Policy.AUTHENTICATED_PROFILE.ToString(), policy =>
                {
                    policy.RequireAuthenticatedUser();
                });

            });


            string connectionString = configurationManager
                .GetConnectionString("DefaultConnection") ?? throw new Exception(" null in connectionString");

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                );

        }
    }
}
