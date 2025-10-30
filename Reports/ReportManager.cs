using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace ReqnrollTestProjectSauseDemoApp.Reports
{
    public static class ReportManager
    {
        private static readonly object _lock = new(); // thread safety for parallel runs
        private static ExtentReports _extentReports;
        private static ExtentSparkReporter _sparkReporter;

        // Base directory for all reports (you can keep this as you like)
        private static readonly string _reportDir =
            @"C:\Users\sumanta_swain\source\repos\ReqnrollTestProjectSauseDemoApp\Reports\";

        private static string _reportPath;

        /// <summary>
        /// Creates a single Extent Report per run (not per scenario).
        /// Each test case/scenario will be added to the same report,
        /// even in parallel execution.
        /// </summary>
        public static ExtentReports GetReports()
        {
            if (_extentReports == null)
            {
                lock (_lock)
                {
                    if (_extentReports == null)
                    {
                        if (!Directory.Exists(_reportDir))
                            Directory.CreateDirectory(_reportDir);

                        // Report name with timestamp (same file for entire run)
                        var timestamp = DateTime.Now.ToString("dd_MM_yyyy_HH_mm");
                        var reportFileName = $"ExtentReport_{timestamp}.html";
                        _reportPath = Path.Combine(_reportDir, reportFileName);

                        _sparkReporter = new ExtentSparkReporter(_reportPath);
                        _extentReports = new ExtentReports();
                        _extentReports.AttachReporter(_sparkReporter);

                        // Add environment/system info
                        _extentReports.AddSystemInfo("Machine", Environment.MachineName);
                        _extentReports.AddSystemInfo("OS", Environment.OSVersion.ToString());
                        _extentReports.AddSystemInfo("User", Environment.UserName);
                        _extentReports.AddSystemInfo("Run Date", DateTime.Now.ToString("dd-MM-yyyy HH:mm"));
                    }
                }
            }

            return _extentReports;
        }

        /// <summary>
        /// Adds a test node dynamically for a scenario.
        /// </summary>
        public static ExtentTest CreateTest(string scenarioName)
        {
            var extent = GetReports();
            lock (_lock)
            {
                return extent.CreateTest(scenarioName);
            }
        }

        public static void FlushReports()
        {
            lock (_lock)
            {
                _extentReports?.Flush();
            }
        }

        /// <summary>
        /// Optional: Returns the final HTML report path for reference/logging.
        /// </summary>
        public static string GetReportPath() => _reportPath;
    }
}
