namespace NoteService.Infrastructure.Jobs;

public interface IDeleteNotesJob
{
    public Task StartAsync();
}