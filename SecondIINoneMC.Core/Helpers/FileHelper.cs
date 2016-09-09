#region using directives
using System;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Collections;
using System.Web;
#endregion

namespace SecondIINoneMC.Core.Helpers
{
    public static class FileHelper
    {
        #region Static Public Methods

        /// <summary>
        /// Retrieves the size of a File.
        /// </summary>
        /// <author>Juba Jitu Jackson</author>
        /// <date>06/13/06</date>
        /// <param name="path">File Path.</param>
        /// <returns>long</returns>
        public static long GetFileSize(string path)
        {
            long fileSize = 0;

            try
            {
                FileInfo file = new FileInfo(path);

                fileSize = file.Length;
            }
            catch (Exception ex)
            {
                throw new Exception("Can not determine file size.", ex);
            }

            return fileSize;
        }

        /// <summary>
        /// Retrieves the size of a Directory.
        /// </summary>
        /// <author>Juba Jitu Jackson</author>
        /// <date>06/13/06</date>
        /// <param name="path">Directory Path</param>
        /// <returns>long</returns>
        public static long GetDirectorySize(string path)
        {
            long directorySize = 0;

            try
            {
                FileInfo[] files = (new DirectoryInfo(path)).GetFiles();
                foreach (FileInfo file in files)
                {
                    directorySize += file.Length;
                }

                DirectoryInfo[] folders = (new DirectoryInfo(path)).GetDirectories();
                foreach (DirectoryInfo folder in folders)
                {
                    directorySize += GetDirectorySize(folder.FullName);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Can not determine folder size.", ex);
            }

            return directorySize;
        }

        /// <summary>
        /// Reads a text file and converts the file to HTML.
        /// </summary>
        /// <author>Juba Jitu Jackson</author>
        /// <date>05/23/07</date>
        /// <param name="path">File Path of the text file to process.</param>
        /// <returns>string</returns>
        public static string ReadTextFile(string path)
        {
            StringBuilder fileContentStringBuilder = new StringBuilder();

            if (File.Exists(path))
            {
                StreamReader streamReader = new StreamReader(path);
                string fileString = streamReader.ReadToEnd();
                fileContentStringBuilder.Append(fileString);
                streamReader.Dispose();
            }

            return fileContentStringBuilder.ToString();
        }

        /// <summary>
        /// Reads a text file and converts the file to HTML.
        /// </summary>
        /// <author>Juba Jitu Jackson</author>
        /// <date>05/23/07</date>
        /// <param name="path">File Path of the text file to process.</param>
        /// <returns>string</returns>
        public static string ReadTextFileWithSpacesBL(string path)
        {
            StringBuilder fileContentStringBuilder = new StringBuilder();

            if (File.Exists(path))
            {
                StreamReader streamReader = new StreamReader(path);
                string fileString = streamReader.ReadToEnd();
                fileContentStringBuilder.Append(Regex.Replace(fileString, "\n", "<br />"));
                streamReader.Dispose();
            }

            return fileContentStringBuilder.ToString();
        }

        /// <summary>
        /// Deletes a File.
        /// </summary>
        /// <author>Juba Jitu Jackson</author>
        /// <date>05/23/07</date>
        /// <param name="file">File path of the file to be deleted.</param>
        /// <returns>void</returns>
        public static void DeleteFile(string file)
        {
            try
            {
                if (File.Exists(file))
                    File.Delete(file);
            }
            catch (Exception ex)
            {
                throw new Exception("Can not delete file.  Access is denied.", ex);
            }
        }

        /// <summary>
        /// Deletes a Directory and all the directories and files within it.
        /// </summary>
        /// <author>Juba Jitu Jackson</author>
        /// <date>06/13/06</date>
        /// <param name="directory">File path of the directory to be deleted.</param>
        /// <returns>void</returns>
        public static void DeleteDirectory(string directory)
        {
            try
            {
                if (Directory.Exists(directory))
                    Directory.Delete(directory);
            }
            catch (Exception ex)
            {
                throw new Exception("Can not delete directory.  Access is denied.", ex);
            }
        }

        /// <summary>
        /// Creates a Text File.
        /// </summary>
        /// <author>Juba Jitu Jackson</author>
        /// <date>05/23/07</date>
        /// <param name="path">The File Path the new text file will be saved to.</param>
        /// <returns>void</returns>
        public static void CreateTextFile(string path, string content)
        {
            try
            {
                StreamWriter streamWriter = new StreamWriter(path);
                streamWriter.WriteLine(content);
                streamWriter.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("File can not be created.  Access is denied.", ex);
            }
        }

        #endregion
    }
}
