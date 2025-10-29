using log4net;
using log4net.Config;
using System.Reflection;

namespace ReqnrollTestProjectSauseDemoApp.Logs
{
    public static class LogManager
    {

        private static readonly ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void InitializeLogging(string scenarioName)
        {
            try
            {
                // ✅ Fixed base directory for your solution
                string logDir = @"C:\Users\sumanta_swain\source\repos\ReqnrollTestProjectSauseDemoApp\Logs";

                if (!Directory.Exists(logDir))
                    Directory.CreateDirectory(logDir);


                string safeScenarioName = scenarioName.Replace(" ", "_");
                string timestamp = DateTime.Now.ToString("dd_MM_yyyy_HH_mm");
                string logFileName = Path.Combine(logDir, $"{safeScenarioName}_{timestamp}.log");


                log4net.GlobalContext.Properties["LogFileName"] = logFileName;


                string configPath = @"C:\Users\sumanta_swain\source\repos\ReqnrollTestProjectSauseDemoApp\Credentials\Log4Net.config";
                XmlConfigurator.Configure(new FileInfo(configPath));

                _log.Info($"🟢 Logging initialized for: {scenarioName}");
                _log.Info($"🗂️ Logs will be saved to: {logFileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error initializing logging: {ex.Message}");
            }
        }

        public static ILog GetLogger()
        {
            var callingType = MethodBase.GetCurrentMethod().DeclaringType;
            return log4net.LogManager.GetLogger(callingType);
        }

    }
}
