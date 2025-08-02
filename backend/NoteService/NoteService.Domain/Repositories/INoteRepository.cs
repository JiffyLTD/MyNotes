using NoteService.Domain.DTOs;
using NoteService.Domain.Entities;

namespace NoteService.Domain.Repositories;

public interface INoteRepository
{
    Task<Note> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Note>> GetAllAsync(Guid accountId, CancellationToken cancellationToken);
    Task<List<Note>> GetAllDeletedAsync(Guid accountId, CancellationToken cancellationToken);
    Task AddAsync(Note note, CancellationToken cancellationToken);
    Task UpdateAsync(UpdateNoteDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(DeleteNoteDto dto, CancellationToken cancellationToken);
    Task RestoreAsync(RestoreNoteDto dto, CancellationToken cancellationToken);
}