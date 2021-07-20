using System;
using Bossaz.Abstracts;
using Bossaz.Entities;
namespace Bossaz.NotificationSender
{
    public delegate void NotificationSender(User user, Notification notification);
    public class NotificationPublisher
    {
        public static event NotificationSender EventHandler;
        public static void OnSend(User user, Notification notification)
        {
            EventHandler?.Invoke(user, notification);
        }
    }
}