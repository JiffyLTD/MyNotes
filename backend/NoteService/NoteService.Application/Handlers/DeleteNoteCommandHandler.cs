using MediatR;
using NoteService.Application.Commands;
using NoteService.Domain.DTOs;
using NoteService.Domain.Repositories;

namespace NoteService.Application.Handlers;

public class DeleteNoteCommandHandler(ICommandNoteRepository repository) : IRequestHandler<DeleteNoteCommand, bool>
{
    public async Task<bool> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        await repository.DeleteAsync(new DeleteNoteDto
        {
            NoteId = request.NoteId,
            AccountId = request.AccountId
        }, cancellationToken);
        
        return true;
    }
}