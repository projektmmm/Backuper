using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Net;
using Renci.SshNet;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Daemon
{
    public class BackupOperations
    {
        public static ReportMaker Batches(ReportMaker errors, string time = "BEFORE")
        {
            string tempPath = Path.GetTempPath();
            
            foreach (BatchesSettings batch in DaemonSettings.batchesSettings)
            {
                if (batch.Time != time)
                    continue;

                if (batch.Type == "SAVED")
                {
                    try
                    {
                        Process.Start(batch.LocalPath);
                    }
                    catch
                    {
                        errors.AddError(new ErrorDetails()
                        {
                            Problem = "Program didn't find saved batch. It was unable to run it.",
                            ProblemPath = batch.LocalPath
                        });
                    }
                }
                else if (batch.Type == "COMMAND")
                {
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(tempPath + "\\savedProcedure.bat", false))
                        {
                            sw.Write(batch.CommandText);
                            sw.Flush();
                        }

                        Process.Start(tempPath + "\\savedProcedure.bat");
                    }
                    catch
                    {
                        errors.AddError(new ErrorDetails()
                        {
                            Problem = "Program was unable to run typed query." + batch.Id.ToString()
                        });
                    }
                }
            }

            return errors;
        }

        public static bool ZipFiles(List<string> destinationPaths, PlannedBackups backup)
        {
            bool toRet = true;
            string filename = "";
            string tempPath = Path.GetTempPath();

            foreach (string item in destinationPaths)
            {
                try
                {
                    File.Delete($"{tempPath}//backup.zip");
                    ZipFile.CreateFromDirectory(item, $"{tempPath}//backup.zip", CompressionLevel.Optimal, false);
                }
                catch { toRet = false; continue; }

                if (backup.Override)
                {
                    try
                    {
                        Directory.Delete(item, true);
                        Directory.CreateDirectory(item);
                        File.Copy($"{tempPath}//backup.zip", $"{item}//backup.zip", true);
                    }
                    catch { toRet = false; continue; }
                }
                else
                {
                    try
                    {
                        int cnt = 0;
                        foreach (string file in Directory.GetFiles(item, "*", SearchOption.TopDirectoryOnly))
                        {
                            if (file.Contains("backup"))
                                cnt++;
                        }
                        File.Copy($"{tempPath}//backup.zip", $"{item}//backup_{cnt}.zip", true);
                    }
                    catch { toRet = false; continue; }
                }
            }      
            return toRet;
        }

        public static bool Ftp(List<string> destinationPaths, PlannedBackups backup)
        {
            bool ret = true;
            ZipFiles(destinationPaths, backup);
            string path = Path.GetTempPath() + "//backup.zip";

            foreach (FtpSettings item in DaemonSettings.ftpSettings)
            {
                if (ret == true)
                    ret = SendFtp(item, path);
                else
                    SendFtp(item, path);
            }
            return ret;
        }

        private static bool SendFtp(FtpSettings ftp, string source, bool ret = true)
        {
            try
            {                    
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(ftp.ServerAdress));
                    request.Method = WebRequestMethods.Ftp.UploadFileWithUniqueName;

                    request.Credentials = new NetworkCredential(ftp.Username, ftp.Password);

                    byte[] fileContents;
                    using (StreamReader sourceStream = new StreamReader(new FileInfo(source).FullName))
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
            try
            {
                foreach (SshSettings ssh in DaemonSettings.sshSettings)
                {
                    if (toRet)
                        toRet = SendSsh(ssh, source, source);
                    else
                        SendSsh(ssh, source, source);
                }
            }
            catch
            {
                return false;
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