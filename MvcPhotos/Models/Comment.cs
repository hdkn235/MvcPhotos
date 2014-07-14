using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPhotos.Models
{
    public class Comment
    {
        public virtual int CommentId { get; set; }
        public virtual int PhotoId { get; set; }
        public virtual string Content { get; set; }
        public virtual DateTime CreateTime { get; set; }
        public virtual int? UpCount { get; set; }
        public virtual int? DownCount { get; set; }

        public virtual Photo Photo { get; set; }
    }
}