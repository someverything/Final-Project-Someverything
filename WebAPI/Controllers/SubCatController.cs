using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResults;
using Entities.DTOs.SubCategoryDTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCatController : ControllerBase
    {
        private readonly ISubCatService _subCatService;

        public SubCatController(ISubCatService subCatService)
        {
            _subCatService = subCatService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateSubCatDTO model)
        {
            var result = _subCatService.Create(model);

            if (result.Success) return StatusCode((int)result.StatusCode, result.Message);
            else return StatusCode((int)result.StatusCode, result.Message);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAsync(Guid Id)
        {
            var result = await _subCatService.DeleteAsync(Id);

            if (result.Success) return StatusCode((int)result.StatusCode, result.Message);
            else return StatusCode((int)result.StatusCode, result.Message);
        }

        [HttpGet("{Id}")]
        public IActionResult Get(Guid Id)
        {
            var result = _subCatService.Get(Id);
            if (result is IDataResult<GetSubCatDTO> dataResult && dataResult.Success) return Ok(dataResult);
            else return StatusCode((int)((ErrorResult)result).StatusCode, result.Message);
        }
    }
}
