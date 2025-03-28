using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.DTO
{
    public partial class CommandeDTO
    {
        public int? Idcommande {  get; set; }


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
        public DateTime Datecommande { get; set; } = DateTime.Now;

        [Required]
        public bool Avecassurance { get; set; }

        [Required]
        public bool Aveclivraisonexpress { get; set; }

        public string? Instructionlivraison { get; set; }
    }
}
