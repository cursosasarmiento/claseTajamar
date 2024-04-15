using clase.api.Models;
using clase.api.Models.DTOs.MascotaDtos;

namespace clase.api.Contracts.Services
{
    public interface IMascotaService
    {
        Task<MascotaFullResponseDto> GetById(int id);
        Task<IEnumerable<MascotaSimpleResponseDto>> GetAll();
        Task<MascotaSimpleResponseDto> Create(MascotaCreateRequestDto entity);
        Task Update(int id, MascotaUpdateRequestDto entity);
        Task Delete(int id);
    }
}
