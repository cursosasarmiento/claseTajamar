namespace clase.api.Models.DTOs.PersonaDtos
{
    public class PersonaSimpleResponseDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Edad { get; set; }
    }
}
