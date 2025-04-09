using System.ComponentModel.DataAnnotations;

namespace SAE401_API.Models.DTO;

public partial class ProfessionelDTO
{
    [Required]
    public int Idclient { get; set; }

    [Required]
    public int Idactivitepro { get; set; }

    [Required]
    public string Nomsociete { get; set; } = null!;

    [Required]
    public string Numtva { get; set; } = null!;
}
