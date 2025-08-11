using FavoriteNoteService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FavoriteNoteService.Infrastructure.DbContext;

public class BaseFavoriteNotesDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    protected BaseFavoriteNotesDbContext(DbContextOptions options) : base(options) { }
    public DbSet<FavoriteNote> FavoriteNotes => Set<FavoriteNote>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(Constants.DatabaseSchemaName);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseFavoriteNotesDbContext).Assembly);
    }
}

public sealed class FavoriteNotesCommandDbContext : BaseFavoriteNotesDbContext
{
    public FavoriteNotesCommandDbContext(DbContextOptions<FavoriteNotesCommandDbContext> options)
        : base(options)
    {
    }
}

public sealed class FavoriteNotesQueryDbContext : BaseFavoriteNotesDbContext
{
    public FavoriteNotesQueryDbContext(DbContextOptions<FavoriteNotesQueryDbContext> options)
        : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }
}