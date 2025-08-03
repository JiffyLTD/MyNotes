using FavoriteNoteService.Application.Commands;
using FavoriteNoteService.Application.Queries;

namespace FavoriteNoteService.Presentation.Extensions;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterMediatr(
        this IServiceCollection services
    ) 
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(
                typeof(CreateFavoriteNoteCommand).Assembly,
                typeof(DeleteFavoriteNoteCommand).Assembly,
                typeof(GetAllFavoriteNotesQuery).Assembly
            ));

        return services;
    }
}