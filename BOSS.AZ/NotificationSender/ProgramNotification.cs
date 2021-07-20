using Bossaz.Abstracts;
using Bossaz.Entities;

namespace Bossaz.NotificationSender
{
    class ProgramNotification
    {
        public static void Send(User user, Notification notf)
        {
            user.Notifications.Add(notf);
        }
    }
}
