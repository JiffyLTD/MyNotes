using FluentValidation;
using MediatR;

namespace NoteService.Application.Commands;

public sealed class RestoreNoteCommand : IRequest<bool>
{
    public Guid NoteId { get; set; }
    public Guid AccountId { get; set; }
}

public class RestoreNoteCommandValidator : AbstractValidator<RestoreNoteCommand>
{
    public RestoreNoteCommandValidator()
    {
        RuleFor(x => x.NoteId)
            .NotEmpty().WithMessage("NoteId is required.");
        
        RuleFor(x => x.AccountId)
            .NotEmpty().WithMessage("AccountId is required.");
    }
}