using FluentValidation;
using MediatR;
using NoteService.Domain.Entities;

namespace NoteService.Application.Commands;

public sealed class CreateNoteCommand : IRequest<Note>
{
    public Guid AccountId { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; } 
}

public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
{
    public CreateNoteCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(Note.MaxTitleLength)
            .WithMessage($"Title cannot exceed {Note.MaxTitleLength} characters.");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content is required.")
            .MaximumLength(Note.MaxContentLength)
            .WithMessage($"Content cannot exceed {Note.MaxContentLength} characters.");

        RuleFor(x => x.AccountId)
            .NotEmpty().WithMessage("AccountId is required.");
    }
}