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
using Renci.SshNet;

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

        public static bool Ftp(string source)
        {
            string[] subDirs = Directory.GetDirectories(source);
            bool ret = true;

            foreach (FtpSettings item in DaemonSettings.ftpSettings)
            {
                if (ret == true)
                    ret = SendFtp(item, source, source);
                else
                    SendFtp(item, source, source);
            }
            return ret;
        }

        private static bool SendFtp(FtpSettings ftp, string source, string destinationPath, bool ret = true)
        {
            try
            {
                foreach (string subDir in Directory.GetDirectories(source))
                {
                    string[] files = Directory.GetFiles(source, "*.*");
                    string dirSuffix = $"//{new DirectoryInfo(subDir).FullName.Replace(destinationPath, "")}";
                    WebRequest folderRequest = WebRequest.Create(ftp.ServerAdress + dirSuffix);
                    folderRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
                    folderRequest.Credentials = new NetworkCredential(ftp.Username, ftp.Password);
                    using (var resp = (FtpWebResponse)folderRequest.GetResponse())
                    {
                        Console.WriteLine(resp.StatusCode);
                    }

                    foreach (string file in files)
                    {
                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(ftp.ServerAdress + $"//{dirSuffix}"));
                        request.Method = WebRequestMethods.Ftp.UploadFileWithUniqueName;

                        request.Credentials = new NetworkCredential(ftp.Username, ftp.Password);

                        byte[] fileContents;
                        using (StreamReader sourceStream = new StreamReader(new FileInfo(file).FullName))
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

                    if (ret)
                        ret = SendFtp(ftp, subDir, destinationPath);
                    else
                        SendFtp(ftp, subDir, destinationPath);
                }
                return true;
            }
            catch
            {
                ret = false;
                return ret;
            }
        }

        public static bool Ssh(string source)
        {
            bool toRet = true;
            foreach (SshSettings ssh in DaemonSettings.sshSettings)
            {
                if (toRet)
                    toRet = SendSsh(ssh, source, source);
                else
                    SendSsh(ssh, source, source);
            }

            return toRet;
        }

        private static bool SendSsh(SshSettings ssh, string sourceFolder, string destinationPath, bool toRet = false)
        {
            try
            {
                foreach (DirectoryInfo dir in new DirectoryInfo(sourceFolder).GetDirectories())
                {
                    foreach (FileInfo file in dir.GetFiles())
                    {
                        ConnectionInfo connInfo = new ConnectionInfo(ssh.HostOrIp, ssh.Port, ssh.Username,
                            new AuthenticationMethod[]
                            {
                            new PasswordAuthenticationMethod(ssh.Username, ssh.Password)
                            }
                            );

                        using (var sftp = new SftpClient(connInfo))
                        {
                            string uploadfn = file.FullName;

                            sftp.Connect();
                            sftp.ChangeDirectory(file.FullName.Replace(destinationPath, ""));
                            using (var uplfileStream = System.IO.File.OpenRead(uploadfn))
                            {
                                sftp.UploadFile(uplfileStream, uploadfn, true);
                            }
                            sftp.Disconnect();
                        }

                    }

                    if (toRet)
                        toRet = SendSsh(ssh, sourceFolder, destinationPath);
                    else
                        SendSsh(ssh, sourceFolder, destinationPath);
                }
                return true;
            }
            catch
            {
                toRet = false;
                return false;
            }

        }
    }
}
