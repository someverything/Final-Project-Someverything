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
        public int MyProperty { get; set; }
    }
}
