using Core.Utilities.Results.Abstract;
using Entities.DTOs.ArticleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IArticleServices
    {
        Task<IResult> CreateAsync(List<AddArticleDTO> models);
        Task<IResult> UpdateAsync(Guid Id ,List<AddArticleDTO> models);
        GetArticleDTO Get(Guid Id, string langCode);
        ICollection<GetArticleDTO> GetAll(string langCode);
        Task<IResult> DeleteAsync(Guid Id, string langCode);
    }
}
