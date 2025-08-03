using FluentValidation;

namespace Core.Options;

public sealed class RabbitMqOptions
{
    public required string Host { get; set; }
    public required ushort Port { get; set; } = 5672;
    public required string Login { get; set; }
    public required string Password { get; set; }
}

public sealed class RabbitMqOptionsValidator : AbstractValidator<RabbitMqOptions>
{
    public RabbitMqOptionsValidator()
    {
        RuleFor(options => options.Host)
            .NotEmpty();
        
        RuleFor(options => options.Port)
            .NotEmpty();
        
        RuleForEach(options => options.Login)
            .NotEmpty();
        
        RuleForEach(options => options.Password)
            .NotEmpty();
    }
}