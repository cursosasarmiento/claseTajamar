using clase.api.Models;
using clase.api.Models.DTOs.MascotaTipoDto;

namespace clase.api.Contracts.Services
{
    public interface IMascotaTipoService
    {
        Task<MascotaTipoFullResponseDto> Create(MascotaTipoCreateRequestDto entity);
        Task<MascotaTipoFullResponseDto> GetById(int id);
    }
}
