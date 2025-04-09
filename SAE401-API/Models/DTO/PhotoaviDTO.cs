using System.ComponentModel.DataAnnotations;

namespace SAE401_API.Models.DTO;

public partial class PhotoaviDTO
{
    [Required]
    public int Idavis { get; set; }

    [Required]
    public int Idphoto { get; set; }
}
