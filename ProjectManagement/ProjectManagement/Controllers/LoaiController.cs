using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsManagement.Model;
using ProductsManagement.Properties;
using ProductsManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiController : ControllerBase
    {
        private readonly ILoaiRepository _loaiRepository;

        public LoaiController(ILoaiRepository loaiRepository)
        {
            _loaiRepository = loaiRepository;
        }

        [HttpGet]
        // Can be used to override global caching on a particular endpoint at any point. 
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)]
        [HttpCacheValidation(MustRevalidate = false)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLoais([FromQuery] PagingParams pagingParams)
        {
            var results = await _loaiRepository.GetLoais(pagingParams);
            return Ok(new ApiResponse(Resource.GET_SUCCESS, pagingParams, results));
        }

        [HttpGet]
        [Route("GetLoai")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLoai([FromQuery] int id)
        {
            var result = await _loaiRepository.GetLoai(id);
            return Ok(new ApiResponse(Resource.GET_SUCCESS, new { id = id }, result));
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateLoai([FromBody] CreateLoaiDTO loaiDTO)
        {
            var result = await _loaiRepository.CreateLoai(loaiDTO);
            return Ok(new ApiResponse(Resource.CREATE_SUCCESS, null, result));
        }

        [Authorize]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] UpdateLoaiDTO loaiDTO)
        {
            var result = await _loaiRepository.UpdateLoai(id, loaiDTO);
            return Ok(new ApiResponse(result));
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var result = await _loaiRepository.DeleteLoai(id);
            return Ok(new ApiResponse(result));
        }
    }
}
