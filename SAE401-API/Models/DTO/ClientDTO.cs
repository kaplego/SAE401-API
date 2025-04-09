using System.ComponentModel.DataAnnotations;

namespace SAE401_API.Models.DTO;

public partial class ClientDTO
{
    public int? Idclient { get; set; }

    [Required]
    public string Nomclient { get; set; } = null!;

    [Required]
    public string Prenomclient { get; set; } = null!;


    public char? Civiliteclient { get; set; }

    [Required]
    public string Emailclient { get; set; } = null!;


    public string? Telfixeclient { get; set; }

    [Required]
    public string Telportableclient { get; set; } = null!;

    [Required]
    public DateTime? Datecreationcompte { get; set; } = DateTime.UtcNow;

    public string? Hashmdp { get; set; } = null;

    [Required]
    public int Pointfideliteclient { get; set; }

    [Required]
    public bool Newslettermiliboo { get; set; }

    [Required]
    public bool Newsletterpartenaires { get; set; }
}
