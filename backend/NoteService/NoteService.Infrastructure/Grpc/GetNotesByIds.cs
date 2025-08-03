using LinqToDB.EntityFrameworkCore;
using NoteService.Grpc;

namespace NoteService.Infrastructure.Grpc;

public partial class NoteGrpcClient 
{
    public async Task<GetNotesByIdsResponse> GetNotesByIds(GetNotesByIdsRequest request, CancellationToken cancellationToken)
    {
        await using var dbContext = _notesDbContextFactory.CreateDbContext();

        var notes = await dbContext.Notes
            .Where(x => request.NoteIds.Contains(x.Id))
            .Select(x => new Note
            {
                Id = x.Id,
                AccountId = x.AccountId,
                Title = x.Title,
                Content = x.Content,
                UpdatedAt = x.UpdatedAt
            })
            .ToArrayAsyncLinqToDB(cancellationToken);
            
        return new GetNotesByIdsResponse
        {
            Notes = notes
        };
    }
}