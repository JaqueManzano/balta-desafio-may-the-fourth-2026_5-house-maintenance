public interface IHomeTaskService
{
    Task<string> SendAsync(string text, CancellationToken cancellationToken);
}