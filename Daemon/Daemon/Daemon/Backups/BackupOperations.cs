using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using System.Threading;
using System.Net;

namespace Daemon
{
    public class BackupOperations
    {
        public static bool ZipFiles(List<string> destinationPaths)
        {
            try
            {
                foreach (string destinationPath in destinationPaths)
                {
                    FileStream fsOut = File.Create(destinationPath);
                    ZipOutputStream zipStream = new ZipOutputStream(fsOut);
                    zipStream.SetLevel(3);
                    zipStream.Password = null;

                    int folderOffset = destinationPath.Length + (destinationPath.EndsWith("\\") ? 0 : 1);
                    BackupOperations.CompressFolder(destinationPath, zipStream, folderOffset);


                    zipStream.IsStreamOwner = true;
                    zipStream.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void CompressFolder(string path, ZipOutputStream zipStream, int folderOffset)
        {
            string[] files = Directory.GetFiles(path);

            foreach (string file in files)
            {
                FileInfo fi = new FileInfo(file);

                string entryName = file.Substring(folderOffset);
                entryName = ZipEntry.CleanName(entryName);
                ZipEntry newEntry = new ZipEntry(entryName);
                newEntry.DateTime = fi.LastWriteTime;
                newEntry.Size = fi.Length;

                zipStream.PutNextEntry(newEntry);
                byte[] buffer = new byte[4096];
                using (FileStream streamReader = File.OpenRead(file))
                {
                    StreamUtils.Copy(streamReader, zipStream, buffer);
                }

                zipStream.CloseEntry();                
            }

            string[] folders = Directory.GetDirectories(path);
            foreach (string item in folders)
            {
                CompressFolder(item, zipStream, folderOffset);
            }
        }

        public static void SendFtp(string source)
        {
            string[] files = Directory.GetFiles(source, "*.*");
            string[] subDirs = Directory.GetDirectories(source);

            foreach (FtpSettings item in DaemonSettings.ftpSettings)
            {
                foreach (string file in files)
                {

                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(item.ServerAdress));
                    request.Method = WebRequestMethods.Ftp.UploadFileWithUniqueName;

                    request.Credentials = new NetworkCredential(item.Username, item.Password);

                    byte[] fileContents;
                    using (StreamReader sourceStream = new StreamReader(Path.GetFileName(file)))
                    {
                        fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                    }

                    request.ContentLength = fileContents.Length;

                    using (Stream requestStream = request.GetRequestStream())
                    {
                        requestStream.Write(fileContents, 0, fileContents.Length);
                    }

                    using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                    {
                        Console.WriteLine("Upload File Complete, status {0}", response.StatusDescription);
                    }
                }

                foreach (string subDir in subDirs)
                {
                    using (WebClient client = new WebClient())
                    {
                        //client.cre
                    }
                }
            }

        }
    }
}
