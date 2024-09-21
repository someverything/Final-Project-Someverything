using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.ArticleDTOs;
using Microsoft.EntityFrameworkCore;

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
            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
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
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }

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
                .ToList()
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
            if (article == null) throw new Exception("Article not found");

            return article;
        }

        public List<GetArticleLangDTO> GetArticleLangs(Guid Id)
        {
            using var context = new AppDbContext()
        }

        public async Task UpdateArticleAsync(Guid Id, UpdateArticleDTO model)
        {
            await using var context = new AppDbContext();
            var article = await context.Articles
                .Include(a => a.ArticleLangs)
                .Include(a => a.ArtSubCats)
                .Include(a => a.ArticleTags)
                .Include(a => a.ArticlePhotos)
                .FirstOrDefaultAsync(a => a.Id == Id);

            if (article == null) throw new Exception("Article not found!");

            article.IsActive = model.IsActive;

            foreach (var langDTO in model.UpdateArticleLangDTOs)
            {
                var existingLang = article.ArticleLangs
                    .FirstOrDefault(al => al.LangCode == langDTO.LangCode);

                if (existingLang is not null)
                {
                    existingLang.Title = langDTO.Name;
                    existingLang.Description = langDTO.Description;
                }
                else
                {
                    article.ArticleLangs.Add(new ArticleLang
                    {
                        ArticleId = Id,
                        LangCode = langDTO.LangCode,
                        Title = langDTO.Name,
                        Description = langDTO.Description
                    });
                }
            }

            foreach (var photoDTO in model.UpdateArticlePhotoDTOs)
            {
                var existingPhoto = article.ArticlePhotos
                    .FirstOrDefault(ap => ap.FileName == photoDTO.Name);

                if (existingPhoto is not null) existingPhoto.Path = photoDTO.Path;
                else
                {
                    article.ArticlePhotos.Add(new ArticlePhoto
                    {
                        ArticleId = article.Id,
                        FileName = photoDTO.Name,
                        Path = photoDTO.Path
                    });
                }
            }

            var currentSubCatIds = article.ArtSubCats.Select(asc => asc.SubCatId).ToList();
            var newSubCats = model.SubCatId.Except(currentSubCatIds);
            var removedSubCats = currentSubCatIds.Except(model.SubCatId);

            foreach (var newSubCatId in newSubCats)
            {
                article.ArtSubCats.Add(new ArtSubCat
                {
                    SubCatId = newSubCatId,
                    ArticleId = article.Id,
                });
            }

            var subCatsToRemove = article.ArtSubCats
                .Where(asc => removedSubCats.Contains(asc.SubCatId))
                .ToList();

            foreach (var subCat in subCatsToRemove)
            {
                context.ArtSubCats.Remove(subCat);
            }

            var currentTagIds = article.ArticleTags.Select(at => at.TagId).ToList();
            var newTags = model.TagId.Except(currentTagIds);
            var removedTags = currentTagIds.Except(model.TagId);

            foreach (var newTagId in newTags)
            {
                article.ArticleTags.Add(new ArticleTag
                {
                    ArticleId = article.Id,
                    TagId = newTagId,
                });
            }

            var tagsToRemove = article.ArticleTags
                .Where(at => removedTags.Contains(at.TagId))
                .ToList();
            foreach (var tag in tagsToRemove)
            {
                context.ArticleTags.Remove(tag);
            }

            await context.SaveChangesAsync();
        }
    }
}
