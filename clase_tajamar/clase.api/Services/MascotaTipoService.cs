using AutoMapper;
using clase.api.Contracts.Repositories;
using clase.api.Contracts.Services;
using clase.api.Models;
using clase.api.Models.DTOs.MascotaTipoDto;
using clase.api.Persistence;

namespace clase.api.Services
{
    public class MascotaTipoService(IMascotaTipoRepository repository, IMapper _mapper) : IMascotaTipoService
    {
        private readonly IMascotaTipoRepository _repository = repository;
        private readonly IMapper mapper = _mapper;

        public async Task<MascotaTipoFullResponseDto> Create(MascotaTipoCreateRequestDto dto)
        {
            
            var entity = await _repository.Create(mapper.Map<MascotaTipo>(dto));
            return mapper.Map<MascotaTipoFullResponseDto>(entity);
        }

        public async Task<MascotaTipoFullResponseDto> GetById(int id)
        {
            var toReturn = await _repository.GetById(id);
            return mapper.Map<MascotaTipoFullResponseDto>(toReturn);
        }
    }
}
