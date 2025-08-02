namespace NoteService.Domain.DTOs;

public sealed class UpdateNoteDto
{
    public Guid NoteId { get; set; }
    public Guid AccountId { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
}