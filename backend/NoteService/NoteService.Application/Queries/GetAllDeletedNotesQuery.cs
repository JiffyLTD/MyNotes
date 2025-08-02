using FluentValidation;
using MediatR;
using NoteService.Domain.Entities;

namespace NoteService.Application.Queries;

public class GetAllDeletedNotesQuery: IRequest<List<Note>> 
{
    public Guid AccountId { get; set; }
}

public class GetAllDeletedNotesQueryValidator : AbstractValidator<GetAllDeletedNotesQuery>
{
    public GetAllDeletedNotesQueryValidator()
    {
        RuleFor(x => x.AccountId)
            .NotEmpty().WithMessage("AccountId is required.");
    }
}