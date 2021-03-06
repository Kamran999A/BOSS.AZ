using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Bossaz.Abstracts;
using Bossaz.ConsoleInterface;
using Bossaz.DataFilter;
using Bossaz.Entities;
using Bossaz.Enums;
using Bossaz.Helpers;
using Bossaz.Logger;
using Bossaz.Sides.UserAccess;

namespace Bossaz.Sides.Employee
{
    public static class AdsSection
    {
        public static void Start(Worker worker, Database.Database db)
        {
            var logger = new Logger.ConsoleLogger();
            var seeAdsLoop = true;
            var mainVacancies = db.GetVacancies();
            IList<Vacancy> vacancies = mainVacancies;
            while (seeAdsLoop)
            {
                Console.Clear();
                UserAccessSide.Bosik();
                ExceptionHandle.Handle(VacancyHelper.SeeAds, vacancies);
                ConsoleScreen.PrintMenu(ConsoleScreen.FilterMenu, ConsoleColor.Blue);
                var filterMenuChoice = (FilterMenuEnum)ConsoleScreen.Input(ConsoleScreen.FilterMenu.Count);
                switch (filterMenuChoice)
                {
                    case FilterMenuEnum.Select:
                    {
                        var loop2 = true;
                        while (loop2)
                        {
                            Console.Clear();
                                UserAccessSide.Bosik();
                                if (!ExceptionHandle.Handle(VacancyHelper.SeeAds, vacancies))
                                break;
                            Console.WriteLine("Vacancy id: ");
                            var vacId = UserHelper.InputGuid();
                            try
                            {
                                var vacancy = VacancyHelper.GetVacancy(vacId, vacancies);
                                while (true)
                                {
                                    var requestFromWorker = vacancy.CheckWorkerRequest(worker.Guid);
                                    Console.Clear();
                                        UserAccessSide.Bosik();

                                        Console.WriteLine(vacancy++); 
                                    Database.Database.Changes = true;
                                    Console.WriteLine();
                                    Console.WriteLine($"1. {(requestFromWorker ? "Cancel" : "Request")}"); ;
                                    Console.WriteLine("2. Back");
                                    var choice = ConsoleScreen.Input(2);
                                    if (choice == 1)
                                    {
                                        if (requestFromWorker)
                                        {
                                            vacancy.RemoveRequest(worker.Guid);
                                        }
                                        else
                                        {
                                            Cv cv = null;
                                            while (true)
                                            {
                                                Console.Clear();
                                                    UserAccessSide.Bosik();

                                                    if (!ExceptionHandle.Handle(worker.ShowAllCv, true))
                                                {
                                                    LoggerPublisher.OnLogInfo("Please add public Cv!");
                                                    ConsoleScreen.Clear();
                                                    break;
                                                }
                                                var cvId = UserHelper.InputGuid();
                                                try
                                                {
                                                    cv = CvHelper.GetCv(cvId, worker.Cvs);
                                                    break;
                                                }
                                                catch (Exception e)
                                                {
                                                    LoggerPublisher.OnLogError(e.Message);
                                                    ConsoleScreen.Clear();
                                                }
                                            }
                                            if (cv != null)
                                            {
                                                vacancy.SendRequest(worker.Guid, cv.Guid);
                                            }
                                        }
                                            break;
                                        }
                                    else if (choice == 2)
                                    {
                                        if (ConsoleScreen.DisplayMessageBox("Info", "Do you want to see other Ads?",
                                            MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                            loop2 = false;
                                        break;
                                    }
                                }
                                if (loop2)
                                    continue;
                            }
                            catch (Exception e)
                            {
                                LoggerPublisher.OnLogError(e.Message);
                                ConsoleScreen.Clear();
                            }
                            if (!loop2 || ConsoleScreen.DisplayMessageBox("Info", "Do you want to try again?",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                break;
                        }
                        break;
                    }
                    case FilterMenuEnum.ByCategory:
                    {
                        Console.Clear();
                            UserAccessSide.Bosik();

                            vacancies = ExceptionHandle.Handle(VacancyFilter.FilterByCategory, UserHelper.InputCategory(),
                            vacancies);
                        break;
                    }
                    case FilterMenuEnum.ByEducation:
                    {
                        Console.Clear();
                            UserAccessSide.Bosik();

                            vacancies = ExceptionHandle.Handle(VacancyFilter.FilterByEducation, UserHelper.InputEducation(), vacancies);
                        break;
                    }
                    case FilterMenuEnum.ByExperience:
                    {
                        Console.Clear();
                            UserAccessSide.Bosik();

                            vacancies = ExceptionHandle.Handle(VacancyFilter.FilterByExperience, UserHelper.InputExperience(), vacancies);
                        break;
                    }
                    case FilterMenuEnum.ByRegion:
                    {
                        Console.Clear();
                            UserAccessSide.Bosik();

                            vacancies = ExceptionHandle.Handle(VacancyFilter.FilterByRegion, UserHelper.InputRegion(), vacancies);
                        break;
                    }
                    case FilterMenuEnum.BySalary:
                    {
                        Console.Clear();
                            UserAccessSide.Bosik();

                            var input = UserHelper.InputSalary();
                        var salary = UserHelper.ParseSalary(input);
                        vacancies = ExceptionHandle.Handle(VacancyFilter.FilterBySalary, salary, vacancies);
                        break;
                    }
                    case FilterMenuEnum.Reset:
                    {
                        vacancies = mainVacancies;
                        break;
                    }
                    case FilterMenuEnum.Back:
                    {
                        seeAdsLoop = false;
                        break;
                    }
                }
            }
        }
    }
}