using Core.Interfaces;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
namespace Core.Extensions.ServiceCollection;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterHangfire<TDbOptions> (
        this IServiceCollection services,
        string serviceName
    ) where TDbOptions : class, IDbOptions
    {
        services.AddHangfire((provider, config) =>
        {
            var dbOptions = provider.GetRequiredService<IOptions<TDbOptions>>().Value;
            config.UsePostgreSqlStorage(dbOptions.MasterConnectionString, new PostgreSqlStorageOptions
            {
                SchemaName = $"hangfire_{serviceName}",
                PrepareSchemaIfNecessary = true
            });
        });

        services.AddHangfireServer(options =>
        {
            options.WorkerCount = 10;
        });

        return services;
    }
}