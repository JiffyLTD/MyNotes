namespace NoteService.Infrastructure.DbContext;

public interface INotesDbContextFactory
{
    NotesDbContext CreateDbContext();
}