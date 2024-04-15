using clase.api.Contracts.Services;
using clase.api.Models;
using clase.api.Models.DTOs.PersonaDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace clase.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController(IPersonaService service) : ControllerBase
    {
        private readonly IPersonaService _service = service;

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if(id == 0)
            {
                return BadRequest("El id no puede ser 0");
            }
            var result = await _service.GetById(id);
            if(result == null)
                return NotFound($"No se encuentra a la persona con el id: {id}");
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetAll();
            if(result == null || !result.Any())
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PersonaCreateRequestDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PersonaUpdateRequestDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la URL no coincide con el ID de la entidad Persona.");
            }

            await _service.Update(id, dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);

            return NoContent();
        }

    }
}
