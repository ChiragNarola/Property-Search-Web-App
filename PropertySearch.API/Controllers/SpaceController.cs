using Microsoft.AspNetCore.Mvc;
using PropertySearch.Business.Filter;
using PropertySearch.Business.Models.DTOs;
using PropertySearch.Business.Models.DTOs.PageSort;
using PropertySearch.Business.Models.RMs.PageSort;
using PropertySearch.Business.Services.Interfaces;

namespace PropertySearch.API.Controllers
{
    [Route("api/spaces")]
    public class SpaceController : Controller
    {
        private readonly ISpaceService _spaceService;
        public SpaceController(
            ISpaceService spaceService
            )
        {
            _spaceService = spaceService;
        }
        [HttpPost("list")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListAsync([FromBody] PagedRM<SpaceFilter> request)
        {
            var spaceList = await _spaceService.ListAsync(request);
            return Ok(PropertySearchResultDTO<PagedResultDTO<SpaceDTO>>.Success(spaceList));
        }

        [HttpGet("avgspacesize")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAvgSpaceSizeAsync()
        {
            var avgspacesize = await _spaceService.GetAvgSpaceSize();
            return Ok(PropertySearchResultDTO<double>.Success(avgspacesize));
        }

        [HttpGet("totalspace")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTotalSpaceAsync()
        {
            var totalspace = await _spaceService.GetTotalSpace();
            return Ok(PropertySearchResultDTO<int>.Success(totalspace));
        }
    }
}
