using MediatR;
using NoteService.Application.Commands;
using NoteService.Domain.DTOs;
using NoteService.Domain.Repositories;

namespace NoteService.Application.Handlers;

public class RestoreNoteCommandHandler(ICommandNoteRepository repository) : IRequestHandler<RestoreNoteCommand, bool>
{
    public async Task<bool> Handle(RestoreNoteCommand request, CancellationToken cancellationToken)
    {
        await repository.RestoreAsync(new RestoreNoteDto
        {
            NoteId = request.NoteId,
            AccountId = request.AccountId
        }, cancellationToken);
        
        return true;
    }
}