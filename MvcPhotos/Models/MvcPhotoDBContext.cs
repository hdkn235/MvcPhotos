using System.Data.Entity;

namespace MvcPhotos.Models
{
    public class MvcPhotoDBContext : DbContext
    {
        // 您可以向此文件中添加自定义代码。更改不会被覆盖。
        // 
        // 如果您希望只要更改模型架构，Entity Framework
        // 就会自动删除并重新生成数据库，则将以下
        // 代码添加到 Global.asax 文件中的 Application_Start 方法。
        // 注意: 这将在每次更改模型时销毁并重新创建数据库。
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<MvcPhotos.Models.MvcPhotoDBContext>());

        public MvcPhotoDBContext()
            : base("name=MvcPhotoDBContext")
        {
            Database.SetInitializer(new MvcPhotoDBInitializer());
        }

        public DbSet<Photo> Photos { get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Comment> Comments { get; set; }
    }
}
