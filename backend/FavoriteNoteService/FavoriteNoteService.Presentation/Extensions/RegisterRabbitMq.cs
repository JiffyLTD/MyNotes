using System.Reflection;
using Core.Options;
using FavoriteNoteService.Infrastructure.Consumers;
using MassTransit;
using Microsoft.Extensions.Options;

namespace FavoriteNoteService.Presentation.Extensions;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterRabbitMq(
        this IServiceCollection services
    ) 
    {
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            { 
                var options = context.GetRequiredService<IOptions<RabbitMqOptions>>().Value;
                cfg.Host(host: options.Host, h =>
                {
                    h.Username(options.Login);
                    h.Password(options.Password);
                });
                
                cfg.ReceiveEndpoint("note-deleted-queue", e =>
                {
                    e.ConfigureConsumer<NoteDeletedConsumer>(context);
                });
            });

            x.AddConsumer<NoteDeletedConsumer>();
        });

        return services;
    }
}