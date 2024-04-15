using clase.api.Models;

namespace clase.api.Contracts
{
    public interface IMascotaTipoService
    {
        Task<MascotaTipo> Create(MascotaTipo entity);
        Task<MascotaTipo> GetById(int id);
    }
}
