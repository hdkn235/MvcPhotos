using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcPhotos.Models;

namespace MvcPhotos.ViewModels
{
    public class UploadPhoto
    {
        public HttpPostedFileBase File { get; set; }

        public Photo Photo { get; set; }
    }
}