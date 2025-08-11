using MediatR;
using NoteService.Application.Commands;
using NoteService.Domain.Entities;
using NoteService.Domain.Repositories;

namespace NoteService.Application.Handlers;

public class CreateNoteCommandHandler(ICommandNoteRepository repository) : IRequestHandler<CreateNoteCommand, Note>
{
    public async Task<Note> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        
        var note = new Note
        {
            Id = Guid.NewGuid(),
            AccountId = request.AccountId,
            Title = request.Title,
            Content = request.Content,
            CreatedAt = now,
            UpdatedAt = now,
            DeletedAt = null
        };

        await repository.AddAsync(note, cancellationToken);
        return note;
    }
}