using NoteService.Domain.Entities;

namespace NoteService.Presentation.Rest.DTOs;

public class CreateNoteAsyncResponse
{
    public Note Note { get; set; }
}