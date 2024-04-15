using clase.api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace clase.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MascotasController : ControllerBase
    {
        private readonly MascotasDbContext _context;

        public MascotasController(MascotasDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == 0)
            {
                return BadRequest("El id no puede ser 8");
            }
            var result = await _context.Mascotas.FindAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _context.MascotaTipos.ToListAsync();
            if (result == null || result.Count == 0)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Mascota m)
        {
            m.Id = 0;
            var propietario = await _context.Personas.FirstOrDefaultAsync(x=>x.Id == m.PropietarioId);
            var tipo = await _context.MascotaTipos.FirstOrDefaultAsync(x => x.Id == m.MascotaTipoId);
            if (propietario == null)
                return BadRequest("El id del propietario no se encuentra en base de datos");
            if (tipo == null)
                return BadRequest("El id del tipo de mascota no se encuentra en base de datos");
            m.Propietario = propietario;
            m.MascotaTipo = tipo;
            await _context.AddAsync(m);
            await _context.SaveChangesAsync();
            return Ok(m);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Mascota m)
        {
            if (id != m.Id)
            {
                return BadRequest("El ID de la URL no coincide con el ID de la entidad Mascota.");
            }
            var propietario = await _context.Personas.FirstOrDefaultAsync(x => x.Id == m.PropietarioId);
            var tipo = _context.MascotaTipos.FirstOrDefaultAsync(x => x.Id == m.MascotaTipoId);
            if (propietario == null)
                return BadRequest("El id del propietario no se encuentra en base de datos");
            if (tipo == null)
                return BadRequest("El id del tipo de mascota no se encuentra en base de datos");

            _context.Entry(m).State = EntityState.Modified;

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
                return StatusCode(500, "No se pudo actualizar la mascota: " + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var mascota = await _context.Mascotas.FindAsync(id);
            if (mascota == null)
            {
                return NotFound();
            }

            _context.Mascotas.Remove(mascota);
            await _context.SaveChangesAsync();

            return NoContent();  // Respuesta estándar para indicar que la entidad se eliminó correctamente
        }

    }
}
