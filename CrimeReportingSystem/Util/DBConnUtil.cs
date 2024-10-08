using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CrimeReportingSystem.Util {
    internal static class DBConnUtil {
        private static IConfiguration _iconfiguration;

        static DBConnUtil()
        {
            GetAppSettingsFile();
        }

        private static void GetAppSettingsFile() {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // sets the base path where configuration file is located
                .AddJsonFile("appSetting.json"); //  configuration should include the configuration file named as appsetting.json
            _iconfiguration = builder.Build(); // build creates an Iconfiguration object which has data from appsetting file
        }

        public static string GetConnectionString() {
            return _iconfiguration.GetConnectionString("LocalConnectionString");
        }

    }
}


     
