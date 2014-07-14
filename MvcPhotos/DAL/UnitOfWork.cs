using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcPhotos.IDAL;
using MvcPhotos.Models;

namespace MvcPhotos.DAL
{
    public class UnitOfWork : IDisposable
    {
        private MvcPhotoDBContext context = new MvcPhotoDBContext();

        private IPhotoRepository photoRepository;

        public IPhotoRepository PhotoRepository
        {
            get
            {
                if (this.photoRepository == null)
                {
                    this.photoRepository = new PhotoRepository(context);
                }
                return this.photoRepository;
            }
        }

        private IAlbumRepository albumRepository;

        public IAlbumRepository AlbumRepository
        {
            get
            {
                if (this.albumRepository == null)
                {
                    this.albumRepository = new AlbumRepository(context);
                }
                return this.albumRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}