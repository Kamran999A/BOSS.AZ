using System;
using System.Collections.Generic;
using Bossaz.Entities;
using Bossaz.Exceptions;
namespace Bossaz.Helpers
{
    public static class WorkerHelper
    {
        public static void ShowVacancies(List<Vacancy> vacancies)
        {
            if (vacancies.Count == 0)
                throw new CvException("There is no vacancy!");

            foreach (var vacancy in vacancies)
            {
                Console.WriteLine("------------------------------------------");
                Console.WriteLine(vacancy);
            }
        }
    }
}