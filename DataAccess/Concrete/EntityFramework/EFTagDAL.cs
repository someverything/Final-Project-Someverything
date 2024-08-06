using AutoMapper;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.TagDTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFTagDAL : EFRepositoryBase<Tag, AppDbContext>, ITagDAL
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EFTagDAL(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void CreateTag(CreateTagDTO model)
        {
            Tag tag = new Tag();
            _context.Tags.Add(tag);
            _context.SaveChanges();
        }

        public void DeleteTag(Guid Id)
        {
            Tag tag = _context.Tags.AsNoTracking().FirstOrDefault(x => x.Id == Id);
            if (tag != null) _context.Tags.Remove(tag);
            _context.SaveChanges();
        }

        public IQueryable<Tag> GetAll()
        {
            var tags = _context.Tags.ToList();
            return _mapper.Map<IQueryable<Tag>>(tags);
        }

        public async Task<Tag> GetTagAsync(Guid Id)
        {
            var tag = await _context.Tags.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);
            return _mapper.Map<Tag>(tag);
        }

        public async Task UpdateTagAsync(UpdateTagDTO model)
        {
            var tag = await _context.Tags.FindAsync(model.Id);
            if (tag == null)
            {
                throw new KeyNotFoundException("Tag not found");
            }
            _mapper.Map(model, tag);
            _context.Tags.Update(tag);
            await _context.SaveChangesAsync();
        }
    }
}
