using clase.api.Models.DTOs.MascotaDtos;

namespace clase.api.Models.DTOs.PersonaDtos
{
    public class PersonaFullResponseDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Edad { get; set; }
        public List<MascotaSimpleResponseDto> Mascotas { get; set; } = [];
    }
}
