namespace FavoriteNoteService.Domain.DTOs;

public sealed class DeleteFavoriteNoteDto
{
    public required Guid NoteId { get; set; }
    public required Guid AccountId { get; set; }
}