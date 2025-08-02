using FluentValidation;

namespace Core.Options;

public sealed class KeycloakOptions
{
    public required string MetadataAddress { get; set; }
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
}

public sealed class KeycloakOptionsValidator : AbstractValidator<KeycloakOptions>
{
    public KeycloakOptionsValidator()
    {
        RuleFor(options => options.MetadataAddress)
            .NotEmpty();
        
        RuleForEach(options => options.Issuer)
            .NotEmpty();
        
        RuleForEach(options => options.Audience)
            .NotEmpty();
    }
}