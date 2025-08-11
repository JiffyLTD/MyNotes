using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FavoriteNoteService.Infrastructure.DbContext;

public class FavoriteNotesDbContextFactory(IServiceProvider serviceProvider) : IFavoriteNotesDbContextFactory
{
    public T CreateDbContext<T>() where T : BaseFavoriteNotesDbContext
    {
        var dbContextFactory = serviceProvider.GetRequiredService<IDbContextFactory<T>>();
        return dbContextFactory.CreateDbContext();
    }
}