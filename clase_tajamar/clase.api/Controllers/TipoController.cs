using clase.api.Contracts;
using clase.api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace clase.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoController(IMascotaTipoService service) : ControllerBase
    {
        private readonly IMascotaTipoService _service = service;

        [HttpPost]
        public async Task<IActionResult> Post(MascotaTipo mt)
        {
            var response = await _service.Create(mt);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetById(id);
            if(result == null)
                return NotFound();
            return Ok(result);
        }


    }
}
