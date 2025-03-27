namespace SAE401_API.Models.DTO
{
    public class AdresseDTO
    {
        public int Idadresse { get; set; }
        public int Idpays { get; set; }
        public string Codeinsee { get; set; } = null!;
        public int Idclient { get; set; }
        public int Iddepartement { get; set; }
        public string? Nomadresse { get; set; }
        public string? Numerorue { get; set; }
        public string Nomrue { get; set; } = null!;
        public string Codepostaladresse { get; set; } = null!;
    }
}
