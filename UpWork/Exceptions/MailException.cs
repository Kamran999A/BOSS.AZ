using System;

namespace Bossaz.Exceptions
{
    public class MailException:ApplicationException
    {
        public MailException(string message):base(message)
        {
            
        }
    }
}