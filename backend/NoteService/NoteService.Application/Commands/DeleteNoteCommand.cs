using FluentValidation;
using MediatR;
using NoteService.Domain.Entities;

namespace NoteService.Application.Commands;

public sealed class DeleteNoteCommand : IRequest<bool>
{
    public Guid NoteId { get; set; }
    public Guid AccountId { get; set; }
}

public class DeleteNoteCommandValidator : AbstractValidator<DeleteNoteCommand>
{
    public DeleteNoteCommandValidator()
    {
        RuleFor(x => x.NoteId)
            .NotEmpty().WithMessage("NoteId is required.");
        
        RuleFor(x => x.AccountId)
            .NotEmpty().WithMessage("AccountId is required.");
    }
}