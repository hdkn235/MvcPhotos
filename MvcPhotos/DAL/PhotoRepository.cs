using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcPhotos.Models;
using MvcPhotos.IDAL;

namespace MvcPhotos.DAL
{
    public class PhotoRepository : GenericRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(MvcPhotoDBContext context)
            : base(context)
        {

        }
    }
}