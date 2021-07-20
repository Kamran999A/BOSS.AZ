using System;
using System.Runtime.Hosting;
namespace Bossaz.Exceptions
{
    public class LoginException : ApplicationException
    {
        public LoginException(string message) : base(message)
        {
        }
    }
}