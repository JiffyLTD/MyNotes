using FavoriteNoteService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FavoriteNoteService.Infrastructure.Configurations;

public class FavoriteNoteConfiguration: IEntityTypeConfiguration<FavoriteNote>
{
    public void Configure(EntityTypeBuilder<FavoriteNote> builder)
    {
        builder.HasKey(x => new { x.NoteId, x.AccountId });
    }
}