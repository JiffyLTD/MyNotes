using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoteService.Domain.Entities;

namespace NoteService.Infrastructure.Configurations;

public class NoteConfiguration: IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Title)
            .HasMaxLength(Note.MaxTitleLength);
        
        builder.Property(x => x.Content)
            .HasMaxLength(Note.MaxContentLength);
    }
}