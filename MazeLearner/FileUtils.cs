using Microsoft.VisualBasic.FileIO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MazeLearner
{
    public static class FileUtils
    {
        private static Regex FileNameRegex = new Regex("^(?<path>.*[\\\\\\/])?(?:$|(?<fileName>.+?)(?:(?<extension>\\.[^.]*$)|$))", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public static bool Exists(string path)
        {
            return File.Exists(path);
        }
        public static void Delete(string path, bool forceDeleteFile = false)
        {
            if (forceDeleteFile == true)
            {
                File.Delete(path);
            } 
            else
            {
                FileSystem.DeleteFile(path, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
            }
        }
        public static void Copy(string source, string destination, bool overwrite = true)
        {
            try
            {
                File.Copy(source, destination, overwrite);
                return;
            }
            catch (IOException ex)
            {
                if (ex.GetType() != typeof(IOException))
                    throw;

                using FileStream fileStream = File.OpenRead(source);
                using FileStream destination2 = File.Create(destination);
                fileStream.CopyTo(destination2);
                return;
            }
        }
        public static void Move(string source, string destination, bool overwrite = true, bool forceDeleteSourceFile = false)
        {
            Copy(source, destination, overwrite);
            Delete(source, forceDeleteSourceFile);
        }
        public static string GetFullPath(string path)
        {
            return path;
        }
        public static int GetFileSize(string path)
        {
            return (int)new FileInfo(path).Length;
        }
        public static void Read(string path, byte[] buffer, int length)
        {
            using FileStream fileStream = File.OpenRead(path);
            fileStream.Read(buffer, 0, length);
        }
        public static byte[] ReadAllBytes(string path)
        {
            return File.ReadAllBytes(path);
        }
        public static void WriteAllBytes(string path, byte[] data)
        {
            Write(path, data, data.Length);
        }

        public static void Write(string path, byte[] data, int length)
        {
            string parentFolderPath = GetParentFolderPath(path);
            if (parentFolderPath != "") TryCreatingDirectory(parentFolderPath);
            RemoveReadOnlyAttribute(path);
            using FileStream fileStream = File.Open(path, FileMode.Create);
            while (fileStream.Position < length)
            {
                fileStream.Write(data, (int)fileStream.Position, Math.Min(length - (int)fileStream.Position, 2048));
            }
        }
        public static void RemoveReadOnlyAttribute(string path)
        {
            if (!File.Exists(path))
                return;

            try
            {
                FileAttributes attributes = File.GetAttributes(path);
                if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    attributes &= ~FileAttributes.ReadOnly;
                    File.SetAttributes(path, attributes);
                }
            }
            catch (Exception)
            {
            }
        }
        public static string GetParentFolderPath(string path, bool includeExtension = true)
        {
            Match match = FileNameRegex.Match(path);
            if (match == null || match.Groups["path"] == null)
                return "";

            return match.Groups["path"].Value;
        }
        public static bool TryCreatingDirectory(string folderPath)
        {
            if (Directory.Exists(folderPath)) return true;
            try
            {
                Directory.CreateDirectory(folderPath);
                return true;
            }
            catch (Exception exception)
            {
                Loggers.Warn($"{exception}, {folderPath}");
                return false;
            }
        }
        public static void OpenFolder(string folderPath)
        {
            if (TryCreatingDirectory(folderPath))
            {
                Process.Start(new ProcessStartInfo(folderPath) { UseShellExecute = true });
            }
        }
        public static byte[] ToByteArray(this string str)
        {
            byte[] array = new byte[str.Length * 2];
            Buffer.BlockCopy(str.ToCharArray(), 0, array, 0, array.Length);
            return array;
        }
        public static string GetFileName(string path, bool includeExtension = true)
        {
            Match match = FileNameRegex.Match(path);
            if (match == null || match.Groups["fileName"] == null)
                return "";

            includeExtension &= match.Groups["extension"] != null;
            return match.Groups["fileName"].Value + (includeExtension ? match.Groups["extension"].Value : "");
        }
    }
}
