using ITB.Business.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertySearch.Business.Filter;
using PropertySearch.Business.Models.DTOs;
using PropertySearch.Business.Models.DTOs.PageSort;
using PropertySearch.Business.Models.RMs;
using PropertySearch.Business.Models.RMs.PageSort;
using PropertySearch.Business.Services.Interfaces;

namespace PropertySearch.API.Controllers
{
    [Route("api/properties")]
    public class PropertyController : Controller
    {
        private readonly IPropertyService _propertyService;
        public PropertyController(
            IPropertyService propertyService
            )
        {
            _propertyService = propertyService;
        }
        [HttpPost("list")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListAsync([FromBody] PagedRM<PropertyFilter> request)
        {
            var propertyList = await _propertyService.ListAsync(request);
            return Ok(PropertySearchResultDTO<PagedResultDTO<PropertyDTO>>.Success(propertyList));
        }

        [HttpGet("property/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var propertyDetails = await _propertyService.GetByIdAsync(id);
            return Ok(PropertySearchResultDTO<PropertyDTO>.Success(propertyDetails));
        }

        [HttpPost("property/create")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAsync([FromBody][ValidateRequire] CreatePropertyRequest request)
        {
            await _propertyService.CreateAsync(request);
            return Ok(PropertySearchResultDTO.Success());
        }
    }
}
