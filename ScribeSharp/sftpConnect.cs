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
        public string readFile(string filepath)
        {
            using (FtpClient ftp = CreateFtpClient())
            {
                ftp.DownloadFile(filepath, Path.GetFileName(filepath));
                string fs = File.ReadAllText(filepath);
                return fs;
            }
        }

        public void deleteFile(string filepath)
        {
            using (FtpClient ftp = CreateFtpClient())
            {
                ftp.DeleteFile(Path.GetFileName(filepath));

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
                    }
                }
                else
                {
                    deleteFile(filepath);
                    uploadFile(filepath);
                }
            }
        }

        
        private FtpClient CreateFtpClient()
        {
            return new FtpClient("ftp://e129250-ftp.services.easyname.eu", new System.Net.NetworkCredential { UserName = "196693ftp2", Password = ".dd4erlqtmi4" });
        }
    }
}
