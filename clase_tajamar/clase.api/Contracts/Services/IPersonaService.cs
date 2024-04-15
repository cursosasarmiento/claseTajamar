using clase.api.Models;
using clase.api.Models.DTOs.PersonaDtos;

namespace clase.api.Contracts.Services
{
    public interface IPersonaService
    {
        Task<PersonaFullResponseDto> GetById(int id);
        Task<IEnumerable<PersonaSimpleResponseDto>> GetAll();
        Task<PersonaSimpleResponseDto> Create(PersonaCreateRequestDto entity);
        Task Update(int id, PersonaUpdateRequestDto entity);
        Task Delete(int id);
    }
}
