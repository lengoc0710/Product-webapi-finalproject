using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsManagement.Controllers
{
    [ApiVersion("2.0", Deprecated = true)]
    [Route("api/loai")]
    [ApiController]
    public class LoaisController : ControllerBase
    {
        private DatabaseContext _context;

        public LoaisController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLoais()
        {
            return Ok(_context.Loais);
        }
    }
}
