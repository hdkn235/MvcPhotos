using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcPhotos.Models
{
    [Bind(Include = "PhotoId,AlbumId,Title,Description,PhotoPath,CreateTime")]
    public class Photo
    {
        public int PhotoId { get; set; }

        [DisplayName("相册")]
        public int AlbumId { get; set; }

        [DisplayName("照片名称")]
        [Required(ErrorMessage = "请输入照片名称！")]
        [StringLength(160, ErrorMessage = "照片名称长度不能超过160个字符")]
        public virtual string Title { get; set; }

        [DisplayName("照片描述")]
        [StringLength(160, ErrorMessage = "描述长度不能超过160个字符")]
        public virtual string Description { get; set; }

        [DisplayName("路径")]
        public virtual string PhotoPath { get; set; }

        [DisplayName("创建时间")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public virtual DateTime CreateTime { get; set; }

        [DisplayName("访问量")]
        [DisplayFormat(NullDisplayText = "0", ApplyFormatInEditMode = true, DataFormatString = "{0:d")]
        public virtual int? Clicks { get; set; }

        [DisplayName("支持数")]
        [DisplayFormat(NullDisplayText = "0", ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public virtual int? UpCount { get; set; }

        [DisplayName("反对数")]
        [DisplayFormat(NullDisplayText = "0", ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public virtual int? DownCount { get; set; }

        public virtual Album Album { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}