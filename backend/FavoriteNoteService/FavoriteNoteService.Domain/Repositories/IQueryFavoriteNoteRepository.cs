namespace FavoriteNoteService.Domain.Repositories;

public interface IQueryFavoriteNoteRepository
{
    Task<Guid[]> GetAllFavoriteNotesAsync(Guid accountId, CancellationToken cancellationToken);
}