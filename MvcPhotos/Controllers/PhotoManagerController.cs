using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPhotos.DAL;
using MvcPhotos.Models;

namespace MvcPhotos.Controllers
{
    public class PhotoManagerController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        //
        // GET: /PhotoManager/

        public ActionResult Index(string searchString)
        {
            var photos = unitOfWork.PhotoRepository.Get(
                orderBy: q => q.OrderByDescending(p => p.CreateTime),
                includeProperties: p => p.Album
                );

            if (!string.IsNullOrEmpty(searchString))
            {
                photos = photos.Where(p => p.Title
                    .ToUpper()
                    .Contains(searchString.ToUpper()));
            }
            return View(photos);
        }

        //
        // GET: /PhotoManager/Details/5

        public ActionResult Details(int id = 0)
        {
            Photo photo = unitOfWork.PhotoRepository.Get(
                filter: p => p.PhotoId == id,
                includeProperties: p => p.Comments
                ).SingleOrDefault();

            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        //
        // GET: /PhotoManager/Create

        public ActionResult Create()
        {
            ViewBag.AlbumId = new SelectList(
                unitOfWork.AlbumRepository.Get(),
                "AlbumId",
                "Name");
            return View();
        }

        //
        // POST: /PhotoManager/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Photo photo)
        {
            if (ModelState.IsValid)
            {
                photo.CreateTime = DateTime.Now;
                unitOfWork.PhotoRepository.Insert(photo);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            ViewBag.AlbumId = new SelectList(
                unitOfWork.AlbumRepository.Get(),
                "AlbumId",
                "Name",
                photo.AlbumId);
            return View(photo);
        }

        //
        // GET: /PhotoManager/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Photo photo = unitOfWork.PhotoRepository.Get(
                filter: p => p.PhotoId == id,
                includeProperties: p => p.Comments
                ).SingleOrDefault();
            if (photo == null)
            {
                return HttpNotFound();
            }
            ViewBag.AlbumId = new SelectList(
                unitOfWork.AlbumRepository.Get(),
                "AlbumId",
                "Name",
                photo.AlbumId);
            return View(photo);
        }

        //
        // POST: /PhotoManager/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Photo photo)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.PhotoRepository.Update(photo);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            ViewBag.AlbumId = new SelectList(
                unitOfWork.AlbumRepository.Get(),
                "AlbumId",
                "Name",
                photo.AlbumId);
            return View(photo);
        }

        //
        // GET: /PhotoManager/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Photo photo = unitOfWork.PhotoRepository.GetByID(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        //
        // POST: /PhotoManager/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            unitOfWork.PhotoRepository.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public ActionResult QuickSearch(string term)
        {
            var artists = unitOfWork.PhotoRepository.Get(
                filter: p => p.Title.Contains(term)
                ).Select(p => new { value = p.Title });
            return Json(artists, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}