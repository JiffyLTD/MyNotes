using System.Security.Claims;
using Core.Extensions;
using FavoriteNoteService.Application.Commands;
using FavoriteNoteService.Application.Queries;
using FavoriteNoteService.Presentation.Rest.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NoteService.Grpc;

namespace FavoriteNoteService.Presentation.Rest.Apis;

public static class FavoriteNoteApi
{
    public static IEndpointRouteBuilder MapFavoriteNoteApi(this IEndpointRouteBuilder app)
    {
        var api = app
            .MapGroup("api")
            .RequireAuthorization();

        //TODO: пересмотреть название REST методов
        api.MapGet("favorites", GetFavoriteNotesAsync);
        api.MapPost("favorite", CreateFavoriteNoteAsync);
        api.MapDelete("favorite", DeleteFavoriteNoteAsync);
        
        return app;
    }
    
    private static async Task<Ok<GetFavoriteNotesAsyncResponse>> GetFavoriteNotesAsync(
        ClaimsPrincipal claimsPrincipal,
        [FromQuery] int? offset,
        [FromQuery] int? limit,
        [FromServices] IMediator mediator,
        [FromServices] INoteGrpcClient noteGrpcClient,
        CancellationToken cancellationToken
    )
    {
        var accountId = claimsPrincipal.GetUserId();
        
        var query = new GetAllFavoriteNotesQuery
        {
            AccountId = accountId
        };

        var favoriteNoteIds = await mediator.Send(query, cancellationToken);
        
        var response = await noteGrpcClient
            .GetNotesByIds(new GetNotesByIdsRequest { NoteIds = favoriteNoteIds, AccountId = accountId }, cancellationToken);
        
        return TypedResults.Ok(new GetFavoriteNotesAsyncResponse { FavoriteNotes = response.Notes });
    }
    
    private static async Task<Ok<bool>> CreateFavoriteNoteAsync(
        ClaimsPrincipal claimsPrincipal,
        [FromBody] CreateFavoriteNoteAsyncRequest body,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken
    )
    {
        var command = new CreateFavoriteNoteCommand
        {
            AccountId = claimsPrincipal.GetUserId(),
            NoteId = body.NoteId
        };
        
        return TypedResults.Ok(await mediator.Send(command, cancellationToken));
    }
    
    private static async Task<Ok<bool>> DeleteFavoriteNoteAsync(
        ClaimsPrincipal claimsPrincipal,
        [FromBody] DeleteFavoriteNoteAsyncRequest body,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken
    )
    {
        var command = new DeleteFavoriteNoteCommand
        {
            AccountId = claimsPrincipal.GetUserId(),
            NoteId = body.NoteId
        };
        
        return TypedResults.Ok(await mediator.Send(command, cancellationToken));
    }
}