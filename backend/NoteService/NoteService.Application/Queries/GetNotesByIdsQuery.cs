using FluentValidation;
using MediatR;
using NoteService.Domain.Entities;

namespace NoteService.Application.Queries;

public class GetNotesByIdsQuery: IRequest<List<Note>> 
{
    public Guid AccountId { get; set; }
    public required Guid[] NoteIds { get; set; }
}

public class GetNotesByIdsQueryValidator : AbstractValidator<GetNotesByIdsQuery>
{
    public GetNotesByIdsQueryValidator()
    {
        RuleFor(x => x.AccountId)
            .NotEmpty().WithMessage("AccountId is required.");
        
        RuleFor(x => x.NoteIds)
            .NotEmpty()
            .NotNull()
            .WithMessage("NoteIds is required.");
        
        RuleForEach(x => x.NoteIds)
            .NotEmpty()
            .NotNull()
            .WithMessage("NoteId is required.");
    }
}