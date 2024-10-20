using notlcd.Models;

namespace notlcd
{
    public interface INotificationService
    {
        void AddNotification(string firstLineText, string? secondLineText);
        Task Send(Notification notification, CancellationToken cancellationToken = default);
    }
}