using AspDotNet.FTPHelper;
using FluentFTP;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ScribeSharp
{
    class sftpConnect

    {
        public void readFile(string localpath)
        {
            using (FtpClient ftp = CreateFtpClient())
            {
                ftp.Connect();
                ftp.DownloadFile(localpath, Path.GetFileName(localpath));
                ftp.Dispose();
                //string fs = File.ReadAllText(filepath);
                //return fs;
            }
        }
        public void readFile(string localpath, string joinID)
        {
            using (FtpClient ftp = CreateFtpClient())
            {
                ftp.Connect();
                ftp.CreateDirectory($"/{joinID}/");
                ftp.DownloadFile(localpath, $"/{joinID}/{Path.GetFileName(localpath)}");
                ftp.Dispose();
                //string fs = File.ReadAllText(filepath);
                //return fs;
            }
        }
        public void readFile(string localpath, string joinID, string actualPath)
        {
            using (FtpClient ftp = CreateFtpClient())
            {
                ftp.Connect();
                ftp.CreateDirectory($"/{joinID}/");
                ftp.DownloadFile(localpath, $"/{joinID}/{Path.GetFileName(actualPath)}");
                ftp.Dispose();
                //string fs = File.ReadAllText(filepath);
                //return fs;
            }
        }

        public void deleteFile(string filepath)
        {
            using (FtpClient ftp = CreateFtpClient())
            {
                ftp.DeleteFile(Path.GetFileName(filepath));
                ftp.Dispose();
            }
        }


        public void uploadFile(string filepath)
        {
            using (FtpClient ftp = CreateFtpClient())
            {
                if (!ftp.FileExists(Path.GetFileName(filepath)))
                {
                    using (FileStream fs = File.OpenRead(filepath))
                    {
                        ftp.Upload(fs, Path.GetFileName(filepath));
                        fs.Close();
                    }
                    ftp.Dispose();

                }
                else
                {
                    deleteFile(filepath);
                    uploadFile(filepath);

                }
            }
        }
        public void uploadPPT(string filepath, string classID)
        {
            using (FtpClient ftp = CreateFtpClient())
            {
                if (!ftp.FileExists(Path.GetFileName(filepath)))
                {
                    using (FileStream fs = File.OpenRead(filepath))
                    {
                        ftp.CreateDirectory($"/{classID}/");
                        ftp.Upload(fs, $"/{classID}/{Path.GetFileName("currentPowerpoint.pptx")}");
                        fs.Close();
                    }
                    ftp.Dispose();

                }
                else
                {
                    deleteFile(filepath);
                    uploadPPT(filepath, classID);

                }
            }
        }


        private FtpClient CreateFtpClient()
        {
            return new FtpClient("ftp://e129250-ftp.services.easyname.eu", new System.Net.NetworkCredential { UserName = "196693ftp2", Password = ".dd4erlqtmi4" });
        }
    }
}
