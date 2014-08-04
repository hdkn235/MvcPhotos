using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPhotos.DAL;
using MvcPhotos.Models;
using MvcPhotos.ViewModels;

namespace MvcPhotos.Controllers
{
    public class PhotoUploadController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        //
        // GET: /Image/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadPhoto(Photo model)
        {
            if (ModelState.IsValid)
            {
                string name = SavePhoto();

                if (ModelState.IsValid)
                {
                    model.CreateTime = DateTime.Now;
                    model.PhotoPath = name;
                    unitOfWork.PhotoRepository.Insert(model);
                    unitOfWork.Save();

                    return RedirectToAction("Index", "PhotoManager");
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult BatchUpload()
        {
            bool isSavedSuccessfully = true;
            int count = 0;
            string msg = "";

            string fileName = "";
            string fileExtension = "";
            string filePath = "";
            string fileNewName = "";

            int albumId = string.IsNullOrEmpty(Request.Params["hidAlbumId"]) ?
                0 : int.Parse(Request.Params["hidAlbumId"]);

            try
            {
                string directoryPath = Server.MapPath("~/Content/photos");
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);

                foreach (string f in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[f];

                    if (file != null && file.ContentLength > 0)
                    {
                        fileName = file.FileName;
                        fileExtension = Path.GetExtension(fileName);
                        fileNewName = Guid.NewGuid().ToString() + fileExtension;
                        filePath = Path.Combine(directoryPath, fileNewName);
                        file.SaveAs(filePath);

                        Photo model = new Photo();
                        model.Title =Path.GetFileNameWithoutExtension(fileName);
                        model.PhotoPath = fileNewName;
                        model.CreateTime = DateTime.Now;
                        model.AlbumId = albumId;
                        unitOfWork.PhotoRepository.Insert(model);

                        count++;
                    }
                }

                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                isSavedSuccessfully = false;
            }


            return Json(new
            {
                Result = isSavedSuccessfully,
                Count = count,
                Message = msg
            });
        }

        #region private method

        /// <summary>
        /// 保存照片到服务器
        /// </summary>
        /// <returns></returns>
        private string SavePhoto()
        {
            string name = "";

            try
            {
                if (Request.Files.Count == 1)
                {
                    HttpPostedFileBase file = Request.Files[0];

                    Bitmap original = Bitmap.FromStream(file.InputStream) as Bitmap;
                    if (original != null)
                    {
                        name = Guid.NewGuid().ToString() + ".png";
                        var fn = Server.MapPath("~/Content/photos/" + name);
                        original.Save(fn, System.Drawing.Imaging.ImageFormat.Png);
                    }
                    else
                    {
                        ModelState.AddModelError("File",
                            "Your upload did not seem valid. Please try again using only correct images!");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("File", ex.Message);
            }

            return name;
        }

        #endregion
    }
}
