using MediatR;
using NoteService.Application.Queries;
using NoteService.Domain.Entities;
using NoteService.Domain.Repositories;

namespace NoteService.Application.Handlers;

public class GetNotesByIdsQueryHandler(IQueryNoteRepository repository) : IRequestHandler<GetNotesByIdsQuery, List<Note>>
{
    public async Task<List<Note>> Handle(GetNotesByIdsQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetAllAsync(request.AccountId, cancellationToken);
    }
}