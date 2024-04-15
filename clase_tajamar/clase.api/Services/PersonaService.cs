using AutoMapper;
using clase.api.Contracts.Repositories;
using clase.api.Contracts.Services;
using clase.api.Models;
using clase.api.Models.DTOs.PersonaDtos;
using clase.api.Persistence;
using Microsoft.EntityFrameworkCore;

namespace clase.api.Services
{
    public class PersonaService(IPersonaRepository _repository, IMapper _mapper) : IPersonaService
    {
        private readonly IPersonaRepository personaRepository = _repository;
        private readonly IMapper mapper = _mapper;

        public async Task<PersonaSimpleResponseDto> Create(PersonaCreateRequestDto dto)
        {
            
            var entity = await personaRepository.Create(mapper.Map<Persona>(dto));
            return mapper.Map<PersonaSimpleResponseDto>(entity);
        }

        public async Task Delete(int id)
        {
            var persona = await GetById(id) ?? throw new Exception($"No se encuentra a la persona con id: {id}");
            await personaRepository.Delete(mapper.Map<Persona>(persona));
        }

        public async Task<IEnumerable<PersonaSimpleResponseDto>> GetAll()
        {
            var result = await personaRepository.GetAll();
            return mapper.Map< IEnumerable<PersonaSimpleResponseDto>>(result);
        }

        public async Task<PersonaFullResponseDto> GetById(int id)
        {
            var result = await personaRepository.GetById(id);
            return mapper.Map< PersonaFullResponseDto > (result);
        }
        

        public async Task Update(int id, PersonaUpdateRequestDto dto)
        {
           await personaRepository.Update(mapper.Map<Persona>(dto));
            
        }
    }
}
