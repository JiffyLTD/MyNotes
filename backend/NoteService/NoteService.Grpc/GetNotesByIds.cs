using ProtoBuf;

namespace NoteService.Grpc;

[ProtoContract]
public class GetNotesByIdsRequest
{
    [ProtoMember(1)]
    public Guid[] NoteIds { get; set; }
    
    [ProtoMember(2)]
    public Guid AccountId { get; set; }
}

[ProtoContract]
public class GetNotesByIdsResponse
{
    [ProtoMember(1)] 
    public Note[] Notes { get; set; } = [];
}

[ProtoContract]
public class Note
{
    [ProtoMember(1)]
    public Guid Id { get; set; }
    [ProtoMember(2)]
    public Guid AccountId { get; set; }
    [ProtoMember(3)]
    public required string Title { get; set; }
    [ProtoMember(4)]
    public required string Content { get; set; }
    [ProtoMember(5)]
    public DateTime UpdatedAt { get; set; }
}

public partial interface INoteGrpcClient
{
    Task<GetNotesByIdsResponse> GetNotesByIds(GetNotesByIdsRequest request, CancellationToken cancellationToken);
}