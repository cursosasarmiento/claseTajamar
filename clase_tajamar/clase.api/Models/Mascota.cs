using System.Text.Json.Serialization;

namespace clase.api.Models
{
    public class Mascota
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public int MascotaTipoId { get; set; }
        public MascotaTipo MascotaTipo { get; set; } = new MascotaTipo();

        public int? PropietarioId { get; set; }
        [JsonIgnore]
        public Persona? Propietario { get; set; }
    }
}
