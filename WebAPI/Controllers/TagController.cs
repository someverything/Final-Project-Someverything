using Business.Abstract;
using DataAccess.Abstract;
using Entities.DTOs.TagDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : Controller
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpPost("Create")]
        public IActionResult CreateTag([FromBody] CreateTagDTO model)
        {
            var result = _tagService.Create(model);
            if(result.Success) return Ok(result);
            return BadRequest(result.Message);
        }
    }
}
