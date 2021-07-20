using System;
namespace Bossaz.Exceptions
{
    public class RegisterException : ApplicationException
    {
        public RegisterException(string message) : base(message)
        {
        }
    }
}