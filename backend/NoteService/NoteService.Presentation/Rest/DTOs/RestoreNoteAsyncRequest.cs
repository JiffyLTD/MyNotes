using FluentValidation;
using NoteService.Domain.Entities;

namespace NoteService.Presentation.Rest.DTOs;

public class RestoreNoteAsyncRequest
{
    public required Guid NoteId { get; set; }
}

public class RestoreNoteAsyncRequestValidator : AbstractValidator<RestoreNoteAsyncRequest>
{
    public RestoreNoteAsyncRequestValidator()
    {
        RuleFor(x => x.NoteId)
            .NotEmpty().WithMessage("NoteId is required.");
    }
}