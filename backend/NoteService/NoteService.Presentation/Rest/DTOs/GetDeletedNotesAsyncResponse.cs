using NoteService.Domain.Entities;

namespace NoteService.Presentation.Rest.DTOs;

public class GetDeletedNotesAsyncResponse
{
    public List<Note> Notes { get; set; }
}