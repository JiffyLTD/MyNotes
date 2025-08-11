namespace FavoriteNoteService.Infrastructure.DbContext;

public interface IFavoriteNotesDbContextFactory
{
    public T CreateDbContext<T>() where T : BaseFavoriteNotesDbContext;
}