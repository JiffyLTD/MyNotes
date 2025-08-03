using FavoriteNoteService.Domain.DTOs;
using FavoriteNoteService.Domain.Entities;
using FavoriteNoteService.Domain.Repositories;
using FavoriteNoteService.Infrastructure.DbContext;
using LinqToDB;
using LinqToDB.EntityFrameworkCore;

namespace FavoriteNoteService.Infrastructure.Repositories;

public class FavoriteNoteRepository(IFavoriteNotesDbContextFactory dbContextFactory) : IFavoriteNoteRepository
{
    public async Task AddAsync(FavoriteNote note, CancellationToken cancellationToken)
    {
        await using var context = dbContextFactory.CreateDbContext();
        await context.FavoriteNotes.AddAsync(note, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(DeleteFavoriteNoteDto dto, CancellationToken cancellationToken)
    {
        await using var context = dbContextFactory.CreateDbContext();
        await context.FavoriteNotes.DeleteAsync(x =>
                x.NoteId == dto.NoteId &&
                x.AccountId == dto.AccountId
            , cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Guid[]> GetAllFavoriteNotesAsync(Guid accountId, CancellationToken cancellationToken)
    {
        await using var context = dbContextFactory.CreateDbContext();
        return await context.FavoriteNotes
            .Where(x => x.AccountId == accountId)
            .Select(x => x.NoteId)
            .ToArrayAsyncLinqToDB(cancellationToken);
    }
}