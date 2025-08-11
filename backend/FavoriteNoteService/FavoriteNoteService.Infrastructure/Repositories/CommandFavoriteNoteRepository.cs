using FavoriteNoteService.Domain.DTOs;
using FavoriteNoteService.Domain.Entities;
using FavoriteNoteService.Domain.Repositories;
using FavoriteNoteService.Infrastructure.DbContext;
using LinqToDB;

namespace FavoriteNoteService.Infrastructure.Repositories;

public class CommandFavoriteNoteRepository(IFavoriteNotesDbContextFactory dbContextFactory) : ICommandFavoriteNoteRepository
{
    public async Task AddAsync(FavoriteNote note, CancellationToken cancellationToken)
    {
        await using var context = dbContextFactory.CreateDbContext<FavoriteNotesCommandDbContext>();
        await context.FavoriteNotes.AddAsync(note, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(DeleteFavoriteNoteDto dto, CancellationToken cancellationToken)
    {
        await using var context = dbContextFactory.CreateDbContext<FavoriteNotesCommandDbContext>();
        await context.FavoriteNotes.DeleteAsync(x =>
                x.NoteId == dto.NoteId &&
                x.AccountId == dto.AccountId
            , cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}