using AutoMapper;
using clase.api.Contracts;
using clase.api.Models;
using clase.api.Models.DTOs.MascotaTipoDto;

namespace clase.api.Services
{
    public class MascotaTipoService(MascotasDbContext context, IMapper _mapper) : IMascotaTipoService
    {
        private readonly MascotasDbContext _context = context;
        private readonly IMapper mapper = _mapper;

        public async Task<MascotaTipoFullResponseDto> Create(MascotaTipoCreateRequestDto dto)
        {
            var entity = mapper.Map<MascotaTipo>(dto);
            await _context.MascotaTipos.AddAsync(entity);
            await _context.SaveChangesAsync();
            return mapper.Map<MascotaTipoFullResponseDto>(entity);
        }

        public async Task<MascotaTipoFullResponseDto> GetById(int id)
        {
            var toReturn = await _context.MascotaTipos.FindAsync(id);
            return mapper.Map<MascotaTipoFullResponseDto>(toReturn);
        }
    }
}
