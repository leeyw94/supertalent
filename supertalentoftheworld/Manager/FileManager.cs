using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web;

namespace supertalentoftheworld.Manager
{
    public static class FileManager
    {
        public static string ROOT_PATH = @"D:\Webfile\Supertalent\product\";
        public static string ROOT_PATH2 = @"D:\Webfile\Supertalent\model\";
        public static string URL_PATH = "http://supertalent.theblueeye.com/upload/";
        public static string URL_PATH2 = "http://supertalent.theblueeye.com/upload2/";


        public static List<string> FileUpload()
        {
            var httpRequest = System.Web.HttpContext.Current.Request;
            HttpFileCollection uploadFiles = httpRequest.Files;

            var docfiles = new List<string>();

            if (httpRequest.Files.Count > 0)
            {
                for (int i = 0; i < uploadFiles.Count; i++)
                {
                    string time = DateTime.Now.ToString("yyMMddHHmmss");
                    HttpPostedFile postedFile = uploadFiles[i];

                    if (string.IsNullOrEmpty(postedFile.FileName))
                    {
                        continue;
                    }

             
                    var filePath =  ROOT_PATH + time + Path.GetFileName(postedFile.FileName);
                    postedFile.SaveAs(filePath);

                    if (postedFile.ContentType.Contains("image"))
                    {
                        string thumbnail_path = ROOT_PATH +"t_" + time + postedFile.FileName;
                        Size size = new Size();
                        size.Width = 400;
                        size.Height = 300;

                        MakeThumbnail(filePath, thumbnail_path, size);
                    }


                    string filename = time + Path.GetFileName(postedFile.FileName);
                    docfiles.Add(filename);

                }
            }

            return docfiles;
        }

        public static List<string> FileUpload2()
        {
            var httpRequest = System.Web.HttpContext.Current.Request;
            HttpFileCollection uploadFiles = httpRequest.Files;

            var docfiles = new List<string>();

            if (httpRequest.Files.Count > 0)
            {
                for (int i = 0; i < uploadFiles.Count; i++)
                {
                    string time = DateTime.Now.ToString("yyMMddHHmmss");
                    HttpPostedFile postedFile = uploadFiles[i];

                    if (string.IsNullOrEmpty(postedFile.FileName))
                    {
                        continue;
                    }


                    var filePath = ROOT_PATH2 + time + Path.GetFileName(postedFile.FileName);
                    postedFile.SaveAs(filePath);

                    if (postedFile.ContentType.Contains("image"))
                    {
                        string thumbnail_path = ROOT_PATH2+ "t_"+time + postedFile.FileName;
                        Size size = new Size();
                        size.Width = 400;
                        size.Height = 300;

                        MakeThumbnail(filePath, thumbnail_path, size);
                    }


                    string filename = time + Path.GetFileName(postedFile.FileName);
                    docfiles.Add(filename);

                }
            }

            return docfiles;
        }


        /// <summary>
        /// 이미지를 썸네일 형태로 만들어서.. 용량을 줄이고 크기를 맞춘다.
        /// Size 크기에 맞게 썸네일 형태로 만든다.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="thumbnail_path"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string MakeThumbnail(string path,string thumbnail_path, Size size)
        {
            Image originalImage = Image.FromFile(path);

            double ratioX = size.Width / (double)originalImage.Width;
            double ratioY = size.Height / (double)originalImage.Height;

            double ratio = Math.Max(ratioX, ratioY);

            int newWidth = (int)(originalImage.Width * ratio);
            int newHeight = (int)(originalImage.Height * ratio);

            Bitmap newImage = new Bitmap(size.Width, size.Height);
            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.FillRectangle(Brushes.Transparent, 0, 0, newImage.Width, newImage.Height);
                g.DrawImage(originalImage, (size.Width - newWidth) / 2, (size.Height - newHeight) / 2, newWidth, newHeight);
            }

            newImage.Save(thumbnail_path);

            originalImage.Dispose();
            newImage.Dispose();

            return thumbnail_path;
        }


        public static byte[] ReadFile(string filename)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(ROOT_PATH + filename);
            return fileBytes;
        }
    }
}