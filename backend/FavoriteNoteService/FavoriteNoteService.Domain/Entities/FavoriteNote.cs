namespace FavoriteNoteService.Domain.Entities;

public sealed class FavoriteNote
{
    public Guid NoteId { get; set; }
    public Guid AccountId { get; set; }
}