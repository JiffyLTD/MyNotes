using NoteService.Application.Commands;
using NoteService.Application.Queries;

namespace NoteService.Presentation.Extensions;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterMediatr(
        this IServiceCollection services
    ) 
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(
                typeof(CreateNoteCommand).Assembly,
                typeof(UpdateNoteCommand).Assembly,
                typeof(DeleteNoteCommand).Assembly,
                typeof(RestoreNoteCommand).Assembly,
                typeof(GetAllNotesQuery).Assembly,
                typeof(GetAllDeletedNotesQuery).Assembly
            ));

        return services;
    }
}