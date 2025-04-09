using System.ComponentModel.DataAnnotations;

namespace SAE401_API.Models.DTO
{
    public partial class CommandeDTO
    {
        public int? Idcommande { get; set; }


        [Required]
        public int Idclient { get; set; }

        [Required]
        public int IdadresseLivr { get; set; }

        public int? Idcodepromo { get; set; }

        [Required]
        public int IdadresseFact { get; set; }

        [Required]
        public int Idstatut { get; set; }

        [Required]
        public int Idtransporteur { get; set; }

        [Required]
        public DateTime Datecommande { get; set; } = DateTime.UtcNow;

        [Required]
        public bool Avecassurance { get; set; }

        [Required]
        public bool Aveclivraisonexpress { get; set; }

        public string? Instructionlivraison { get; set; }
    }
}
