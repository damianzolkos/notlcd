using notlcd.Models;

namespace notlcd
{
    public interface INotificationService
    {
        void AddNotification(string firstLineText, string? secondLineText;
        void AddNotification(Notification notification);
        Task Send(Notification notification, CancellationToken cancellationToken = default);
    }
}