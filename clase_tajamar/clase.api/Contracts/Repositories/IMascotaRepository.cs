using clase.api.Models;

namespace clase.api.Contracts.Repositories
{
    public interface IMascotaRepository
    {
        Task<Mascota> GetById(int id);
        Task<IEnumerable<Mascota>> GetAll();
        Task<Mascota> Create(Mascota persona);
        Task Update(Mascota persona);
        Task Delete(Mascota mascota);
    }
}
