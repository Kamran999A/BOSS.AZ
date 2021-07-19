using System;
using System.Runtime.Hosting;

namespace Bossaz.Exceptions
{
    public class DatabaseException:ApplicationException
    {
        public DatabaseException(string message):base(message)
        {
        }
    }
}