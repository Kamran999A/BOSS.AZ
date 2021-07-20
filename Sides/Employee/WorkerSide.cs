using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Windows.Forms;
using Bossaz.Abstracts;
using Bossaz.ConsoleInterface;
using Bossaz.Entities;
using Bossaz.Enums;

using Bossaz.Helpers;
using Bossaz.Logger;
using Bossaz.Sides.UserAccess;

namespace Bossaz.Sides.Employee
{
    public static class WorkerSide
    {
        public static void Start(Worker worker, Database.Database db)
        {
            var workerSideMainLoop = true;
            while (workerSideMainLoop)
            {
                Console.Title = $"Worker: {worker.Name}";
                if (Database.Database.Changes)
                {
                    FileHelper.WriteToJson(db);
                    Database.Database.Changes = false;
                }
                Console.Clear();
                UserAccessSide.Bosik();
                ConsoleScreen.PrintMenu(ConsoleScreen.WorkerSideMainMenu, ConsoleColor.DarkGreen);
                var mainChoice = (WorkerSideMainMenu) ConsoleScreen.Input(ConsoleScreen.WorkerSideMainMenu.Count);
                Console.Clear();
                switch (mainChoice)
                {
                    case WorkerSideMainMenu.SEEADS:
                    {
                            AdsSection.Start(worker, db);
                        break;
                    }
                    case WorkerSideMainMenu.YOURCV:
                    {
                            CvSection.Start(worker);
                        break;
                    }
                    case WorkerSideMainMenu.CvNotifications:
                    {
                        while (true)
                        {   label:
                            Console.Clear();
                                UserAccessSide.Bosik();
                                if (!ExceptionHandle.Handle(worker.ShowAllCvWithRequestCount))
                                break;
                            var cvId = UserHelper.InputGuid();
                            Cv cv = null;
                            try
                            {
                                cv = worker.GetCv(cvId);
                                if (cv.RequestFromEmployers.Count == 0)
                                {
                                    LoggerPublisher.OnLogError("There is no request!");
                                    ConsoleScreen.Clear();
                                        break;
                                }
                                var vacancies = db.GetAllVacanciesFromRequests(cv.RequestFromEmployers);
                                while (true)
                                {
                                    Console.Clear();
                                        UserAccessSide.Bosik();
                                        if (!ExceptionHandle.Handle(WorkerHelper.ShowVacancies, vacancies))
                                        break;
                                    var vacancyId = UserHelper.InputGuid();
                                    var vacancy = vacancies.SingleOrDefault(v => v.Guid == vacancyId);
                                    if (vacancy == null)
                                    {
                                        LoggerPublisher.OnLogError($"There is no vacancy associated this id -> {vacancyId}");
                                        ConsoleScreen.Clear();
                                        continue;
                                    }
                                    var employer =
                                        DatabaseHelper.GetUser(
                                            cv.RequestFromEmployers.SingleOrDefault(r => r.Value == vacancy.Guid).Key,
                                            db.Users) as Entities.Employer;
                                    Console.Clear();
                                        UserAccessSide.Bosik();
                                        Console.WriteLine(vacancy);
                                    ConsoleScreen.PrintMenu(ConsoleScreen.CvAdsChoices, ConsoleColor.DarkGreen);
                                    var choice =
                                        (CvAdsChoices)ConsoleScreen.Input(ConsoleScreen.CvAdsChoices.Count);
                                    if (choice == CvAdsChoices.Back)
                                       goto label;
                                    Database.Database.Changes = true;
                                    switch (choice)
                                    {
                                        case CvAdsChoices.Accept:
                                            {
                                                cv.RemoveRequest(employer.Guid);
                                                NotificationSender.NotificationPublisher.OnSend(employer, new Notification() { Title = "From CV owner\n", Message = $"Congratulations dear {vacancy.Ad.Contact}!\r\n Your request Accepted.",
                                                    Message2 = $"\r\n Cv info:" +
                                                    $"\r\n Name : {cv.Name},\r\n Surname : {cv.Surname},\r\n Catagory : {cv.Category}" });
                                                LoggerPublisher.OnLogInfo("Accepted.");
                                                break;
                                            }
                                        case CvAdsChoices.Decline:
                                            {
                                                cv.RemoveRequest(employer.Guid);
                                                NotificationSender.NotificationPublisher.OnSend(employer, new Notification() { Title = "From CV owner\n", Message = $"We are sorry dear {vacancy.Ad.Contact}!\r\n Your request declined.",Message2 = $"\r\n Cv info:\r\n Name : {cv.Name},\r\n Surname : {cv.Surname},\r\n Catagory : {cv.Category}" });
                                                LoggerPublisher.OnLogInfo("Declined.");
                                                break;
                                            }
                                    }
                                    Database.Database.Changes = true;
                                    if (ConsoleScreen.DisplayMessageBox("Info", "Do you want to see other Vacancies?",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                        break;
                                }
                            }
                            catch (Exception e)
                            {
                                LoggerPublisher.OnLogError(e.Message);
                            }

                            if (cv == null && ConsoleScreen.DisplayMessageBox("Info", "Do you want to try again?",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                break;
                        }
                        break;
                    }
                    case WorkerSideMainMenu.AllNotifications:
                    {
                            UserAccessSide.Bosik();
                            NotificationSide.AllNotificationsStart(worker);
                        break;
                    }
                    case WorkerSideMainMenu.UnreadNotifications:
                    {
                            UserAccessSide.Bosik();
                            NotificationSide.OnlyUnReadNotificationsStart(worker);
                        break;
                    }
                    case WorkerSideMainMenu.Logout:
                    {
                        workerSideMainLoop = false;
                        break;
                    }
                }
            }
        }
    }
}