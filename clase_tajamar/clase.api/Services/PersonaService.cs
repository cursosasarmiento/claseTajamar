using AutoMapper;
using clase.api.Contracts;
using clase.api.Models;
using clase.api.Models.DTOs.PersonaDtos;
using Microsoft.EntityFrameworkCore;

namespace clase.api.Services
{
    public class PersonaService(MascotasDbContext context, IMapper _mapper) : IPersonaService
    {
        private readonly MascotasDbContext _context = context;
        private readonly IMapper mapper = _mapper;

        public async Task<PersonaSimpleResponseDto> Create(PersonaCreateRequestDto dto)
        {
            var entity = mapper.Map<Persona>(dto);
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return mapper.Map<PersonaSimpleResponseDto>(entity);
        }

        public async Task Delete(int id)
        {
            var persona = await GetById(id) ?? throw new Exception($"No se encuentra a la persona con id: {id}");
            _context.Personas.Remove(mapper.Map<Persona>(persona));
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PersonaSimpleResponseDto>> GetAll()
        {
            var result = await _context.Personas.Include(x => x.Mascotas).ThenInclude(x => x.MascotaTipo).ToListAsync();
            return mapper.Map< IEnumerable<PersonaSimpleResponseDto>>(result);
        }

        public async Task<PersonaFullResponseDto> GetById(int id)
        {
            var result = await _context.Personas.Include(x => x.Mascotas).ThenInclude(x => x.MascotaTipo).FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map< PersonaFullResponseDto > (result);
        }
        

        public async Task Update(int id, PersonaUpdateRequestDto dto)
        {
            var entity = mapper.Map<Persona>(dto);
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
