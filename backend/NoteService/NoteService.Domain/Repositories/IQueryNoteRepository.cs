using NoteService.Domain.DTOs;
using NoteService.Domain.Entities;

namespace NoteService.Domain.Repositories;

public interface IQueryNoteRepository
{
    Task<Note> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Note>> GetAllAsync(Guid accountId, CancellationToken cancellationToken);
    Task<List<Note>> GetAllByIdsAsync(GetNotesByIdsDto dto, CancellationToken cancellationToken);
    Task<List<Note>> GetAllDeletedAsync(Guid accountId, CancellationToken cancellationToken);
}