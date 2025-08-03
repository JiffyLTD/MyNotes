using FluentValidation;

namespace FavoriteNoteService.Presentation.Options;

public class GrpcClientsOptions
{
    public string NoteServiceGrpcUrl { get; set; } = null!;
}

public sealed class GrpcClientsOptionsValidator : AbstractValidator<GrpcClientsOptions>
{
    public GrpcClientsOptionsValidator()
    {
        RuleFor(e => e.NoteServiceGrpcUrl)
            .NotEmpty();
    }
}