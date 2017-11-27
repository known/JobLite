using System.Configuration;

namespace JobLite.Service
{
    public class Config
    {
        public static string Server = ConfigurationManager.AppSettings["Server"];
        public static int TimerInterval = int.Parse(ConfigurationManager.AppSettings["TimerInterval"]);
        
        public static string SmtpServer = ConfigurationManager.AppSettings["SmtpServer"];
        public static string ServiceMailName = ConfigurationManager.AppSettings["ServiceMailName"];
        public static string ServiceMail = ConfigurationManager.AppSettings["ServiceMail"];
        public static string ServiceMailPassword = ConfigurationManager.AppSettings["ServiceMailPassword"];
        public static string ExceptionMailTo = ConfigurationManager.AppSettings["ExceptionMailTo"];
    }
}
