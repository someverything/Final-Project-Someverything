using Core.DataAccess.EntityFramework;
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
        void CreateTag(CreateTagDTO model);
        Task UpdateTagAsync(UpdateTagDTO model);
        void DeleteTag(Guid Id);
        Task<Tag> GetTagAsync(Guid Id);
    }
}
