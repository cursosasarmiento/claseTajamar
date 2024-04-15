using clase.api.Contracts;
using clase.api.Models;

namespace clase.api.Services
{
    public class MascotaTipoService(MascotasDbContext context) : IMascotaTipoService
    {
        private readonly MascotasDbContext _context = context;

        public async Task<MascotaTipo> Create(MascotaTipo entity)
        {
            entity.Id = 0;
            await _context.MascotaTipos.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<MascotaTipo> GetById(int id)
        {
            var toReturn  = await _context.MascotaTipos.FindAsync(id);
            return toReturn!;
        }
    }
}
