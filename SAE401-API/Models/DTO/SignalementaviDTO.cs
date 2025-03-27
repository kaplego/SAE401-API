namespace SAE401_API.Models.DTO
{
    public class SignalementaviDTO
    {
        public int Idavis { get; set; }
        public int Idtypesignalement { get; set; }
        public string Emailsignalement { get; set; } = null!;
        public DateTime Datesignalement { get; set; }
        public string Contenusignalement { get; set; } = null!;
    }
}
