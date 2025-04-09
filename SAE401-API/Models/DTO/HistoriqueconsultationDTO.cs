using System.ComponentModel.DataAnnotations;

namespace SAE401_API.Models.DTO
{
    public partial class HistoriqueconsultationDTO
    {
        public int Idproduit { get; set; }

        public int Idclient { get; set; }

        [Required]
        public DateTime Dateconsultation { get; set; } = DateTime.UtcNow;
    }
}
