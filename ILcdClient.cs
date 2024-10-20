namespace notlcd;

public interface ILcdClient
{
    Task SetFirstLine(string text, CancellationToken cancellationToken = default);
    Task SetSecondLine(string text, CancellationToken cancellationToken = default);
    Task SetLines(string firstLineText, string secondLineText, CancellationToken cancellationToken = default);
    Task ToggleBacklight(CancellationToken cancellationToken = default);
}