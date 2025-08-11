using Core.Interfaces;
using LinqToDB.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Core.Extensions.ServiceCollection;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterPooledDbContextFactories<TCommandDbContext, TQueryDbContext, TDbOptions>(
        this IServiceCollection services,
        string schema
    )
        where TCommandDbContext : DbContext
        where TQueryDbContext : DbContext
        where TDbOptions : class, IDbOptions
    {
        services.AddPooledDbContextFactory<TCommandDbContext>(
            (provider, options) =>
            {
                var dbOptions = provider.GetRequiredService<IOptions<TDbOptions>>().Value;
                var loggerFactory = provider.GetRequiredService<ILoggerFactory>();

                options
                    .UseNpgsql(dbOptions.MasterConnectionString,
                        builder => builder.MigrationsHistoryTable("migrations_history", schema))
                    .UseLoggerFactory(loggerFactory)
                    .UseSnakeCaseNamingConvention()
                    .UseValidationCheckConstraints();
            });
        
        services.AddPooledDbContextFactory<TQueryDbContext>(
            (provider, options) =>
            {
                var dbOptions = provider.GetRequiredService<IOptions<TDbOptions>>().Value;
                var loggerFactory = provider.GetRequiredService<ILoggerFactory>();

                options
                    .UseNpgsql(dbOptions.ReplicaConnectionString,
                        builder => builder.MigrationsHistoryTable("migrations_history", schema))
                    .UseLoggerFactory(loggerFactory)
                    .UseSnakeCaseNamingConvention()
                    .UseValidationCheckConstraints()
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

        LinqToDBForEFTools.Initialize();

        return services;
    }

}