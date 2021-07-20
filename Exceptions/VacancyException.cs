using System;
namespace Bossaz.Exceptions
{
    public class VacancyException : ApplicationException
    {
        public VacancyException(string message) : base(message)
        {
        }
    }
}