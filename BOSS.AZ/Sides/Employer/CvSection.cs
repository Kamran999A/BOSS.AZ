using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Bossaz.ConsoleInterface;
using Bossaz.DataFilter;
using Bossaz.Entities;
using Bossaz.Enums;
using Bossaz.Helpers;
using Bossaz.Logger;
using Bossaz.Sides.UserAccess;

namespace Bossaz.Sides.Employer
{
    public static class CvSection
    {
        public static void Start(Entities.Employer employer, Database.Database db)
        {
            var logger = new Logger.ConsoleLogger();
            var seeCvsLoop = true;
            var mainCvs = db.GetCvs();
            IList<Cv> cvs = mainCvs;
            while (seeCvsLoop)
            {
                                        label:
                Console.Clear();
                UserAccessSide.Bosik();
                ExceptionHandle.Handle(CvHelper.SeeCvs, cvs);
                ConsoleScreen.PrintMenu(ConsoleScreen.FilterMenu, ConsoleColor.Blue);
                var filterMenuChoice = (FilterMenuEnum)ConsoleScreen.Input(ConsoleScreen.FilterMenu.Count);
                switch (filterMenuChoice)
                {
                    case FilterMenuEnum.Select:
                    {
                        var loop3 = true;
                        while (loop3)
                        {
                            Console.Clear();
                                UserAccessSide.Bosik();
                                if (!ExceptionHandle.Handle(CvHelper.SeeCvs, cvs))
                                break;
                            Console.WriteLine("Cv id: ");
                            var cvId = UserHelper.InputGuid();
                            try
                            {
                                var cv = CvHelper.GetCv(cvId, cvs);
                                Console.Clear();
                                while (true)
                                {
                                        UserAccessSide.Bosik();
                                        var requestFromEmployer = cv.CheckEmployerRequest(employer.Guid);
                                    Console.Clear();
                                        UserAccessSide.Bosik();
                                        Console.WriteLine(cv++); 
                                    Database.Database.Changes = true;
                                    Console.WriteLine();
                                    Console.WriteLine($"1. {(requestFromEmployer ? "Cancel" : "Request")}");
                                    Console.WriteLine("2. Back");
                                    var choice = ConsoleScreen.Input(2);
                                    if (choice == 1)
                                    {
                                        if (requestFromEmployer)
                                        {
                                            cv.RemoveRequest(employer.Guid);
                                                break;
                                        }
                                        else
                                        {
                                            Vacancy vacancy = null;
                                            while (true)
                                            {
                                                Console.Clear();
                                                    UserAccessSide.Bosik();
                                                    if (!ExceptionHandle.Handle(employer.ShowAllAds, true))
                                                {
                                                    LoggerPublisher.OnLogInfo("Please add public Vacancy!");
                                                    ConsoleScreen.Clear();
                                                        goto label;
                                                }
                                                var vacancyId = UserHelper.InputGuid();
                                                try
                                                {
                                                    vacancy = VacancyHelper.GetVacancy(vacancyId, employer.Vacancies);
                                                    break;
                                                }
                                                catch (Exception e)
                                                {
                                                    LoggerPublisher.OnLogError(e.Message);
                                                    ConsoleScreen.Clear();
                                                }
                                            }
                                            if (vacancy != null)
                                            {
                                                cv.SendRequest(employer.Guid, vacancy.Guid);
                                                break;
                                            }
                                            }
                                            break;
                                        }
                                    else if (choice == 2)
                                    {
                                        if (ConsoleScreen.DisplayMessageBox("Info", "Do you want to see other Cvs?",
                                            MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                            loop3 = false;
                                        break;
                                    }
                                }

                                if (loop3)
                                    continue;
                            }
                            catch (Exception e)
                            {
                                LoggerPublisher.OnLogError(e.Message);
                            }
                        }
                        break;
                    }
                    case FilterMenuEnum.ByCategory:
                    {
                        Console.Clear();
                            UserAccessSide.Bosik();
                            cvs = ExceptionHandle.Handle(CvFilter.FilterByCategory, UserHelper.InputCategory(), cvs);
                        break;
                    }
                    case FilterMenuEnum.ByEducation:
                    {
                        Console.Clear();
                            UserAccessSide.Bosik();
                            cvs = ExceptionHandle.Handle(CvFilter.FilterByEducation, UserHelper.InputEducation(), cvs);
                        break;
                    }
                    case FilterMenuEnum.ByExperience:
                    {
                        Console.Clear();
                            UserAccessSide.Bosik();
                            cvs = ExceptionHandle.Handle(CvFilter.FilterByExperience, UserHelper.InputExperience(), cvs);
                        break;
                    }
                    case FilterMenuEnum.ByRegion:
                    {
                        Console.Clear();
                            UserAccessSide.Bosik();
                            cvs = ExceptionHandle.Handle(CvFilter.FilterByRegion, UserHelper.InputRegion(), cvs);
                        break;
                    }
                    case FilterMenuEnum.BySalary:
                    {
                        Console.Clear();
                            UserAccessSide.Bosik();
                            var input = UserHelper.InputSalary();
                        var salary = UserHelper.ParseSalary(input);
                        cvs = ExceptionHandle.Handle(CvFilter.FilterBySalary, salary, cvs);
                        break;
                    }
                    case FilterMenuEnum.Reset:
                    {
                        cvs = mainCvs;
                        break;
                    }
                    case FilterMenuEnum.Back:
                    {
                        seeCvsLoop = false;
                        break;
                    }
                }
            }
        }
    }
}