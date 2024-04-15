using clase.api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace clase.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly MascotasDbContext _context;

        public PersonasController(MascotasDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if(id == 0)
            {
                return BadRequest("El id no puede ser 0");
            }
            var result = await _context.Personas.Include(x=>x.Mascotas).ThenInclude(x=>x.MascotaTipo).FirstOrDefaultAsync(x=>x.Id == id);
            if(result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _context.Personas.Include(x=>x.Mascotas).ThenInclude(x => x.MascotaTipo).ToListAsync();
            if(result == null || result.Count == 0)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Persona p)
        {
            p.Id = 0;
            await _context.AddAsync(p);
            await _context.SaveChangesAsync();
            return Ok(p);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Persona persona)
        {
            if (id != persona.Id)
            {
                return BadRequest("El ID de la URL no coincide con el ID de la entidad Persona.");
            }

            _context.Entry(persona).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Personas.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se pudo actualizar la persona: " + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var persona = await _context.Personas.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }

            _context.Personas.Remove(persona);
            await _context.SaveChangesAsync();

            return NoContent();  // Respuesta estándar para indicar que la entidad se eliminó correctamente
        }

    }
}
