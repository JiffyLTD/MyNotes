using MediatR;
using NoteService.Application.Queries;
using NoteService.Domain.Entities;
using NoteService.Domain.Repositories;

namespace NoteService.Application.Handlers;

public class GetAllDeletedNotesQueryHandler(IQueryNoteRepository repository) : IRequestHandler<GetAllDeletedNotesQuery, List<Note>>
{
    public async Task<List<Note>> Handle(GetAllDeletedNotesQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetAllDeletedAsync(request.AccountId, cancellationToken);
    }
}