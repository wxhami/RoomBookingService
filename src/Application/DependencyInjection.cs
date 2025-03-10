using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    private static Assembly Assembly => Assembly.GetExecutingAssembly();

    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration) =>
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(Assembly); })
            .AddValidatorsFromAssembly(Assembly);
    

    

}