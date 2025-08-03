using FluentValidation;

namespace FavoriteNoteService.Presentation.Rest.DTOs;

public sealed class CreateFavoriteNoteAsyncRequest
{
    public required Guid NoteId { get; set; }
}

public class CreateFavoriteNoteAsyncRequestValidator : AbstractValidator<CreateFavoriteNoteAsyncRequest>
{
    public CreateFavoriteNoteAsyncRequestValidator()
    {
        RuleFor(x => x.NoteId)
            .NotEmpty().WithMessage("NoteId is required.");
    }
}