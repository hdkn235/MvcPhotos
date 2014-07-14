using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPhotos.Common;

namespace MvcPhotos.Models
{
    [Bind(Include = "AlbumId,Name,Description,CoverPath")]
    public class Album:IValidatableObject
    {
        public virtual int AlbumId { get; set; }

        [DisplayName("相册名称")]
        [Required(ErrorMessage = "请输入相册名称！")]
        [StringLength(160, ErrorMessage = "相册名称长度不能超过160个字符")]
        //Remote特性可以利用服务器端的回调函数执行客户端的验证逻辑(Ajax 实现)
        [Remote(
            "CheckRepeatAlbumName", 
            "Validate", 
            ErrorMessage = "相册名称已存在，请重新输入！",
            AdditionalFields = "AlbumId"    
        )]
        public virtual string Name { get; set; }

        [DisplayName("相册描述")]
        [StringLength(160, ErrorMessage = "相册描述长度不能超过160个字符")]
        public virtual string Description { get; set; }

        [DisplayName("封面路径")]
        public virtual string CoverPath { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }

        public IEnumerable<ValidationResult> Validate(
            ValidationContext validationContext
            )
        {
            if (!ValidateHelper.NotExistRepeatAlbumName(Name, AlbumId))
            {
                yield return new ValidationResult(
                    "相册名称已存在，请重新输入！",
                    new[] { "Name" });
            }
        }
    }
}