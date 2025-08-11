using Core.Exceptions;
using LinqToDB.EntityFrameworkCore;
using NoteService.Domain.DTOs;
using NoteService.Domain.Entities;
using NoteService.Domain.Repositories;
using NoteService.Infrastructure.DbContext;

namespace NoteService.Infrastructure.Repositories;

public class QueryNoteRepository(INotesDbContextFactory dbContextFactory) : IQueryNoteRepository
{
    public async Task<List<Note>> GetAllAsync(Guid accountId, CancellationToken cancellationToken)
    {
        await using var context = dbContextFactory.CreateDbContext<NotesQueryDbContext>();
        
        return await context.Notes
            .Where(n =>
                n.AccountId == accountId &&
                n.DeletedAt == null
            )   
            .OrderBy(x => x.CreatedAt)
            .ToListAsyncLinqToDB(cancellationToken);
    }

    public async Task<List<Note>> GetAllByIdsAsync(GetNotesByIdsDto dto, CancellationToken cancellationToken)
    {
        await using var context = dbContextFactory.CreateDbContext<NotesQueryDbContext>();
        
        return await context.Notes
            .Where(n =>
                n.AccountId == dto.AccountId &&
                n.DeletedAt == null && 
                dto.NoteIds.Contains(n.Id)
            )   
            .OrderBy(x => x.CreatedAt)
            .ToListAsyncLinqToDB(cancellationToken);
    }

    public async Task<List<Note>> GetAllDeletedAsync(Guid accountId, CancellationToken cancellationToken)
    {
        await using var context = dbContextFactory.CreateDbContext<NotesQueryDbContext>();
        
        return await context.Notes
            .Where(n => 
                n.AccountId == accountId &&
                n.DeletedAt != null
                )
            .OrderBy(x => x.DeletedAt)
            .ToListAsyncLinqToDB(cancellationToken);
    }

    public async Task<Note> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await using var context = dbContextFactory.CreateDbContext<NotesQueryDbContext>();

        var note = await context.Notes
            .FirstOrDefaultAsyncLinqToDB(n => n.Id == id, cancellationToken);
        
        if (note == null)
            throw new NotFoundException($"Заметка с ID = '{id}' не найдена");
        
        return note;
    }
}