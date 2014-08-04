using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcPhotos.Helpers
{
    public class UploadHelper
    {
        /// <summary>
        /// 保存单张照片到服务器
        /// </summary>
        /// <returns></returns>
        public static string SavePhotoToServer(HttpPostedFileBase file)
        {
            string filePath = "";
            string fileNewName = "";

            string directoryPath = HttpContext.Current.Server.MapPath("~/Content/photos");
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            if (file != null && file.ContentLength > 0)
            {
                fileNewName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                filePath = Path.Combine(directoryPath, fileNewName);
                file.SaveAs(filePath);
            }

            return fileNewName;
        }

        /// <summary>
        /// 替换新的照片
        /// </summary>
        /// <param name="oldName"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string UpdatePhotoToServer(string oldName, HttpPostedFileBase file)
        {
            if (!string.IsNullOrEmpty(oldName))
            {
                string directoryPath = HttpContext.Current.Server.MapPath("~/Content/photos");
                string oldPath = Path.Combine(directoryPath, oldName);
                if (File.Exists(oldPath))
                {
                    File.Delete(oldPath);
                }                
            }

            return SavePhotoToServer(file);
        }
    }
}