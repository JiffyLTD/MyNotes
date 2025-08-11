using FavoriteNoteService.Infrastructure.DbContext;
using LinqToDB;
using MassTransit;
using Microsoft.Extensions.Logging;
using NoteService.InternalEvents;

namespace FavoriteNoteService.Infrastructure.Consumers;

public class NoteDeletedConsumer(IFavoriteNotesDbContextFactory dbContextFactory, ILogger<NoteDeletedConsumer> logger) : IConsumer<NotesDeleted>
{
    public async Task Consume(ConsumeContext<NotesDeleted> context)
    {
        var request = context.Message;
        
        await using var dbContext = dbContextFactory.CreateDbContext<FavoriteNotesCommandDbContext>();

        await dbContext.FavoriteNotes.DeleteAsync(x => request.NoteIds.Contains(x.NoteId));
        
        logger.LogInformation($"Было удалено {request.NoteIds.Length} избранных заметок");
    }
}