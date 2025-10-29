using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace ReqnrollTestProjectSauseDemoApp.Reports
{
    public static class ReportManager
    {
        private static ExtentReports _extentReports;
        private static ExtentSparkReporter _sparkReporter;

        /// <summary>
        /// Creates a new Extent Report for each scenario/test case with timestamped name.
        /// </summary>
        public static ExtentReports GetReports(string scenarioName)
        {
            // Base directory for reports
            var reportDir = @"C:\Users\sumanta_swain\source\repos\ReqnrollTestProjectSauseDemoApp\Reports\";

            if (!Directory.Exists(reportDir))
                Directory.CreateDirectory(reportDir);

            // Use scenario name and timestamp for unique report file
            var timestamp = DateTime.Now.ToString("dd_MM_yyyy_HH_mm");
            string safeScenarioName = string.IsNullOrEmpty(scenarioName)
                ? "TestReport"
                : string.Concat(scenarioName.Split(Path.GetInvalidFileNameChars()));

            var reportFileName = $"{safeScenarioName}_{timestamp}.html";
            var reportPath = Path.Combine(reportDir, reportFileName);

            // Initialize new reporter each time
            _sparkReporter = new ExtentSparkReporter(reportPath);
            _extentReports = new ExtentReports();
            _extentReports.AttachReporter(_sparkReporter);

            // Optional: Add system/environment info
            _extentReports.AddSystemInfo("Machine", Environment.MachineName);
            _extentReports.AddSystemInfo("OS", Environment.OSVersion.ToString());
            _extentReports.AddSystemInfo("User", Environment.UserName);
            _extentReports.AddSystemInfo("Run Date", DateTime.Now.ToString("dd-MM-yyyy HH:mm"));

            return _extentReports;
        }

        public static void FlushReports()
        {
            _extentReports?.Flush();
        }
    }
}
