namespace NoteService.Domain.DTOs;

public sealed class GetNotesByIdsDto
{
    public required Guid[] NoteIds { get; set; }
    public Guid AccountId { get; set; }
}