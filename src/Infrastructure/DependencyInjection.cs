using Application.Common.Interfaces;
using Application.Common.Options;
using Hangfire;
using Hangfire.PostgreSql;
using Infrastructure.Extensions;
using Infrastructure.Persistence;
using Infrastructure.Services;
using MassTransit;
using Microsoft.AspNetCore.Builder;
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
        services.AddDatabase(configuration).AddQueue(configuration).AddSettings(configuration)
            .AddHangfire(configuration).AddServices();

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var dataSource = new NpgsqlDataSourceBuilder(configuration.GetConnectionString("Database")).Build();
        return services.AddDbContext<DatabaseContext>(
                o => o.EnableSensitiveDataLogging().UseNpgsql(dataSource),
                ServiceLifetime.Transient,
                ServiceLifetime.Transient)
            .AddTransient<IDatabaseContext>(provider => provider.GetRequiredService<DatabaseContext>())
            .AddTransient<DatabaseContextInitializer>();
        ;
    }

    private static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration) =>
        services.ConfigureOptions<EmailOptions>(configuration).ConfigureOptions<RabbitMqOptions>(configuration);

    private static IServiceCollection AddQueue(this IServiceCollection services, IConfiguration configuration)
    {
        var options = new RabbitMqOptions();
        configuration.GetSection(nameof(RabbitMqOptions)).Bind(options);

        return services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(options.Host, options.Port, options.VirtualHost, h =>
                {
                    h.Username(options.Username);
                    h.Password(options.Password);
                });

                cfg.ConfigureEndpoints(context);
            });
        });
    }

    private static IServiceCollection AddHangfire(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        services.AddHangfire(config => config
            .UsePostgreSqlStorage(o => o.UseNpgsqlConnection(connectionString)));

        services.AddHangfireServer();

        return services;
    }

    public static IApplicationBuilder UseHangfire(this IApplicationBuilder services)
    {
        return services.UseHangfireDashboard();
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services.AddTransient<INotificationService, NotificationService>()
            .AddTransient<ISmtpClientFabric, SmtpClientFabric>().AddTransient<IMailSender, MailSender>();
    }
}