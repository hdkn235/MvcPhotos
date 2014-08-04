using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcPhotos.DAL;

namespace MvcPhotos.Helpers
{
    public class ValidateHelper
    {
        private static UnitOfWork unitOfWork = new UnitOfWork();

        public static bool NotExistRepeatAlbumName(string name, int AlbumId)
        {
            var result = unitOfWork.AlbumRepository.Get(
               filter: p => p.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)
                       &&
                       p.AlbumId != AlbumId
               ).Count() == 0;

            return result;
        }
    }
}