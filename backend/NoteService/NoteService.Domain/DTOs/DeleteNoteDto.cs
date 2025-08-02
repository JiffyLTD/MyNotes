namespace NoteService.Domain.DTOs;

public sealed class DeleteNoteDto
{
    public Guid NoteId { get; set; }
    public Guid AccountId { get; set; }
}