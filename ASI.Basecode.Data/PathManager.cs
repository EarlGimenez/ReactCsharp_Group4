using System.IO;

namespace ASI.Basecode.Data
{
    /// 
    /// Path Manager
    /// 
    public class PathManager
    {
        /// 
        /// Gets or sets the setup root directory.
        /// 
        public static string SetupRootDirectory { get; set; }

        /// 
        /// Setups the specified setup root directory.
        /// 
        /// <param name="setupRootDirectory">The setup root directory.</param>
        public static void Setup(string setupRootDirectory)
        {
            SetupRootDirectory = setupRootDirectory;
        }

        /// 
        /// Directory Path
        /// 
        public static class DirectoryPath
        {
            /// 
            /// Log file storage directory path
            /// 
            public static string LogDirectory
            {
                get { return GetFolderPath(SetupRootDirectory, "logs"); }
            }

            /// 
            /// application log directory path
            /// 
            /// <param name="appName">application name</param>
            /// <returns>directory path</returns>
            public static string ApplicationLogsDirectory(string appName)
            {
                return GetFolderPath(Path.Combine(LogDirectory, appName));
            }
        }

        /// 
        /// File Path
        /// 
        public static class FilePath
        {
        }

        /// 
        /// Gets the folder path and create the directory
        /// 
        /// <param name="path">Path</param>
        /// <param name="folderName">Name of the folder.</param>
        /// <returns>Directory path</returns>
        private static string GetFolderPath(string path, string folderName = "")
        {
            string result = Path.Combine(path, folderName);
            if (!Directory.Exists(result))
            {
                Directory.CreateDirectory(result);
            }

            return result;
        }
    }
}
