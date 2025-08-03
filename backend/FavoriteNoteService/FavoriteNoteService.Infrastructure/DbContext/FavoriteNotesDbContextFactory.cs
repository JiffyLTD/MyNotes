using Microsoft.EntityFrameworkCore;

namespace FavoriteNoteService.Infrastructure.DbContext;

public class FavoriteNotesDbContextFactory(IDbContextFactory<FavoriteNotesDbContext> factory) : IFavoriteNotesDbContextFactory
{
    public FavoriteNotesDbContext CreateDbContext() => factory.CreateDbContext();
}