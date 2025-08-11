using Core.Exceptions;
using LinqToDB.EntityFrameworkCore;
using NoteService.Domain.DTOs;
using NoteService.Domain.Entities;
using NoteService.Domain.Repositories;
using NoteService.Infrastructure.DbContext;

namespace NoteService.Infrastructure.Repositories;

public class CommandNoteRepository(INotesDbContextFactory dbContextFactory) : ICommandNoteRepository
{
    public async Task AddAsync(Note note, CancellationToken cancellationToken)
    {
        await using var context = dbContextFactory.CreateDbContext<NotesCommandDbContext>();
        await context.Notes.AddAsync(note, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(DeleteNoteDto dto, CancellationToken cancellationToken)
    {
        await using var context = dbContextFactory.CreateDbContext<NotesCommandDbContext>();
        var note = await context.Notes
            .FirstOrDefaultAsyncLinqToDB(x => 
                x.Id == dto.NoteId &&
                x.AccountId == dto.AccountId
                , cancellationToken);

        if (note == null)
            throw new NotFoundException($"Заметка с ID = '{dto.NoteId}' не найдена у пользователя с ID = {dto.AccountId}");

        note.DeletedAt = DateTime.UtcNow;
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task RestoreAsync(RestoreNoteDto dto, CancellationToken cancellationToken)
    {
        await using var context = dbContextFactory.CreateDbContext<NotesCommandDbContext>();
        var note = await context.Notes
            .FirstOrDefaultAsyncLinqToDB(x => 
                    x.Id == dto.NoteId &&
                    x.AccountId == dto.AccountId
                , cancellationToken);

        if (note == null)
            throw new NotFoundException($"Заметка с ID = '{dto.NoteId}' не найдена у пользователя с ID = {dto.AccountId}");

        note.DeletedAt = null;
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(UpdateNoteDto dto, CancellationToken cancellationToken)
    {
        await using var context = dbContextFactory.CreateDbContext<NotesCommandDbContext>();
        
        var note = await context.Notes
            .FirstOrDefaultAsyncLinqToDB(n => 
                n.Id == dto.NoteId && 
                n.AccountId == dto.AccountId
                , cancellationToken);
        
        if (note == null)
            throw new NotFoundException($"Заметка с ID = '{dto.NoteId}' не найдена у пользователя с ID = {dto.AccountId}");
        
        note.Title = dto.Title;
        note.Content = dto.Content;
        note.UpdatedAt = DateTime.UtcNow;
        
        context.Notes.Update(note);
        await context.SaveChangesAsync(cancellationToken);
    }
}