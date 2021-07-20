using System;
using Bossaz.Abstracts;

namespace Bossaz.Entities
{
    public class Notification : Id
    {
        public bool IsRead { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Message2 { get; set; }
        public override string ToString()
        {
            return $@"Title: {Title}
Message:
{Message}{Message2}";
        }
        public static Notification operator ++(Notification notification)
        {
            notification.IsRead = true;
            return notification;
        }
    }
}