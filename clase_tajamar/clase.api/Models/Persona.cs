namespace clase.api.Models
{
    public class Persona
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Edad { get; set; }
        public List<Mascota> Mascotas { get; set; } = [];
    }
}
