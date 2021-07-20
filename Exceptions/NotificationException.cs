using System;
namespace Bossaz.Exceptions
{
    public class NotificationException : ApplicationException
    {
        public NotificationException(string message) : base(message)
        {
        }
    }
}