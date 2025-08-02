using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Core.Extensions.ServiceCollection;

public static partial class ServiceCollectionExtensions
{
    public static OpenTelemetryBuilder RegisterOpenTelemetry(
        this IServiceCollection services,
        ILoggingBuilder iLoggingBuilder,
        string serviceName
    )
    {
        return services.AddOpenTelemetry()
            .WithTracing(tracing =>
            {
                tracing
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddOtlpExporter();
            })
            .ConfigureResource(resource => { resource.AddService(serviceName); });
    }
}