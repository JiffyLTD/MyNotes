using FavoriteNoteService.Presentation.Options;
using Microsoft.Extensions.Options;
using NoteService.Grpc;
using ProtoBuf.Grpc.ClientFactory;

namespace FavoriteNoteService.Presentation.Extensions;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterGrpcClients(
        this IServiceCollection services
    )
    {
        var options = 
        services.AddCodeFirstGrpcClient<INoteGrpcClient>((serviceProvider, options) =>
        {
            var grpcClientsOptions = serviceProvider.GetRequiredService<IOptions<GrpcClientsOptions>>().Value;
            options.Address = new Uri(grpcClientsOptions.NoteServiceGrpcUrl);
        });

        return services;
    }
}