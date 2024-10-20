namespace notlcd;

public class TimedHostedRemindersService : IHostedService, IDisposable
{
    private readonly INotificationService _notificationService;
    private Timer? _timer = null;

    public TimedHostedRemindersService(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public Task StartAsync(CancellationToken stoppingToken)
    {
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
        return Task.CompletedTask;
    }

    private void DoWork(object? state)
    {
        var targetTime = DateTimeService.GetCurrentDateTime();
        foreach (var reminder in  DB.Reminders.ToList())
        {
            if (targetTime.Hour == reminder.Hour && targetTime.Minute == reminder.Minute)
            {
                // TODO: there is a bug here that the notification will be shown for a whole minute
                _notificationService.AddNotification(reminder.FirstLineText, reminder.SecondLineText);
            }
        }
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