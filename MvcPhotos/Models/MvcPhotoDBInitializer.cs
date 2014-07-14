using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MvcPhotos.Models
{
    public class MvcPhotoDBInitializer : DropCreateDatabaseIfModelChanges<MvcPhotoDBContext>
    {
        protected override void Seed(MvcPhotoDBContext context)
        {
            var albums = new List<Album>
            {
                new Album{Name="人物",Description="人物",CoverPath="nopic.gif"},
                new Album{Name="风景",Description="风景",CoverPath="nopic.gif"},
                new Album{Name="卡通",Description="卡通",CoverPath="nopic.gif"},
                new Album{Name="家庭",Description="家庭",CoverPath="nopic.gif"},
                new Album{Name="自拍",Description="自拍",CoverPath="nopic.gif"}
            };
            albums.ForEach(a => context.Albums.Add(a));
            context.SaveChanges();

            var photos = new List<Photo> 
            { 
                new Photo{AlbumId=1,Title="图片1",PhotoPath="01.jpg",Description="图片one",CreateTime=DateTime.Parse("2005-09-01")},
                new Photo{AlbumId=2,Title="图片2",PhotoPath="02.jpg",Description="图片two",CreateTime=DateTime.Parse("2006-09-01")},
                new Photo{AlbumId=3,Title="图片3",PhotoPath="03.jpg",Description="图片three",CreateTime=DateTime.Parse("2007-09-01")},
                new Photo{AlbumId=4,Title="图片4",PhotoPath="04.jpg",Description="图片four",CreateTime=DateTime.Parse("2008-09-01")},
                new Photo{AlbumId=1,Title="图片5",PhotoPath="05.jpg",Description="图片five",CreateTime=DateTime.Parse("2009-09-01")},
                new Photo{AlbumId=2,Title="图片6",PhotoPath="06.jpg",Description="图片six",CreateTime=DateTime.Parse("2010-09-01")},
                new Photo{AlbumId=3,Title="图片7",PhotoPath="07.jpg",Description="图片seven",CreateTime=DateTime.Parse("2011-09-01")}
            };
            photos.ForEach(p => context.Photos.Add(p));
            context.SaveChanges();
        }
    }
}