namespace FavoriteNoteService.Infrastructure.DbContext;

public interface IFavoriteNotesDbContextFactory
{
    FavoriteNotesDbContext CreateDbContext();
}