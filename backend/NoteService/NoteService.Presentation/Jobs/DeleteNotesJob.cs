using System.Xml;
using LinqToDB;
using LinqToDB.EntityFrameworkCore;
using MassTransit;
using Microsoft.Extensions.Options;
using NoteService.Infrastructure.DbContext;
using NoteService.Infrastructure.Jobs;
using NoteService.InternalEvents;
using NoteService.Presentation.Options;

namespace NoteService.Presentation.Jobs;

public class DeleteNotesJob(
    INotesDbContextFactory contextFactory,
    IOptions<NoteServiceOptions> options,
    ILogger<DeleteNotesJob> logger,
    IPublishEndpoint publishEndpoint
    ) : IDeleteNotesJob
{
    private const int ChunkSize = 100;
    private readonly TimeSpan _deletionDelay = XmlConvert.ToTimeSpan(options.Value.DeletionDelay);
    public async Task StartAsync()
    {
        await using var dbContext = contextFactory.CreateDbContext();
        
        var now = DateTime.UtcNow;

        var allCountDeleted = await dbContext.Notes
            .CountAsyncLinqToDB(x => 
                x.DeletedAt != null &&
                x.DeletedAt + _deletionDelay < now);

        var deletedNotes = 0;
        while (allCountDeleted > deletedNotes)
        {
            var notesToDelete = await dbContext.Notes
                .Where(x =>
                    x.DeletedAt != null &&
                    x.DeletedAt + _deletionDelay < now
                )
                .Take(ChunkSize)
                .Skip(deletedNotes)
                .Select(x => x.Id)
                .ToListAsyncLinqToDB();
            
            var deletedNotesOutput = await dbContext.Notes
                .Where(x => notesToDelete.Contains(x.Id))
                .DeleteWithOutputAsync(
                    x => x.Id
                );
            
            logger.LogInformation($"Было удалено {deletedNotesOutput.Length} заметок");
            
            deletedNotes += deletedNotesOutput.Length;
            
            if (deletedNotesOutput.Length > 0)
            {
                await publishEndpoint.Publish(new NotesDeleted
                {
                    NoteIds = deletedNotesOutput
                });
            }
        }
    }
}