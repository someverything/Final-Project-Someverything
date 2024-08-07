using Core.DataAccess.EntityFramework;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs.TagDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ITagDAL : IRepositoryBase<Tag>
    {
        IQueryable<Tag> GetAll();
        Task<IDataResult<CreateTagDTO>> CreateTag(CreateTagDTO model);
        Task UpdateTagAsync(Tag tag);
        Task DeleteTag(Guid Id);
        Task<Tag> GetTagAsync(Guid Id);
    }
}
