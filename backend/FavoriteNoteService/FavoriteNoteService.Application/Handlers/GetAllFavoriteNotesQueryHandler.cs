using FavoriteNoteService.Application.Queries;
using FavoriteNoteService.Domain.Repositories;
using MediatR;

namespace FavoriteNoteService.Application.Handlers;

public class GetAllFavoriteNotesQueryHandler(IQueryFavoriteNoteRepository repository) : IRequestHandler<GetAllFavoriteNotesQuery, Guid[]>
{
    public async Task<Guid[]> Handle(GetAllFavoriteNotesQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetAllFavoriteNotesAsync(request.AccountId, cancellationToken);
    }
}