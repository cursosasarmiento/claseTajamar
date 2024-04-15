using clase.api.Contracts.Repositories;
using clase.api.Models;
using Microsoft.EntityFrameworkCore;

namespace clase.api.Persistence.Repositories
{
    public class MascotaTipoSqlRepository(MascotasDbContext context) : IMascotaTipoRepository
    {

        private readonly MascotasDbContext _context;
        public async Task<MascotaTipo> Create(MascotaTipo entity)
        {
            await _context.MascotaTipos.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<MascotaTipo> GetById(int id)
        {
            return await _context.MascotaTipos.FindAsync(id); 
        }
    }
}
