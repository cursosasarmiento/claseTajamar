using clase.api.Contracts;
using clase.api.Models;
using Microsoft.EntityFrameworkCore;

namespace clase.api.Services
{
    public class PersonaService(MascotasDbContext context) : IPersonaService
    {
        private readonly MascotasDbContext _context = context;

        public async Task<Persona> Create(Persona entity)
        {
            entity.Id = 0;
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(int id)
        {
            var persona = await GetById(id) ?? throw new Exception($"No se encuentra a la persona con id: {id}");
            _context.Personas.Remove(persona);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Persona>> GetAll()
        {
            return await _context.Personas.Include(x => x.Mascotas).ThenInclude(x => x.MascotaTipo).ToListAsync();
        }

        public async Task<Persona> GetById(int id)
        {
            var result = await _context.Personas.Include(x => x.Mascotas).ThenInclude(x => x.MascotaTipo).FirstOrDefaultAsync(x => x.Id == id);
            return result!;
        }

        public async Task Update(int id, Persona entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await GetById(id) == null)
                {
                    throw new Exception($"No se encuentra a la persona con id: {id}");
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Hubo errores al actualizar a la persona con id: {id}, {ex.Message}");
            }
        }
    }
}
