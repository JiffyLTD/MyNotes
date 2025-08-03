using FavoriteNoteService.Domain.DTOs;
using FavoriteNoteService.Domain.Entities;

namespace FavoriteNoteService.Domain.Repositories;

public interface IFavoriteNoteRepository
{
    Task AddAsync(FavoriteNote note, CancellationToken cancellationToken);
    Task DeleteAsync(DeleteFavoriteNoteDto dto, CancellationToken cancellationToken);
    Task<Guid[]> GetAllFavoriteNotesAsync(Guid accountId, CancellationToken cancellationToken);
}