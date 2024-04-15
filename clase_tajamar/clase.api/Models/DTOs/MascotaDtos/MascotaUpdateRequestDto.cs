namespace clase.api.Models.DTOs.MascotaDtos
{
    public class MascotaUpdateRequestDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public int MascotaTipoId { get; set; }
        public int PropietarioId { get; set; }
    }
}
