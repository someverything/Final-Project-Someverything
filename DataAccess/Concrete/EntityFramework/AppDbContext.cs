using Core.Entities.Concrete;
using Entities.Common;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = localhost; Database = SomeverythingDb; Trusted_Connection = True; TrustServerCertificate = True");
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryLang> CategoryLangs { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleLang> ArticleLangs { get; set; }
        public DbSet<ArticleTag> ArticleTags { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AppUser> appUsers { get; set; }
        public DbSet<ArticlePhoto> ArticlePhotos { get; set; }
        public DbSet<ArtSubCat> ArtSubCats { get; set; }
       
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();

            foreach (var data in datas)
            {
                switch (data.State)
                {
                    case EntityState.Added:
                        data.Entity.CreatedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        data.Entity.UpdatedDate = DateTime.Now;
                        break;
                    default:
                        data.Entity.CreatedDate = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            var datas = ChangeTracker.Entries<BaseEntity>();

            foreach (var data in datas)
            {
                switch (data.State)
                {
                    case EntityState.Added:
                        data.Entity.CreatedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        data.Entity.UpdatedDate = DateTime.Now;
                        break;
                    default:
                        data.Entity.CreatedDate = DateTime.Now;
                        break;
                }
            }

            return base.SaveChanges();
        }
    }
}
