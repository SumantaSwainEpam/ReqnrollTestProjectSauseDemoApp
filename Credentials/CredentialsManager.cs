using Microsoft.Extensions.Configuration;
using log4net;
using System.Collections.Generic; // Add this for List<T>
using System.IO; // Add this for Directory and Path
using System; // Add this for Exception
using System.Linq; // Add this for Any()

namespace ReqnrollTestProjectSauseDemoApp.Credentials
{
    public class CredentialsManager
    {

        private static IConfiguration _configuration;

        static CredentialsManager()
        {
            try
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).
                    AddJsonFile(Path.Combine("Credentials", "AppConfig.json"), optional: false, reloadOnChange: true);
                _configuration = builder.Build();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public static string GetBaseUrl()
        {
            var BaseUrl = _configuration.GetSection("TestSettings:BaseUrl").Value;
            if (string.IsNullOrEmpty(BaseUrl))
            {
                throw new InvalidOperationException("BaseUrl is not Configured");
            }
            return BaseUrl;

        }

        public static string GetDefaultUsername()
        {
            return _configuration["TestSettings:Username"]
                ?? throw new InvalidOperationException("Username not found in AppConfig.json");
        }

        public static string GetDefaultPassword()
        {
            return _configuration["TestSettings:Password"]
                ?? throw new InvalidOperationException("Password not found in AppConfig.json");
        }

       public static string GetAppTitle()
       {
            return _configuration["TestSettings:AppTitle"]
                ?? throw new InvalidOperationException("AppTitle not found in AppConfig.json");
       }

    }
}
