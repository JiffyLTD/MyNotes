using Microsoft.EntityFrameworkCore;
using NoteService.Domain.Entities;

namespace NoteService.Infrastructure.DbContext;

public abstract class BaseNotesDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    protected BaseNotesDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Note> Notes => Set<Note>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(Constants.DatabaseSchemaName);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseNotesDbContext).Assembly);
    }
}

public sealed class NotesCommandDbContext : BaseNotesDbContext
{
    public NotesCommandDbContext(DbContextOptions<NotesCommandDbContext> options)
        : base(options)
    {
    }
}

public sealed class NotesQueryDbContext : BaseNotesDbContext
{
    public NotesQueryDbContext(DbContextOptions<NotesQueryDbContext> options)
        : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }
}