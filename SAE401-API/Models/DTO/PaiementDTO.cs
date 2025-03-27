namespace SAE401_API.Models.DTO
{
    public class PaiementDTO
    {
        public int Idcartebancaire { get; set; }
        public int Idcommande { get; set; }
        public int Idtypepaiement { get; set; }
        public DateTime Datepaiement { get; set; }
        public decimal Montantpaiement { get; set; }
        public string? Indicepaiement { get; set; }
    }
}
