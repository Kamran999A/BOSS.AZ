using System;

namespace Bossaz.Exceptions
{
    public class InvalidLinkFormat:ApplicationException
    {
        public InvalidLinkFormat(string message):base(message)
        {
        }
    }
}