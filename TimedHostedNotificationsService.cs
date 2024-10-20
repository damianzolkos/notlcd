namespace notlcd;

public class TimedHostedNotificationsService : IHostedService, IDisposable
{
    private readonly INotificationService _notificationService;
    private Timer? _timer = null;

    public TimedHostedNotificationsService(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public Task StartAsync(CancellationToken stoppingToken)
    {
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(6));
        return Task.CompletedTask;
    }

    private void DoWork(object? state)
    {
        foreach (var notification in  DB.Notifications.ToList())
        {
            Task.Run(async () => await _notificationService.Send(notification));
            DB.Notifications.Remove(notification);
            return;
        }

        if (DB.Notifications.ToList().Count == 0)
        {
            SendDateAndTimeNotification();
        }
    }

    private void SendDateAndTimeNotification()
    {
        var targetTime = DateTimeService.GetCurrentDateTime();
        Task.Run(async () => await _notificationService.Send(new("     " + targetTime.ToShortTimeString() + "      ", "  " + targetTime.ToString("dd/MM/yyyy"), false)));
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}