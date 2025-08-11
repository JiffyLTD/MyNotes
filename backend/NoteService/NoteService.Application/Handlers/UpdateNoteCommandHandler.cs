using MediatR;
using NoteService.Application.Commands;
using NoteService.Domain.DTOs;
using NoteService.Domain.Repositories;

namespace NoteService.Application.Handlers;

public class UpdateNoteCommandHandler(ICommandNoteRepository repository) : IRequestHandler<UpdateNoteCommand, bool>
{
    public async Task<bool> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        await repository.UpdateAsync(new UpdateNoteDto
        {
            NoteId = request.NoteId,
            AccountId = request.AccountId,
            Title = request.Title,
            Content = request.Content
        }, cancellationToken);
        
        return true;
    }
}