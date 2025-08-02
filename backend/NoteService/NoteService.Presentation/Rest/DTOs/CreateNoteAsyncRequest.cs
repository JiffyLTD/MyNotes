using FluentValidation;
using NoteService.Domain.Entities;

namespace NoteService.Presentation.Rest.DTOs;

public class CreateNoteAsyncRequest
{
    public required string Title { get; set; }
    public required string Content { get; set; } 
}

public class CreateNoteAsyncRequestValidator : AbstractValidator<CreateNoteAsyncRequest>
{
    public CreateNoteAsyncRequestValidator()
    {
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