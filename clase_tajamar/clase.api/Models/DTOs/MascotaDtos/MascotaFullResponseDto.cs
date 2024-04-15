using clase.api.Models.DTOs.MascotaTipoDto;
using clase.api.Models.DTOs.PersonaDtos;
using System.Text.Json.Serialization;

namespace clase.api.Models.DTOs.MascotaDtos
{
    public class MascotaFullResponseDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public int MascotaTipoId { get; set; }
        public MascotaTipoFullResponseDto MascotaTipo { get; set; } = new MascotaTipoFullResponseDto();

        public int? PropietarioId { get; set; }
        public PersonaSimpleResponseDto? Propietario { get; set; }
    }
}
