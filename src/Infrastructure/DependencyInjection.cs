using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostBuilder hostBuilder) =>
        services.AddDatabase(configuration);

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var dataSource = new NpgsqlDataSourceBuilder(configuration.GetConnectionString("Database")).Build();
        return services.AddDbContext<DatabaseContext>(
                o => o.EnableSensitiveDataLogging().UseNpgsql(dataSource),
                ServiceLifetime.Transient,
                ServiceLifetime.Transient)
            .AddTransient<IDatabaseContext>(provider => provider.GetRequiredService<DatabaseContext>());
    }
}