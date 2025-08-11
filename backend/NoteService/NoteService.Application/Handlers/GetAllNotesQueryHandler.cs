using MediatR;
using NoteService.Application.Queries;
using NoteService.Domain.Entities;
using NoteService.Domain.Repositories;

namespace NoteService.Application.Handlers;

public class GetAllNotesQueryHandler(IQueryNoteRepository repository) : IRequestHandler<GetAllNotesQuery, List<Note>>
{
    public async Task<List<Note>> Handle(GetAllNotesQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetAllAsync(request.AccountId, cancellationToken);
    }
}