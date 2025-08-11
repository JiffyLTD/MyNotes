using FavoriteNoteService.Domain.DTOs;
using FavoriteNoteService.Domain.Entities;

namespace FavoriteNoteService.Domain.Repositories;

public interface ICommandFavoriteNoteRepository
{
    Task AddAsync(FavoriteNote note, CancellationToken cancellationToken);
    Task DeleteAsync(DeleteFavoriteNoteDto dto, CancellationToken cancellationToken);
}