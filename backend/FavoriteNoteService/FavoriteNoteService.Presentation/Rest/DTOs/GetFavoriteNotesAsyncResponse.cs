using NoteService.Grpc;

namespace FavoriteNoteService.Presentation.Rest.DTOs;

public class GetFavoriteNotesAsyncResponse
{
    public Note[] FavoriteNotes { get; set; }
}