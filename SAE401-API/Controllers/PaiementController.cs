using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.DataManager;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;
using System.Security.Claims;

namespace SAE401_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaiementController : ControllerBase
    {
        private readonly IPaiementRepository<Paiement> dataRepository;

        public PaiementController(IPaiementRepository<Paiement> datarepo)
        {
            dataRepository = datarepo;
        }

        [HttpPost]
        public async Task<ActionResult> PostPaiement([FromBody] PaiementDTO paiementDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var paiement = new Paiement
            {
                Idcartebancaire = paiementDTO.Idcartebancaire,
                Idcommande = paiementDTO.Idcommande,
                Idtypepaiement = paiementDTO.Idtypepaiement,
                Datepaiement = paiementDTO.Datepaiement,
                Montantpaiement = paiementDTO.Montantpaiement,
                Indicepaiement = paiementDTO.Indicepaiement
            };

            await dataRepository.AddPaiementAsync(paiement);
            return NoContent();
        }
    }
}
