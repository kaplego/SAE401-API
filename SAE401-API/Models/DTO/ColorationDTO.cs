namespace SAE401_API.Models.DTO
{
    public class ColorationDTO
    {
        public int Idproduit { get; set; }
        public int Idcouleur { get; set; }
        public decimal Prixvente { get; set; }
        public decimal? Prixsolde { get; set; }
        public int Quantitestock { get; set; }
        public string? Descriptioncoloration { get; set; }
        public bool Estvisible { get; set; }
    }
}
