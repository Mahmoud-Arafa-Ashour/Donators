using Donators.Entites;
using Donators.Entites.Context;
using Donators.Persistant;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using FluentValidation;

namespace Donators;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        var conn = configuration.GetConnectionString("DefaultConnection") ??
            throw new InvalidOperationException("Connection String 'DefaultConnection' not found.");
        services.AddDbContext<DonatorsDBContext>(options => options.UseSqlServer(conn));
        services.AddMappsterServices();
        services.AddAuthconfigServices(configuration);
        services.AddConfigServices();
        services.AddProblemDetails();
        services.AddHttpContextAccessor();
        return services;
    }
    public static IServiceCollection AddMappsterServices(this IServiceCollection services)
    {
        var mappingConfiguration = TypeAdapterConfig.GlobalSettings;
        mappingConfiguration.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton<IMapper>(new Mapper(mappingConfiguration));
        return services;
    }
    public static IServiceCollection AddConfigServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
    public static IServiceCollection AddAuthconfigServices(this IServiceCollection services , IConfiguration configuration)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<DonatorsDBContext>()
            .AddDefaultTokenProviders();
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }
            )
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("xwTYBtjkzBnMf8ZYyk8Gck4a18gy2bN7")),
                    ValidIssuer = "Lagnet EL Zakkah",
                    ValidAudience = "Lagnet El Zakkah Donators"
                };
            });
        services.Configure<IdentityOptions>(Options =>
        {
            Options.Password.RequiredLength = 8;
            Options.SignIn.RequireConfirmedEmail = false;
        });
        return services;
    }
}
