using System.Security.Claims;
using Core.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NoteService.Application.Commands;
using NoteService.Application.Queries;
using NoteService.Presentation.Rest.DTOs;

namespace NoteService.Presentation.Rest.Apis;

public static class NoteApi
{
    public static IEndpointRouteBuilder MapNoteApi(this IEndpointRouteBuilder app)
    {
        var api = app
            .MapGroup("api")
            .RequireAuthorization();

        //TODO: пересмотреть название REST методов
        api.MapGet("notes", GetNotesAsync);
        api.MapGet("notes/deleted", GetDeletedNotesAsync);
        api.MapPost("note/restore", RestoreNoteAsync);
        api.MapPost("note", CreateNoteAsync);
        api.MapPut("note", UpdateNoteAsync);
        api.MapDelete("note", DeleteNoteAsync);
        
        return app;
    }
    
    private static async Task<Ok<GetNotesAsyncResponse>> GetNotesAsync(
        ClaimsPrincipal claimsPrincipal,
        [FromQuery] int? offset,
        [FromQuery] int? limit,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken
    )
    {
        var query = new GetAllNotesQuery
        {
            AccountId = claimsPrincipal.GetUserId()
        };

        var response = await mediator.Send(query, cancellationToken);
        
        return TypedResults.Ok(new GetNotesAsyncResponse { Notes = response });
    }
    
    private static async Task<Ok<GetDeletedNotesAsyncResponse>> GetDeletedNotesAsync(
        ClaimsPrincipal claimsPrincipal,
        [FromQuery] int? offset,
        [FromQuery] int? limit,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken
    )
    {
        var query = new GetAllDeletedNotesQuery
        {
            AccountId = claimsPrincipal.GetUserId()
        };

        var response = await mediator.Send(query, cancellationToken);
        
        return TypedResults.Ok(new GetDeletedNotesAsyncResponse { Notes = response });
    }
    
    private static async Task<Ok<CreateNoteAsyncResponse>> CreateNoteAsync(
        ClaimsPrincipal claimsPrincipal,
        [FromBody] CreateNoteAsyncRequest body,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken
    )
    {
        var command = new CreateNoteCommand
        {
            AccountId = claimsPrincipal.GetUserId(),
            Title = body.Title,
            Content = body.Content
        };

        var response = await mediator.Send(command, cancellationToken);
        
        return TypedResults.Ok(new CreateNoteAsyncResponse { Note = response });
    }
    
    private static async Task<Ok<bool>> DeleteNoteAsync(
        ClaimsPrincipal claimsPrincipal,
        [FromBody] DeleteNoteAsyncRequest body,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken
    )
    {
        var command = new DeleteNoteCommand
        {
            AccountId = claimsPrincipal.GetUserId(),
            NoteId = body.NoteId
        };
        
        return TypedResults.Ok(await mediator.Send(command, cancellationToken));
    }
    
    private static async Task<Ok<bool>> RestoreNoteAsync(
        ClaimsPrincipal claimsPrincipal,
        [FromBody] RestoreNoteAsyncRequest body,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken
    )
    {
        var command = new RestoreNoteCommand
        {
            AccountId = claimsPrincipal.GetUserId(),
            NoteId = body.NoteId
        };
        
        return TypedResults.Ok(await mediator.Send(command, cancellationToken));
    }
    
    private static async Task<Ok<bool>> UpdateNoteAsync(
        ClaimsPrincipal claimsPrincipal,
        [FromBody] UpdateNoteAsyncRequest body,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken
    )
    {
        var command = new UpdateNoteCommand
        {
            NoteId = body.NoteId,
            AccountId = claimsPrincipal.GetUserId(),
            Title = body.Title,
            Content = body.Content
        };
        
        return TypedResults.Ok(await mediator.Send(command, cancellationToken));
    }
}