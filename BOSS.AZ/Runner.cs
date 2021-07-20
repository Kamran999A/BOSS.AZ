using Bossaz.Helpers;
using Bossaz.Logger;
using Bossaz.NotificationSender;
using Bossaz.Sides.UserAccess;
namespace Bossaz
{
    class Runner
    {
        public static void Runnerr()
        {
            var db = new Database.Database();
            NotificationPublisher.EventHandler += MailNotification.Send;
            NotificationPublisher.EventHandler += ProgramNotification.Send;
            var consoleLogger = new ConsoleLogger();
            var fileLogger = new FileLogger();
            LoggerPublisher.ErrorHandler += consoleLogger.Error;
            LoggerPublisher.ErrorHandler += fileLogger.Error;
            LoggerPublisher.InfoHandler += consoleLogger.Info;
            LoggerPublisher.InfoHandler += fileLogger.Info;
            FileHelper.ReadFromJson(ref db);
            UserAccessSide.Start(db);
        }
    }
}