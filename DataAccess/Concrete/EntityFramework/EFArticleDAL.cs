using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.ArticleDTOs;
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

        public Task DeletrArticle(Guid Id)
        {
            throw new NotImplementedException();
        }

        public ICollection<GetArticleDTO> GetAllArticle(Guid Id, string langCode)
        {
            throw new NotImplementedException();
        }

        public GetArticleDTO GetArticle(Guid Id, string langCode)
        {
            throw new NotImplementedException();
        }

        public Task UpdateArticleAsync(Guid Id, UpdateArticleDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
