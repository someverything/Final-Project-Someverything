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

        [HttpPost("Create")]
        public IActionResult CreateSubCat([FromBody] CreateSubCatDTO model)
        {
            var result = _subCatService.Create(model);

            if (result.Success) return StatusCode((int)result.StatusCode, result.Message);
            else return StatusCode((int)result.StatusCode, result.Message);
        }

        [HttpDelete("Delete/{Id}")]
        public async Task<IActionResult> DeleteSubCatAsync(Guid Id)
        {
            var result = await _subCatService.DeleteAsync(Id);

            if (result.Success) return StatusCode((int)result.StatusCode, result.Message);
            else return StatusCode((int)result.StatusCode, result.Message);
        }

        [HttpGet("Get/{Id}")]
        public async Task<IActionResult> GetSubCat(Guid Id)
        {
            var result = await _subCatService.GetAsync(Id);
            if (!result.Success)
                return StatusCode((int)result.StatusCode, result.Message);
            return Ok(result);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllSubCats()
        {
            var result = _subCatService.GetAll();
            if (result.Success) return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateSubCatAsync([FromBody] UpdateSubCatDTO model)
        {
            var result = await _subCatService.Update(model);
            if (!result.Success)
                return StatusCode((int)result.StatusCode, result.Message);
            return Ok(result);
        }


    }
}
