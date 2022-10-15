using System.IO;
using UnityEngine;

namespace OmegaLeo.Toolbox.Runtime.Helpers
{
    /// <summary>
    /// Helper class for any directory related methods.
    /// </summary>
    public static class DirectoryHelper
    {
        /// <summary>
        /// Method to copy recursively all files and folders from one path to another.
        /// </summary>
        /// <param name="path">Origin path</param>
        /// <param name="copyToRoot">Target path</param>
        public static void RecursiveCopy(string path, string copyToRoot)
        {
            // Get all directories inside our origin
            var directories = Directory.GetDirectories(path);

            // Go through all directories that we obtained
            foreach (var directory in directories)
            {
                // Get the directory name and concatenate with the target path
                string directoryName = new DirectoryInfo(directory).Name;
                string copyToPath = Path.Join(copyToRoot, directoryName);

                // Create the target directory if it doesn't exist
                if (!Directory.Exists(copyToPath))
                {
                    Directory.CreateDirectory(copyToPath);
                }

                // Get all files in the current directory
                var files = Directory.GetFiles(directory);

                // Go through all files that we obtained and copy them to the target directory
                foreach (string file in files)
                {
                    string fileName = Path.GetFileName(file);

                    File.Copy(file, Path.Join(copyToPath, fileName), true);
                }

                // Call recursive copy for the current directory so we can copy any directories that might be inside of it
                RecursiveCopy(directory, copyToPath);
            }
        }
    }
}