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
    public class HangHoaController : ControllerBase
    {
        private readonly IHangHoaRepository _hotelRepository;

        public HangHoaController(IHangHoaRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHotels([FromQuery] PagingParams pagingParams)
        {
            var results = await _hotelRepository.GetHangHoas(pagingParams);
            return Ok(new ApiResponse(Resource.GET_SUCCESS, pagingParams, results));
        }

        [HttpGet("{id:int}", Name = "GetHotel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHotel(int id)
        {
            var result = await _hotelRepository.GetHangHoa(id);
            return Ok(new ApiResponse(Resource.GET_SUCCESS, new { id = id }, result));
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateHotel([FromBody] CreateHangHoaDTO hotelDTO)
        {
            var result = await _hotelRepository.CreateHangHoa(hotelDTO);
            return Ok(new ApiResponse(Resource.CREATE_SUCCESS, null, result));
        }

        [Authorize]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateHotel([FromQuery] int id, [FromBody] UpdateHangHoaDTO hotelDTO)
        {
            var result = await _hotelRepository.UpdateHangHoa(id, hotelDTO);
            return Ok(new ApiResponse(result));
        }

        [Authorize]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteHotel([FromQuery] int id)
        {
            var result = await _hotelRepository.DeleteHangHoa(id);
            return Ok(new ApiResponse(result));
        }
    }
}
