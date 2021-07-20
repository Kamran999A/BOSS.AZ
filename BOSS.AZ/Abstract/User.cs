using System;
using System.Collections.Generic;
using Bossaz.Entities;
using Bossaz.Exceptions;

namespace Bossaz.Abstracts
{
    public abstract class User : Id
    {
        private string _password;
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }
        public string Username { get; set; }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
            }
        }
        public string City { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public List<Notification> Notifications { get; set; }
        protected User()
        {
            Notifications = new List<Notification>();
        }
        public void ShowShortNotfInfo(bool onlyUnread = false)
        {
            if (Notifications.Count == 0)
                throw new NotificationException("There is no notification!");
            var flag = false;
            if (onlyUnread)
            {
                foreach (var notification in Notifications)
                {
                    if (!notification.IsRead)
                    {
                        Console.WriteLine($"Guid: {notification.Guid}");
                        Console.WriteLine($"Title: {notification.Title}");
                        flag = true;
                    }
                }
            }
            else
            {
                foreach (var notification in Notifications)
                {
                    Console.WriteLine($"Guid: {notification.Guid}");
                    Console.WriteLine($"Title: {notification.Title}");
                    flag = true;
                }
            }
            if (!flag)
                throw new NotificationException("There is no notification!");
        }
    }
}