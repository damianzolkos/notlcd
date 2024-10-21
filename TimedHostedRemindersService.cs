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
        foreach (var reminder in  DB.Reminders.Where(x => !x.Sent).ToList())
        {
            if (targetTime.Hour == reminder.Hour && targetTime.Minute == reminder.Minute)
            {
                _notificationService.AddNotification(new(reminder.FirstLineText, reminder.SecondLineText, true));
                reminder.Sent = true;
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