using ASI.Basecode.WebApp.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ASI.Basecode.WebApp.Extensions.Configuration
{
    /// 
    /// Configuration Extension
    /// 
    public static class ConfigurationExtensions
    {
        /// 
        /// Gets the setup root directory path.
        /// 
        /// <param name="configuration">Configuration</param>
        /// <returns>Set up root path</returns>
        public static string GetSetupRootDirectoryPath(this IConfiguration configuration)
        {
            return configuration.GetSection("Common")
                                .GetValue<string>("SetupRoot");
        }

        /// 
        /// Gets the CSV output folder path.
        /// 
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static string GetCsvOutputFolderPath(this IConfiguration configuration)
        {
            return configuration.GetSection("Common")
                                .GetValue<string>("CsvOutputFolderPath");
        }

        /// 
        /// Gets the CSV import backup path.
        /// 
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static string GetCsvImportBackupPath(this IConfiguration configuration)
        {
            return configuration.GetSection("Common")
                               .GetValue<string>("CsvImportBackupPath");
        }

        /// 
        /// Gets the logging section.
        /// 
        /// <param name="configuration">Configuration</param>
        /// <returns>Logging settings</returns>
        public static IConfigurationSection GetLoggingSection(this IConfiguration configuration)
        {
            return configuration.GetSection("Logging");
        }

        /// 
        /// Gets the logging log level.
        /// 
        /// <param name="configuration">Configuration</param>
        /// <param name="name">Name</param>
        /// <returns>Logging level</returns>
        public static LogLevel GetLoggingLogLevel(this IConfiguration configuration, string name = "Default")
        {
            return configuration.GetSection("Logging")
                                .GetValue<LogLevel>(string.Format("LogLevel:{0}", name));
        }

        /// 
        /// Gets the size limit of each log file.
        /// 
        /// <param name="configuration">Configuration.</param>
        /// <returns>Log size limit</returns>
        public static string GetLogFileSize(this IConfiguration configuration)
        {
            return configuration.GetSection("Common")
                                .GetValue<string>("LogFileSize");
        }

        /// 
        /// Gets the token authentication
        /// 
        /// <param name="configuration">Configuration</param>
        /// <returns>Token authentication values</returns>
        public static TokenAuthentication GetTokenAuthentication(this IConfiguration configuration)
        {
            return new TokenAuthentication()
            {
                SecretKey = configuration.GetSection("TokenAuthentication:SecretKey").Value,
                Audience = configuration.GetSection("TokenAuthentication:Audience").Value,
                TokenPath = configuration.GetSection("TokenAuthentication:TokenPath").Value,
                CookieName = configuration.GetSection("TokenAuthentication:CookieName").Value,
                ExpirationMinutes = int.Parse(configuration.GetSection("TokenAuthentication:ExpirationMinutes").Value)
            };
        }
    }
}
