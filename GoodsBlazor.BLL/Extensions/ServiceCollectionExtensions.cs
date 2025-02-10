using FluentValidation;
using FluentValidation.AspNetCore;
using GoodsBlazor.BLL.Interfaces;
using GoodsBlazor.BLL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GoodsBlazor.BLL.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

        services.AddAutoMapper(applicationAssembly);
        services.AddValidatorsFromAssembly(applicationAssembly)
            .AddFluentValidationAutoValidation();

        services.AddHttpContextAccessor();

        services.AddScoped<IProductRepository, ProductRepository>();
    }
}
