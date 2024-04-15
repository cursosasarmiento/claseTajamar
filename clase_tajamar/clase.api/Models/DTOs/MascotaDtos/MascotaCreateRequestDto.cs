namespace clase.api.Models.DTOs.MascotaDtos
{
    public class MascotaCreateRequestDto
    {        
        public string Nombre { get; set; } = string.Empty;

        public int MascotaTipoId { get; set; }
        public int PropietarioId { get; set; }
    }
}
