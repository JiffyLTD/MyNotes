using Core.Interfaces;
using FluentValidation;

namespace NoteService.Presentation.Options;

public class NoteServiceOptions : IDbOptions
{
    public string DeletionDelay { get; set; } = null!;
    public string MasterConnectionString { get; set; } = null!;
    public string ReplicaConnectionString { get; set; } = null!;
} 

public sealed class NoteServiceOptionsValidator : AbstractValidator<NoteServiceOptions>
{
    public NoteServiceOptionsValidator()
    {
        RuleFor(e => e.MasterConnectionString)
            .NotEmpty();
        
        RuleFor(e => e.ReplicaConnectionString)
            .NotEmpty();
        
        RuleFor(e => e.DeletionDelay)
            .NotEmpty();
    }
}