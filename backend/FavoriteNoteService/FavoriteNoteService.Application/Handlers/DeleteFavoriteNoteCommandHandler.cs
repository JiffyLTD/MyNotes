using FavoriteNoteService.Application.Commands;
using FavoriteNoteService.Domain.DTOs;
using FavoriteNoteService.Domain.Repositories;
using MediatR;

namespace FavoriteNoteService.Application.Handlers;

public class DeleteFavoriteNoteCommandHandler(ICommandFavoriteNoteRepository repository) : IRequestHandler<DeleteFavoriteNoteCommand, bool>
{
    public async Task<bool> Handle(DeleteFavoriteNoteCommand request, CancellationToken cancellationToken)
    {
        var dto = new DeleteFavoriteNoteDto
        {
            NoteId = request.NoteId,
            AccountId = request.AccountId
        };
        
        await repository.DeleteAsync(dto, cancellationToken);
        return true;
    }
}