using Microsoft.EntityFrameworkCore;
using NoteService.Domain.Entities;

namespace NoteService.Infrastructure.DbContext;

public class NotesDbContext(DbContextOptions<NotesDbContext> options) : Microsoft.EntityFrameworkCore.DbContext(options)
{
    public DbSet<Note> Notes => Set<Note>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(Constants.DatabaseSchemaName);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotesDbContext).Assembly);
    }
}