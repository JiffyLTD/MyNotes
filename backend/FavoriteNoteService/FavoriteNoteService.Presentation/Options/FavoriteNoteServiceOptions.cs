using Core.Interfaces;
using FluentValidation;

namespace FavoriteNoteService.Presentation.Options;

public class FavoriteNoteServiceOptions : IDbOptions
{
    public string ConnectionString { get; set; } = null!;
}

public sealed class NoteServiceOptionsValidator : AbstractValidator<FavoriteNoteServiceOptions>
{
    public NoteServiceOptionsValidator()
    {
        RuleFor(e => e.ConnectionString)
            .NotEmpty();
    }
}