using FluentValidation;
using NoteService.Domain.Entities;

namespace NoteService.Presentation.Rest.DTOs;

public class DeleteNoteAsyncRequest
{
    public required Guid NoteId { get; set; }
}

public class DeleteNoteAsyncRequestValidator : AbstractValidator<DeleteNoteAsyncRequest>
{
    public DeleteNoteAsyncRequestValidator()
    {
        RuleFor(x => x.NoteId)
            .NotEmpty().WithMessage("NoteId is required.");
    }
}