using NoteService.Domain.DTOs;
using NoteService.Domain.Entities;

namespace NoteService.Domain.Repositories;

public interface ICommandNoteRepository
{
    Task AddAsync(Note note, CancellationToken cancellationToken);
    Task UpdateAsync(UpdateNoteDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(DeleteNoteDto dto, CancellationToken cancellationToken);
    Task RestoreAsync(RestoreNoteDto dto, CancellationToken cancellationToken);
}