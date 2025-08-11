namespace NoteService.Infrastructure.DbContext;

public interface INotesDbContextFactory
{
    public T CreateDbContext<T>() where T : BaseNotesDbContext;
}