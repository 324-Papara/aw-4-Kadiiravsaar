namespace Papara.Bussiness.Notification;

public interface INotificationService
{
    public void SendEmail(string subject, string email, string content);
}