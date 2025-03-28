using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.DataManager;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;
using System.IO;
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

        // POST
        [HttpPost]
        [Authorize()]
        public async Task<ActionResult> PostPaiement([FromBody] PaiementDTO paiementDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var commande = await dataRepository.GetCommandeByIdAsync(paiementDTO.Idcommande);
            if (commande == null) NotFound();

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null || identity.FindFirst("id").Value != commande.Value.Idclient.ToString())
            {
                return Forbid();
            }

            var paiement = new Paiement
            {
                Idpaiement = paiementDTO.Idpaiement,
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
