using clase.api.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace clase.api.Persistence
{
    public class MascotasDbSeeder
    {

        public void SeedPersonas(EntityTypeBuilder<Persona> builder)
        {
            builder.HasData(

                new Persona
                {
                    Id = 1,
                    Nombre = "Alejandro",
                    Edad = 30,
                    Mascotas = []
                },
                new Persona
                {
                    Id = 2,
                    Nombre = "Maria",
                    Edad = 15,
                    Mascotas = []
                },
                new Persona
                {
                    Id = 3,
                    Nombre = "Antonia",
                    Edad = 45,
                    Mascotas = []
                }

                );
        }

    }
}
