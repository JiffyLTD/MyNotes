using FluentValidation;
using MediatR;
using NoteService.Domain.Entities;

namespace NoteService.Application.Commands;

public sealed class UpdateNoteCommand : IRequest<bool>
{
    public Guid NoteId { get; set; }
    public Guid AccountId { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
}

public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
{
    public UpdateNoteCommandValidator()
    {
        RuleFor(x => x.NoteId)
            .NotEmpty().WithMessage("NoteId is required.");
        
        RuleFor(x => x.AccountId)
            .NotEmpty().WithMessage("AccountId is required.");
        
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(Note.MaxTitleLength)
            .WithMessage($"Title cannot exceed {Note.MaxTitleLength} characters.");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content is required.")
            .MaximumLength(Note.MaxContentLength)
            .WithMessage($"Content cannot exceed {Note.MaxContentLength} characters.");
    }
}