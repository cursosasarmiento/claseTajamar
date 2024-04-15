using AutoMapper;
using clase.api.Contracts.Repositories;
using clase.api.Models;
using Microsoft.EntityFrameworkCore;

namespace clase.api.Persistence.Repositories
{
    public class PersonaSqlRepository(MascotasDbContext context) : IPersonaRepository
    {
        private readonly MascotasDbContext _context = context;
        public async Task<Persona> Create(Persona persona)
        {
            await _context.AddAsync(persona);
            await _context.SaveChangesAsync();
            return persona;
        }

        public async Task Delete(Persona persona)
        {
            _context.Personas.Remove(persona);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Persona>> GetAll()
        {
            return await _context.Personas.Include(x => x.Mascotas).ThenInclude(x => x.MascotaTipo).ToListAsync();
        }

        public async Task<Persona> GetById(int id)
        {
            return await _context.Personas.Include(x => x.Mascotas).ThenInclude(x => x.MascotaTipo).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(Persona persona)
        {
            _context.Entry(persona).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await GetById(persona.Id) == null)
                {
                    throw new Exception($"No se encuentra a la persona con id: {persona.Id}");
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Hubo errores al actualizar a la persona con id: {persona.Id}, {ex.Message}");
            }
        }
    }
}
