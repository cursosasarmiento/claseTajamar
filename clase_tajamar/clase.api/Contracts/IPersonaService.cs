using clase.api.Models;

namespace clase.api.Contracts
{
    public interface IPersonaService
    {
        Task<Persona> GetById(int id);
        Task<IEnumerable<Persona>> GetAll();
        Task<Persona> Create(Persona entity);
        Task Update(int id, Persona entity);
        Task Delete(int id);
    }
}
