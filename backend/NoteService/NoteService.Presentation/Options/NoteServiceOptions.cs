using Core.Interfaces;
using FluentValidation;

namespace NoteService.Presentation.Options;

public class NoteServiceOptions : IDbOptions
{
    public string ConnectionString { get; set; } = null!;
    public string DeletionDelay { get; set; } = null!;
}

public sealed class NoteServiceOptionsValidator : AbstractValidator<NoteServiceOptions>
{
    public NoteServiceOptionsValidator()
    {
        RuleFor(e => e.ConnectionString)
            .NotEmpty();
        
        RuleFor(e => e.DeletionDelay)
            .NotEmpty();
    }
}