using clase.api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace clase.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoController : ControllerBase
    {
        private readonly MascotasDbContext _context;

        public TipoController(MascotasDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(MascotaTipo mt)
        {
            mt.Id = 0;
            await _context.MascotaTipos.AddAsync(mt);
            await _context.SaveChangesAsync();
            return Ok(mt);
        }


    }
}
