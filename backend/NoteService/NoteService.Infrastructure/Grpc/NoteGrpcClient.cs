using NoteService.Domain.Repositories;
using NoteService.Grpc;

namespace NoteService.Infrastructure.Grpc;

public partial class NoteGrpcClient : INoteGrpcClient
{
    private readonly INoteRepository _noteRepository;

    public NoteGrpcClient(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }
}