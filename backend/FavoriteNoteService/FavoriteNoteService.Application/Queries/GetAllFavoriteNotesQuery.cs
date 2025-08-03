using FluentValidation;
using MediatR;

namespace FavoriteNoteService.Application.Queries;

public class GetAllFavoriteNotesQuery  : IRequest<Guid[]>
{
    public required Guid AccountId { get; set; }
}

public class GetAllFavoriteNotesQueryValidator : AbstractValidator<GetAllFavoriteNotesQuery>
{
    public GetAllFavoriteNotesQueryValidator()
    {
        RuleFor(x => x.AccountId)
            .NotEmpty().WithMessage("AccountId is required.");
    }
}