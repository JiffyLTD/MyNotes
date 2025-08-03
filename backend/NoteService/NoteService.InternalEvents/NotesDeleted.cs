namespace NoteService.InternalEvents;

public class NotesDeleted
{
   public required Guid[] NoteIds { get; set; }
}