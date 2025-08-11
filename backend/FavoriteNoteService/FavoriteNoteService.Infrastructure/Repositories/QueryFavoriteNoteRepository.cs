using FavoriteNoteService.Domain.Repositories;
using FavoriteNoteService.Infrastructure.DbContext;
using LinqToDB.EntityFrameworkCore;

namespace FavoriteNoteService.Infrastructure.Repositories;

public class QueryFavoriteNoteRepository(IFavoriteNotesDbContextFactory dbContextFactory) : IQueryFavoriteNoteRepository
{
    public async Task<Guid[]> GetAllFavoriteNotesAsync(Guid accountId, CancellationToken cancellationToken)
    {
        await using var context = dbContextFactory.CreateDbContext<FavoriteNotesQueryDbContext>();
        return await context.FavoriteNotes
            .Where(x => x.AccountId == accountId)
            .Select(x => x.NoteId)
            .ToArrayAsyncLinqToDB(cancellationToken);
    }
}