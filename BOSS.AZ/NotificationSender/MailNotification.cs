using Bossaz.Abstracts;
using Bossaz.Entities;
using Bossaz.Network;
namespace Bossaz.NotificationSender
{
    public class MailNotification
    {
        public static void Send(User user, Notification notification)
        {
            if (Mail.IsEnable)
            {
                Mail.SendMail(user.Mail, notification.Title, notification.Message, notification.Message2);
            }
        }
    }
}