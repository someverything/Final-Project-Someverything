using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.ArticleDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFArticleDAL : EFRepositoryBase<Article, AppDbContext>, IArticleDAL
    {
        public async Task CreateArticleAsync(List<AddArticleDTO> model)
        {
            await using var context = new AppDbContext();
            await using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                foreach (var articleDTO in model)
                {
                    var article = new Article
                    {
                        CreatedBy = articleDTO.CreatedBy,
                        IsActive = true
                    };
                    context.Articles.Add(article);

                    var articleLang = new ArticleLang
                    {
                        ArticleId = article.Id,
                        LangCode = articleDTO.LangCode,
                        Title = articleDTO.Title,
                        Description = articleDTO.Description
                    };
                    context.ArticleLangs.Add(articleLang);
                    var artSubCat = new ArtSubCat
                    {
                        ArticleId = article.Id,
                        SubCatId = articleDTO.SubCatId
                    };
                    context.ArtSubCats.Add(artSubCat);

                    foreach (var PhotoDTO in articleDTO.ArticlePhotos)
                    {
                        var articlePhoto = new ArticlePhoto
                        {
                            ArticleId = article.Id,
                            FileName = PhotoDTO.FileName,
                            Path = PhotoDTO.Path
                        };
                        context.ArticlePhotos.Add(articlePhoto);
                    }
                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }  
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteArticle(Guid Id)
        {
            await using var context = new AppDbContext();
            var article = await context.Articles
                .Include(a => a.ArticleLangs)
                .Include(a => a.ArtSubCats)
                .Include(a => a.ArticlePhotos)
                .Include(a => a.ArticleTags)
                .FirstOrDefaultAsync(a => a.Id == Id);

            if (article == null) throw new Exception("Article not found");

            context.ArticleLangs.RemoveRange(article.ArticleLangs);
            context.ArticlePhotos.RemoveRange(article.ArticlePhotos);
            context.ArticleTags.RemoveRange(article.ArticleTags);
            context.ArtSubCats.RemoveRange(article.ArtSubCats);

            context.Articles.Remove(article);

            await context.SaveChangesAsync();
        }

        public ICollection<GetArticleDTO> GetAllArticles(string langCode)
        {
            using var context = new AppDbContext();
            var articles = context.Articles
                .Include(a => a.ArticleLangs)
                .Include(a => a.ArtSubCats)
                    .ThenInclude(asc => asc.SubCategory)
                .Include(a => a.ArticlePhotos)
                .Include(a => a.ArticleTags)
                    .ThenInclude(at => at.Tag)
                .Where(a => a.ArticleLangs.Any(al => al.LangCode == langCode))
                .Select(a => new GetArticleDTO
                {
                    Id = a.Id,
                    Title = a.ArticleLangs
                        .Where(al => al.LangCode == langCode)
                        .Select(al => al.Title)
                        .FirstOrDefault() ?? string.Empty,
                    Description = a.ArticleLangs
                        .Where(al => al.LangCode == langCode)
                        .Select(al => al.Description)
                        .FirstOrDefault() ?? string.Empty,
                    IsActive = a.IsActive,
                    LangCode = langCode,
                    Views = a.Views,
                    CreatedBy = a.CreatedBy,
                    SubCategoryName = a.ArtSubCats
                        .Select(asc => asc.SubCategory.Name)
                        .ToList(),
                    TagName = a.ArticleTags
                        .Select(at => at.Tag.Name)
                        .ToList(),
                    Photo = a.ArticlePhotos
                        .Select(ap => ap.FileName)
                .       ToList()
                }).ToList();
            return articles;
        }



        public GetArticleDTO GetArticle(Guid Id, string langCode)
        {
            using var context = new AppDbContext();
            var article = context.Articles
                .Include(a => a.ArticleLangs)
                .Include(a => a.ArtSubCats)
                    .ThenInclude(asc => asc.SubCategory)
                .Include(a => a.ArticlePhotos)
                .Include(a => a.ArticleTags)
                    .ThenInclude(at => at.Tag)
                .Where(a => a.ArticleLangs.Any(al => al.LangCode == langCode))
                .Select(a => new GetArticleDTO
                {
                    Id = a.Id,
                    Title = a.ArticleLangs
                        .Where(al => al.LangCode == langCode)
                        .Select(al => al.Title)
                        .FirstOrDefault() ?? string.Empty,
                    Description = a.ArticleLangs
                        .Where(al => al.LangCode == langCode)
                        .Select(al => al.Description)
                        .FirstOrDefault() ?? string.Empty,
                    IsActive = a.IsActive,
                    LangCode = langCode,
                    Views = a.Views,
                    CreatedBy = a.CreatedBy,
                    SubCategoryName = a.ArtSubCats
                        .Select(asc => asc.SubCategory.Name)
                        .ToList(),
                    TagName = a.ArticleTags
                        .Select(at => at.Tag.Name)
                        .ToList(),
                    Photo = a.ArticlePhotos
                        .Select(ap => ap.FileName)
                .ToList()
                }).FirstOrDefault();
            if(article == null) throw new Exception("Article not found");

            return article;
        }

        public Task UpdateArticleAsync(Guid Id, UpdateArticleDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
