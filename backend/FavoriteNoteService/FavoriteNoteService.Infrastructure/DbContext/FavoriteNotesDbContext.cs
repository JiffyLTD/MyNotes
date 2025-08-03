using FavoriteNoteService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FavoriteNoteService.Infrastructure.DbContext;

public class FavoriteNotesDbContext(DbContextOptions<FavoriteNotesDbContext> options) : Microsoft.EntityFrameworkCore.DbContext(options)
{
    public DbSet<FavoriteNote> FavoriteNotes => Set<FavoriteNote>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(Constants.DatabaseSchemaName);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FavoriteNotesDbContext).Assembly);
    }
}