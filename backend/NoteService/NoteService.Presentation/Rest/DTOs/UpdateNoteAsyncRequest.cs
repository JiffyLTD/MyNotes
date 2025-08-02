using FluentValidation;
using NoteService.Domain.Entities;

namespace NoteService.Presentation.Rest.DTOs;

public class UpdateNoteAsyncRequest 
{
    public Guid NoteId { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
}

public class UpdateNoteAsyncRequestValidator : AbstractValidator<UpdateNoteAsyncRequest>
{
    public UpdateNoteAsyncRequestValidator()
    {
        RuleFor(x => x.NoteId)
            .NotEmpty().WithMessage("NoteId is required.");
        
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