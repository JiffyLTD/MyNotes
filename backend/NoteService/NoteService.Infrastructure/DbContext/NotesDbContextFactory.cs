using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace NoteService.Infrastructure.DbContext;

public class NotesDbContextFactory(IServiceProvider serviceProvider) : INotesDbContextFactory
{
    public T CreateDbContext<T>() where T : BaseNotesDbContext
    {
        var dbContextFactory = serviceProvider.GetRequiredService<IDbContextFactory<T>>();
        return dbContextFactory.CreateDbContext();
    }
}