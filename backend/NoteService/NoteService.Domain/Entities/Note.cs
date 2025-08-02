namespace NoteService.Domain.Entities;

public sealed class Note
{
    public const int MaxTitleLength = 1024;
    public const int MaxContentLength = 8192;
    
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}