using System.ComponentModel.DataAnnotations;

namespace SAE401_API.Models.DTO
{
    public class CartebancaireDTO
    {
        public int? Idcartebancaire { get; set; }

        [Required]
        public int Idclient { get; set; }

        public string? Titulairecartebancaire { get; set; }

        public string? Nomcartebancaire { get; set; }

        [Required]
        public DateTime Dateenregistement { get; set; }

        [Required]
        public string Numcartebancaire { get; set; } = null!;

        [Required]
        public DateTime Dateexpirationcarte { get; set; }
    }
}
