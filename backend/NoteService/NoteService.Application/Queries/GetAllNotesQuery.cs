using FluentValidation;
using MediatR;
using NoteService.Domain.Entities;

namespace NoteService.Application.Queries;

public class GetAllNotesQuery: IRequest<List<Note>> 
{
    public Guid AccountId { get; set; }
}

public class GetAllNotesQueryValidator : AbstractValidator<GetAllNotesQuery>
{
    public GetAllNotesQueryValidator()
    {
        RuleFor(x => x.AccountId)
            .NotEmpty().WithMessage("AccountId is required.");
    }
}