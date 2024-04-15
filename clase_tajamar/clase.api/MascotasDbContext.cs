using clase.api.Models;
using Microsoft.EntityFrameworkCore;

namespace clase.api
{
    public class MascotasDbContext : DbContext
    {
        public MascotasDbContext(DbContextOptions<MascotasDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Persona> Personas { get; set; }
        public virtual DbSet<Mascota> Mascotas { get; set; }
        public virtual DbSet<MascotaTipo> MascotaTipos { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Persona>(entity => { 
                entity.HasKey(p => p.Id);
                entity.HasMany<Mascota>(x => x.Mascotas);
            });                

            builder.Entity<Mascota>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(m => m.MascotaTipo)
                    .WithMany().HasForeignKey(m => m.MascotaTipoId);

                entity.HasOne(m => m.Propietario)
                      .WithMany(p => p.Mascotas)
                      .HasForeignKey(m => m.PropietarioId);
            });

            builder.Entity<MascotaTipo>(entity =>
            {
                entity.HasKey(x => x.Id);
            });
               
                

        }
    }
}
