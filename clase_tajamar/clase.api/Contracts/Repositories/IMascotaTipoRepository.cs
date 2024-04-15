using clase.api.Models;

namespace clase.api.Contracts.Repositories
{
    public interface IMascotaTipoRepository
    {
        Task<MascotaTipo> GetById(int id);
        Task<MascotaTipo> Create(MascotaTipo persona);
    }
}
