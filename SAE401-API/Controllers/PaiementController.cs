using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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


        /// <summary>
        /// Créé un paiement
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="paiement">Le paiement à ajouter</param>
        /// <response code="200">Le paiement à été créé</response>
        /// <response code="400">Le paiement n'est pas valide</response>
        /// <response code="401">Un des paramètres n'est pas rempi (JWT?)</response>
        /// <response code="403">Le JWT ne correspond pas</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        // POST
        [HttpPost]
        [Authorize()]
        public async Task<ActionResult<Paiement?>> PostPaiement([FromBody] PaiementDTO paiement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var commande = await dataRepository.GetCommandeByIdAsync(paiement.Idcommande);
            if (commande == null) NotFound();

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null || identity.FindFirst("id").Value != commande.Value.Idclient.ToString())
            {
                return Forbid();
            }

            var newpaiement = new Paiement
            {
                Idcartebancaire = paiement.Idcartebancaire,
                Idcommande = paiement.Idcommande,
                Idtypepaiement = paiement.Idtypepaiement,
                Datepaiement = paiement.Datepaiement,
                Montantpaiement = paiement.Montantpaiement,
                Indicepaiement = paiement.Indicepaiement
            };

            await dataRepository.AddPaiementAsync(newpaiement);
            return Ok(newpaiement);
        }
    }
}
