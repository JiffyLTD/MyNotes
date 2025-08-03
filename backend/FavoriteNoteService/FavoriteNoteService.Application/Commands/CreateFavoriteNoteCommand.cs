using FluentValidation;
using MediatR;

namespace FavoriteNoteService.Application.Commands;

public class CreateFavoriteNoteCommand  : IRequest<bool>
{
    public required Guid NoteId { get; set; }
    public required Guid AccountId { get; set; }
}

public class CreateFavoriteNoteCommandValidator : AbstractValidator<CreateFavoriteNoteCommand>
{
    public CreateFavoriteNoteCommandValidator()
    {
        RuleFor(x => x.NoteId)
            .NotEmpty().WithMessage("NoteId is required.");

        RuleFor(x => x.AccountId)
            .NotEmpty().WithMessage("AccountId is required.");
    }
}