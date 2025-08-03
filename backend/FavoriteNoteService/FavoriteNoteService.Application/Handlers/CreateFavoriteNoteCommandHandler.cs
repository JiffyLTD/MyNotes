using FavoriteNoteService.Application.Commands;
using FavoriteNoteService.Domain.Entities;
using FavoriteNoteService.Domain.Repositories;
using MediatR;

namespace FavoriteNoteService.Application.Handlers;

public class CreateFavoriteNoteCommandHandler(IFavoriteNoteRepository repository) : IRequestHandler<CreateFavoriteNoteCommand, bool>
{
    public async Task<bool> Handle(CreateFavoriteNoteCommand request, CancellationToken cancellationToken)
    {
        var note = new FavoriteNote
        {
            NoteId = request.NoteId,
            AccountId = request.AccountId
        };

        await repository.AddAsync(note, cancellationToken);
        return true;
    }
}