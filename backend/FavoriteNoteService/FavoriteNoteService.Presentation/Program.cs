using System.Reflection;
using Core.Extensions.ServiceCollection;
using Core.Options;
using Microsoft.EntityFrameworkCore;
using FavoriteNoteService.Domain.Repositories;
using FavoriteNoteService.Infrastructure;
using FavoriteNoteService.Infrastructure.DbContext;
using FavoriteNoteService.Infrastructure.Repositories;
using FavoriteNoteService.Presentation.Extensions;
using FavoriteNoteService.Presentation.Options;
using FavoriteNoteService.Presentation.Rest.Apis;
using FluentValidation;
using ProtoBuf.Grpc.Server;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddValidatorsFromAssembly(Assembly.Load(nameof(Core)), ServiceLifetime.Singleton)
    .AddValidatorsFromAssemblyContaining<Program>(ServiceLifetime.Singleton)
    .RegisterOptions<FavoriteNoteServiceOptions>(builder.Configuration)
    .RegisterOptions<RabbitMqOptions>(builder.Configuration)
    .RegisterOptions<GrpcClientsOptions>(builder.Configuration)
    .RegisterOpenTelemetry(builder.Logging, nameof(FavoriteNoteService)).Services
    .RegisterAuthenticationSchemes(builder.Configuration)
    .RegisterPooledDbContextFactories<FavoriteNotesCommandDbContext, FavoriteNotesQueryDbContext, FavoriteNoteServiceOptions>(Constants.DatabaseSchemaName)
    .AddSingleton<IFavoriteNotesDbContextFactory, FavoriteNotesDbContextFactory>(sp => new FavoriteNotesDbContextFactory(sp))
    .RegisterMediatr()
    .RegisterRabbitMq()
    .AddSingleton<ICommandFavoriteNoteRepository, CommandFavoriteNoteRepository>()
    .AddSingleton<IQueryFavoriteNoteRepository, QueryFavoriteNoteRepository>()
    .AddCors(options =>
    {
        options.AddPolicy("AllowLocalhost",
            b =>
                b.WithOrigins("http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyHeader());
    })
    .RegisterSwagger()
    .RegisterGrpcClients()
    ;

builder.Services.AddCodeFirstGrpc();

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();

try
{
    logger.LogInformation("Сервис запускается...");

    try
    {
        using var scope = app.Services.CreateScope();
        var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<FavoriteNotesCommandDbContext>>();
        await using var dbContext = factory.CreateDbContext();
        await dbContext.Database.MigrateAsync();
        
        logger.LogInformation("Миграции успешно применены");
    }
    catch (Exception e)
    {
        logger.LogError(e,"Ошибка в применении миграций");
    }

    app.UseCors("AllowLocalhost");

    app
        .UseAuthentication()
        .UseAuthorization();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    
    app.MapFavoriteNoteApi();

    await app.RunAsync();

    logger.LogInformation("Сервис успешно запущен.");
}
catch (Exception e)
{
    logger.LogCritical(e, "Ошибка при запуске сервиса");
}