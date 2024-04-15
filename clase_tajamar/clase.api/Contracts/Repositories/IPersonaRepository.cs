using clase.api.Models;

namespace clase.api.Contracts.Repositories
{
    public interface IPersonaRepository
    {
        Task<Persona> GetById(int id);
        Task<IEnumerable<Persona>> GetAll();
        Task<Persona> Create(Persona persona);
        Task Update(Persona persona);
        Task Delete(Persona persona);
    }
}
