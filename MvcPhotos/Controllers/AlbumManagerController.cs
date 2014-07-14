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
    public class AlbumManagerController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        //
        // GET: /AlbumManager/

        public ActionResult Index()
        {
            var albums = unitOfWork.AlbumRepository.Get(
                orderBy: q => q.OrderBy(a => a.Name)
                );
            return View(albums);
        }

        //
        // GET: /AlbumManager/Details/5

        public ActionResult Details(int id = 0)
        {
            Album album = unitOfWork.AlbumRepository.GetByID(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        //
        // GET: /AlbumManager/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /AlbumManager/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Album album)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.AlbumRepository.Insert(album);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(album);
        }

        //
        // GET: /AlbumManager/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Album album = unitOfWork.AlbumRepository.GetByID(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        //
        // POST: /AlbumManager/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Album album)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.AlbumRepository.Update(album);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(album);
        }

        //
        // GET: /AlbumManager/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Album album = unitOfWork.AlbumRepository.GetByID(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        //
        // POST: /AlbumManager/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            unitOfWork.AlbumRepository.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}