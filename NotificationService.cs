using notlcd.Models;

namespace notlcd
{
    public class NotificationService : INotificationService
    {
        private readonly ILcdClient _lcdClient;

        public NotificationService(ILcdClient lcdClient)
        {
            _lcdClient = lcdClient;
        }

        public void AddNotification(string firstLineText, string? secondLineText)
        {
            DB.Notifications.Add(new Notification(firstLineText, secondLineText));
        }

        public void AddNotification(Notification notification)
        {
            DB.Notifications.Add(notification);
        }

        public async Task Send(Notification notification, CancellationToken cancellationToken = default)
        {
            if (notification.Blink)
                await SendNotificationBlink(cancellationToken);
            await SendNotification(notification.FirstLineText, notification.SecondLineText, _lcdClient, cancellationToken);
        }

        private static Task SendNotification(string firstLine, string? secondLine, ILcdClient client, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(secondLine))
            {
                return client.SetFirstLine(firstLine, cancellationToken);
            }
            else
            {
                return client.SetLines(firstLine, secondLine, cancellationToken);
            }
        }

        private async Task SendNotificationBlink(CancellationToken cancellationToken = default)
        {
            // TODO: unxd this
            await _lcdClient.ToggleBacklight(cancellationToken);
            Thread.Sleep(200);
            await _lcdClient.ToggleBacklight(cancellationToken);
            Thread.Sleep(200);
            await _lcdClient.ToggleBacklight(cancellationToken);
            Thread.Sleep(200);
            await _lcdClient.ToggleBacklight(cancellationToken);
            Thread.Sleep(200);
            await _lcdClient.ToggleBacklight(cancellationToken);
            Thread.Sleep(200);
            await _lcdClient.ToggleBacklight(cancellationToken);
        }
    }
}