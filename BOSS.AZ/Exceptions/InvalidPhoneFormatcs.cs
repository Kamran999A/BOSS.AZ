using System;
namespace Bossaz.Exceptions
{
    public class InvalidPhoneFormat : ApplicationException
    {
        public InvalidPhoneFormat(string message) : base(message)
        {
        }
    }
}