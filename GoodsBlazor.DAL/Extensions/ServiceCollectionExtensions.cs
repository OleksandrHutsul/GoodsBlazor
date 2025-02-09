using GoodsBlazor.DAL.Context;
using GoodsBlazor.DAL.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoodsBlazor.DAL.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("GoodsDb");
        services.AddDbContext<GoodsDbContext>(options =>
            options.UseSqlServer(connectionString)
                .EnableSensitiveDataLogging());

        services.AddScoped<IDataSeeder, DataSeeder>();
    }
}
