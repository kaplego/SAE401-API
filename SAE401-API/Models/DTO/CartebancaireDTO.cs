namespace SAE401_API.Models.DTO
{
    public class CartebancaireDTO
    {
        public int Idcartebancaire { get; set; }
        public int Idclient { get; set; }
        public string Nomcartebancaire { get; set; } = null!;
        public DateTime Dateenregistement { get; set; }
        public string Numcartebancaire { get; set; } = null!;
        public DateTime Dateexpirationcarte { get; set; }
    }
}
