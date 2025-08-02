using NoteService.Domain.Entities;

namespace NoteService.Presentation.Rest.DTOs;

public class GetNotesAsyncResponse
{
    public List<Note> Notes { get; set; }
}