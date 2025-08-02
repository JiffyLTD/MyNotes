using Microsoft.EntityFrameworkCore;

namespace NoteService.Infrastructure.DbContext;

public class NotesDbContextFactory(IDbContextFactory<NotesDbContext> factory) : INotesDbContextFactory
{
    public NotesDbContext CreateDbContext() => factory.CreateDbContext();
}