using NoteService.Grpc;
using NoteService.Infrastructure.DbContext;

namespace NoteService.Infrastructure.Grpc;

public partial class NoteGrpcClient : INoteGrpcClient
{
    private readonly INotesDbContextFactory _notesDbContextFactory;

    public NoteGrpcClient(INotesDbContextFactory notesDbContextFactory)
    {
        _notesDbContextFactory = notesDbContextFactory;
    }
}