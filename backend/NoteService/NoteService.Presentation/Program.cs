using System.Reflection;
using Core.Extensions.ServiceCollection;
using Core.HangfireAuthorization;
using Core.Options;
using FluentValidation;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using NoteService.Domain.Repositories;
using NoteService.Infrastructure;
using NoteService.Infrastructure.DbContext;
using NoteService.Infrastructure.Grpc;
using NoteService.Infrastructure.Jobs;
using NoteService.Infrastructure.Repositories;
using NoteService.Presentation.Extensions;
using NoteService.Presentation.Jobs;
using NoteService.Presentation.Options;
using NoteService.Presentation.Rest.Apis;
using ProtoBuf.Grpc.Server;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddValidatorsFromAssembly(Assembly.Load(nameof(Core)), ServiceLifetime.Singleton)
    .AddValidatorsFromAssemblyContaining<Program>(ServiceLifetime.Singleton)
    .RegisterOptions<NoteServiceOptions>(builder.Configuration)
    .RegisterOptions<RabbitMqOptions>(builder.Configuration)
    .RegisterOpenTelemetry(builder.Logging, nameof(NoteService)).Services
    .RegisterAuthenticationSchemes(builder.Configuration)
    .RegisterPooledDbContextFactories<NotesCommandDbContext, NotesQueryDbContext, NoteServiceOptions>(Constants.DatabaseSchemaName)
    .AddSingleton<INotesDbContextFactory, NotesDbContextFactory>(sp => new NotesDbContextFactory(sp))
    .RegisterHangfire<NoteServiceOptions>(nameof(NoteService).ToLower())
    .RegisterMediatr()
    .RegisterRabbitMq()
    .AddSingleton<ICommandNoteRepository, CommandNoteRepository>()
    .AddSingleton<IQueryNoteRepository, QueryNoteRepository>()
    .AddScoped<IDeleteNotesJob, DeleteNotesJob>()
    .AddCors(options =>
    {
        options.AddPolicy("AllowLocalhost",
            b =>
                b.WithOrigins("http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyHeader());
    })
    .RegisterSwagger()
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
        var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<NotesCommandDbContext>>();
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
        
        app.UseHangfireDashboard(options: new DashboardOptions
        {
            Authorization = [new HangfireDashboardAuthorizationFilter()]
        });
    }
    
    app.MapGrpcService<NoteGrpcClient>();
    app.MapNoteApi();

    try
    {
        RecurringJob.AddOrUpdate<IDeleteNotesJob>(
            "cleanup-deleted-notes",
            service => service.StartAsync(),
            Cron.Minutely
        );
    }
    catch (Exception e)
    {
        logger.LogError(e, $"Ошибка при запуске {nameof(IDeleteNotesJob)}");
    }

    await app.RunAsync();

    logger.LogInformation("Сервис успешно запущен.");
}
catch (Exception e)
{
    logger.LogCritical(e, "Ошибка при запуске сервиса");
}