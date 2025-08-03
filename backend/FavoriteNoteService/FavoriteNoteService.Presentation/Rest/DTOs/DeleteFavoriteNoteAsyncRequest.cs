using FluentValidation;

namespace FavoriteNoteService.Presentation.Rest.DTOs;

public sealed class DeleteFavoriteNoteAsyncRequest
{
    public required Guid NoteId { get; set; }
}

public class DeleteFavoriteNoteAsyncRequestValidator : AbstractValidator<DeleteFavoriteNoteAsyncRequest>
{
    public DeleteFavoriteNoteAsyncRequestValidator()
    {
        RuleFor(x => x.NoteId)
            .NotEmpty().WithMessage("NoteId is required.");
    }
}