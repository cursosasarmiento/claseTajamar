using clase.api.Contracts;
using clase.api.Models;
using Microsoft.EntityFrameworkCore;

namespace clase.api.Services
{
    public class MascotaService(MascotasDbContext context, IPersonaService personaService, IMascotaTipoService mascotaTipoService) : IMascotaService
    {
        private readonly MascotasDbContext _context = context;
        private readonly IPersonaService _personaService = personaService;
        private readonly IMascotaTipoService _macotaTipoService = mascotaTipoService;

        public async Task<Mascota> Create(Mascota entity)
        {
            entity.Id = 0;
            if (!entity.PropietarioId.HasValue) throw new Exception("Una mascota debe tener un propietario");
            var propietario = await _personaService.GetById(entity.PropietarioId!.Value);
            var tipo = await _macotaTipoService.GetById(entity.MascotaTipoId);
            if (propietario == null)
                throw new Exception("El id del propietario no se encuentra en base de datos");
            if (tipo == null)
                throw new Exception("El id del tipo de mascota no se encuentra en base de datos");
            entity.Propietario = propietario;
            entity.MascotaTipo = tipo;
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(int id)
        {
            var Mascota = await GetById(id) ?? throw new Exception($"No se encuentra a la Mascota con id: {id}");
            _context.Mascotas.Remove(Mascota);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Mascota>> GetAll()
        {
            return await _context.Mascotas.Include(x => x.MascotaTipo).ToListAsync();
        }

        public async Task<Mascota> GetById(int id)
        {
            var result = await _context.Mascotas.Include(x => x.MascotaTipo).FirstOrDefaultAsync(x => x.Id == id);
            return result!;
        }

        public async Task Update(int id, Mascota entity)
        {
            if (!entity.PropietarioId.HasValue) throw new Exception("Una mascota debe tener un propietario");
            var propietario = await _personaService.GetById(entity.PropietarioId!.Value);
            var tipo = await _macotaTipoService.GetById(entity.MascotaTipoId);
            if (propietario == null)
                throw new Exception("El id del propietario no se encuentra en base de datos");
            if (tipo == null)
                throw new Exception("El id del tipo de mascota no se encuentra en base de datos");

            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await GetById(id) == null)
                {
                    throw new Exception($"No se encontro la mascota con el id: {id}");
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo actualizar la mascota con el id: {id}, {ex.Message}");
            }
        }
    }
}
