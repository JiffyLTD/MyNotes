using NoteService.Domain.DTOs;
using NoteService.Grpc;

namespace NoteService.Infrastructure.Grpc;

public partial class NoteGrpcClient
{
    public async Task<GetNotesByIdsResponse> GetNotesByIds(GetNotesByIdsRequest request,
        CancellationToken cancellationToken)
    {
        var notes = await _queryNoteRepository.GetAllByIdsAsync(new GetNotesByIdsDto
        {
            NoteIds = request.NoteIds,
            AccountId = request.AccountId
        }, cancellationToken);

        return new GetNotesByIdsResponse
        {
            Notes = notes
                .Select(x => new Note
                {
                    Id = x.Id,
                    AccountId = x.AccountId,
                    Title = x.Title,
                    Content = x.Content,
                    UpdatedAt = x.UpdatedAt
                })
                .ToArray()
        };
    }
}