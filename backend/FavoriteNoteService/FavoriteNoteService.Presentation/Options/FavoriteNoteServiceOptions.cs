using Core.Interfaces;
using FluentValidation;

namespace FavoriteNoteService.Presentation.Options;

public class FavoriteNoteServiceOptions : IDbOptions
{
    public string MasterConnectionString { get; set; } = null!;
    public string ReplicaConnectionString { get; set; } = null!;
}

public sealed class NoteServiceOptionsValidator : AbstractValidator<FavoriteNoteServiceOptions>
{
    public NoteServiceOptionsValidator()
    {
        RuleFor(e => e.MasterConnectionString)
            .NotEmpty();
        
        RuleFor(e => e.ReplicaConnectionString)
            .NotEmpty();
    }
}