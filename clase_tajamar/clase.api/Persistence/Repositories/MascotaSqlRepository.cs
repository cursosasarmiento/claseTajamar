using clase.api.Contracts.Repositories;
using clase.api.Models;
using Microsoft.EntityFrameworkCore;

namespace clase.api.Persistence.Repositories
{
    public class MascotaSqlRepository(MascotasDbContext context) : IMascotaRepository
    {
        private readonly MascotasDbContext _context = context;
        public async Task<Mascota> Create(Mascota mascota)
        {
            await _context.AddAsync(mascota);
            await _context.SaveChangesAsync();
            return mascota;
        }

        public async Task Delete(Mascota mascota)
        {
            _context.Mascotas.Remove(mascota);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Mascota>> GetAll()
        {
            return await _context.Mascotas.Include(x => x.MascotaTipo).ToListAsync();
        }

        public async Task<Mascota> GetById(int id)
        {
            return await _context.Mascotas.Include(x => x.MascotaTipo).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(Mascota mascota)
        {
            var propietario = await _context.Personas.FindAsync(mascota.PropietarioId);
            var tipo = await _context.MascotaTipos.FindAsync(mascota.MascotaTipoId);
            if (propietario == null)
                throw new Exception("El id del propietario no se encuentra en base de datos");
            if (tipo == null)
                throw new Exception("El id del tipo de mascota no se encuentra en base de datos");

            _context.Entry(mascota).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await GetById(mascota.Id) == null)
                {
                    throw new Exception($"No se encuentra a la persona con id: {mascota.Id}");
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Hubo errores al actualizar a la persona con id: {mascota.Id}, {ex.Message}");
            }
        }
    }
}
