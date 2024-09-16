using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using Entities.DTOs.ArticleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IArticleDAL : IRepositoryBase<Article>
    {
        Task CreateArticleAsync(List<AddArticleDTO> model);
        Task UpdateArticleAsync(Guid Id, UpdateArticleDTO model);
        GetArticleDTO GetArticle(Guid Id, string langCode);
        ICollection<GetArticleDTO> GetAllArticle(Guid Id, string langCode);
        Task DeleteArticle(Guid Id);
    }
}
