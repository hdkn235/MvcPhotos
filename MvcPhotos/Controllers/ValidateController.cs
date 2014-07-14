using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPhotos.Common;
using MvcPhotos.DAL;

namespace MvcPhotos.Controllers
{
    public class ValidateController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public JsonResult CheckRepeatAlbumName(string name, int AlbumId = 0)
        {
            return Json(
                ValidateHelper.NotExistRepeatAlbumName(name, AlbumId), 
                JsonRequestBehavior.AllowGet);
        }
    }
}
