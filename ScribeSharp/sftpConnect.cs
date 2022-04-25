using AspDotNet.FTPHelper;
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
        private System.Drawing.Image upload;
        public sftpConnect(System.Drawing.Image image)
        {
            upload = image;
            backgroundWorker_DoWorkAsync();
        }
        public sftpConnect(String messages)
        {
            //Pass in strings to upload to database
        }

        //Define a struct to store login
        struct FtpSetting
        {
            public string Server { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string FileName { get; set; }
            public string FullName { get; set; }
        }

        FtpSetting _inputParameter;

        private async Task backgroundWorker_DoWorkAsync()
        {
            Console.WriteLine("test Console");
            //string fileName = ((FtpSetting)e.Argument).FileName;
            //string fullName = ((FtpSetting)e.Argument).FullName;
            string userName = "ScribeSharp@thelastprodigy.site";
            string password = "CSCI4600";
            string server = "ftp://thelastprodigy.site";
            FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(server);
            ftpRequest.Credentials = new NetworkCredential(userName, password);
            ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
            await using FileStream fileStream = File.Open(@$"\\Mac\Home\Desktop\Spring2022\Hasan4600\Scribe.png", FileMode.Open, FileAccess.Read);
            await using Stream requestStream = ftpRequest.GetRequestStream();
            await fileStream.CopyToAsync(requestStream);
            using FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();

            /*FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
            FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
            StreamReader streamReader = new StreamReader(response.GetResponseStream());

            List<string> directories = new List<string>();

            string line = streamReader.ReadLine();
            while (!string.IsNullOrEmpty(line))
            {
                var lineArr = line.Split('/');
                line = lineArr[lineArr.Count() - 1];
                directories.Add(line);
                line = streamReader.ReadLine();
            }

            streamReader.Close();
            foreach (var author in directories)
            {
                Console.WriteLine(author);
            }*/

        }

   
    }
}
