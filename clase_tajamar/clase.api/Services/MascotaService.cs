using AutoMapper;
using clase.api.Contracts.Services;
using clase.api.Models;
using clase.api.Models.DTOs.MascotaDtos;
using clase.api.Persistence;
using Microsoft.EntityFrameworkCore;

namespace clase.api.Services
{
    public class MascotaService(MascotasDbContext context, IPersonaService personaService, IMascotaTipoService mascotaTipoService, IMapper _mapper) : IMascotaService
    {
        private readonly MascotasDbContext _context = context;
        private readonly IPersonaService _personaService = personaService;
        private readonly IMascotaTipoService _macotaTipoService = mascotaTipoService;
        private readonly IMapper mapper = _mapper;

        public async Task<MascotaSimpleResponseDto> Create(MascotaCreateRequestDto dto)
        {

            var propietario = await _context.Personas.FindAsync(dto.PropietarioId);
            var tipo = await _context.MascotaTipos.FindAsync(dto.MascotaTipoId);
            if (propietario == null)
                throw new Exception("El id del propietario no se encuentra en base de datos");
            if (tipo == null)
                throw new Exception("El id del tipo de mascota no se encuentra en base de datos");

            var entity = mapper.Map<Mascota>(dto);

            // Asigna directamente las entidades obtenidas del contexto
            entity.Propietario = propietario;
            entity.MascotaTipo = tipo;

            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();

            return mapper.Map<MascotaSimpleResponseDto>(entity);
        }

        public async Task Delete(int id)
        {
            var Mascota = await GetById(id) ?? throw new Exception($"No se encuentra a la Mascota con id: {id}");
            _context.Mascotas.Remove(mapper.Map<Mascota>(Mascota));
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MascotaSimpleResponseDto>> GetAll()
        {
            var response =  await _context.Mascotas.Include(x => x.MascotaTipo).ToListAsync();
            return mapper.Map< IEnumerable<MascotaSimpleResponseDto>>(response);
        }

        public async Task<MascotaFullResponseDto> GetById(int id)
        {
            var result = await _context.Mascotas.Include(x => x.MascotaTipo).FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<MascotaFullResponseDto>(result);
        }

        public async Task Update(int id, MascotaUpdateRequestDto dto)
        {
            var propietario = await _context.Personas.FindAsync(dto.PropietarioId);
            var tipo = await _context.MascotaTipos.FindAsync(dto.MascotaTipoId);
            if (propietario == null)
                throw new Exception("El id del propietario no se encuentra en base de datos");
            if (tipo == null)
                throw new Exception("El id del tipo de mascota no se encuentra en base de datos");

            var entity = mapper.Map<Mascota>(dto);

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
