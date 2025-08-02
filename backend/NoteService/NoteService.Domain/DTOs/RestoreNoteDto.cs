namespace NoteService.Domain.DTOs;

public sealed class RestoreNoteDto
{
    public Guid NoteId { get; set; }
    public Guid AccountId { get; set; }
}