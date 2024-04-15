using clase.api.Models;

namespace clase.api.Contracts
{
    public interface IMascotaService
    {
        Task<Mascota> GetById(int id);
        Task<IEnumerable<Mascota>> GetAll();
        Task<Mascota> Create(Mascota entity);
        Task Update(int id, Mascota entity);
        Task Delete(int id);
    }
}
