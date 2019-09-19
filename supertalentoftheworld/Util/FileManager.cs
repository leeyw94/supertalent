using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Web;
using SmartFactory.Common;

namespace Board.Util
{
    public class FileManager
    {
        public List<string> FileUpload()
        {
            HttpRequest httpRequest = HttpContext.Current.Request;
            HttpFileCollection uploadFiles = httpRequest.Files;
            var docfiles = new List<string>();
            if (httpRequest.Files.Count > 0)
            {
                for (int i = 0; i < uploadFiles.Count; i++)
                {
                    string time = DateTime.Now.ToString("yyyyMMddHHmmss");
                    HttpPostedFile postedFile = uploadFiles[i];
                    //파일 이름만 좀 수정해주면 될듯
                    string filePath = ConfigurationManager.AppSettings["SaveFilePath"];
                    var dInfo = new DirectoryInfo(filePath);
                    //경로에 폴더가 없으면 만들어준다.
                    if (!dInfo.Exists)
                    {
                        dInfo.Create();
                    }

                    string uploadfilename = Path.GetFileName(postedFile.FileName);
                    if (string.IsNullOrEmpty(uploadfilename))
                    {
                        continue;
                    }

                    string SavePath = filePath + time + uploadfilename;
                    postedFile.SaveAs(SavePath);

                    string filename = time + uploadfilename;
                    docfiles.Add(filename);
                }
            }
            return docfiles;
        }

        public List<string> PhotoFileUpload(string web_department_id)
        {
            int img_width = 300;
            int img_height = 226;
            int file_max = 1000*200; //200k 이상
            string refileName = "";

            HttpRequest httpRequest = HttpContext.Current.Request;
            HttpFileCollection uploadFiles = httpRequest.Files;
            var docfiles = new List<string>();
            if (httpRequest.Files.Count > 0)
            {
                for (int i = 0; i < uploadFiles.Count; i++)
                {
                    string time = DateTime.Now.ToString("yyyyMMddHHmmss");
                    HttpPostedFile postedFile = uploadFiles[i];
                    //파일 이름만 좀 수정해주면 될듯
                    string filePath = ConfigurationManager.AppSettings["SaveFilePath"] + "/" + web_department_id + "/";
                    var dInfo = new DirectoryInfo(filePath);
                    //경로에 폴더가 없으면 만들어준다.
                    if (!dInfo.Exists)
                    {
                        dInfo.Create();
                    }

                    string uploadfilename = Path.GetFileName(postedFile.FileName);
                    if (string.IsNullOrEmpty(uploadfilename))
                    {
                        continue;
                    }

                    int fileSize_temp = uploadFiles[i].ContentLength; //파일사이즈
                    uploadfilename = time + uploadfilename.Replace("%20", "");

                    refileName = "s_" + uploadfilename;


                    string SavePath = filePath + uploadfilename;
                    postedFile.SaveAs(SavePath);

                    #region 고정/비율 

                    if (fileSize_temp >= file_max)
                    {
                        FileHelper.SaveResizedImage(filePath, uploadfilename, refileName, img_width, img_height);
                    }

                    else
                    {
                        FileHelper.SaveResizedImage(filePath, uploadfilename, refileName, 100);
                    }

                    #endregion

                    string filename = uploadfilename;
                    docfiles.Add(filename);
                }
            }
            return docfiles;
        }


        public List<string> M_FileUpload()
        {
            HttpRequest httpRequest = HttpContext.Current.Request;
            HttpFileCollection uploadFiles = httpRequest.Files;


            var docfiles = new List<string>();
            if (httpRequest.Files.Count > 0)
            {
                for (int i = 0; i < uploadFiles.Count; i++)
                {
                    try
                    {
                        string time = DateTime.Now.ToString("yyyyMMddHHmmss");
                        HttpPostedFile postedFile = uploadFiles[i];
                        //파일 이름만 좀 수정해주면 될듯
                        string filePath = ConfigurationManager.AppSettings["SaveFilePath"];
                        var dInfo = new DirectoryInfo(filePath);
                        //경로에 폴더가 없으면 만들어준다.
                        if (!dInfo.Exists)
                        {
                            dInfo.Create();
                        }

                        string uploadfilename = Path.GetFileName(postedFile.FileName);
                        if (string.IsNullOrEmpty(uploadfilename))
                        {
                            continue;
                        }

                        string SavePath = filePath + time + uploadfilename;
                        postedFile.SaveAs(SavePath);

                        string filename = time + uploadfilename;
                        docfiles.Add(filename);
                    }
                    catch
                    {
                    }
                }
            }
            return docfiles;
        }


        //안씀
        public void FileDownload(string remoteAddress, string fileName)
        {
            var wc = new WebClient();

            if (!Directory.Exists("C:\\downloadedFiles"))
                Directory.CreateDirectory("C:\\downloadedFiles");


            wc.DownloadFileAsync(new Uri(remoteAddress), "C:/downloadedFiles/" + fileName);
        }
    }
}