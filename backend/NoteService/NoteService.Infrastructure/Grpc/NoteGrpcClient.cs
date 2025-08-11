using NoteService.Domain.Repositories;
using NoteService.Grpc;

namespace NoteService.Infrastructure.Grpc;

public partial class NoteGrpcClient : INoteGrpcClient
{
    private readonly IQueryNoteRepository _queryNoteRepository;

    public NoteGrpcClient(IQueryNoteRepository queryNoteRepository)
    {
        _queryNoteRepository = queryNoteRepository;
    }
}