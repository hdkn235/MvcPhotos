using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcPhotos.IDAL;
using MvcPhotos.Models;

namespace MvcPhotos.DAL
{
    public class AlbumRepository : GenericRepository<Album>, IAlbumRepository
    {
        public AlbumRepository(MvcPhotoDBContext context)
            : base(context)
        { }
    }
}