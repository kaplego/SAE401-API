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
    public class CommandeController : ControllerBase
    {
        private readonly ICommandeRepository<Commande> _commandeRepository;

        public CommandeController(ICommandeRepository<Commande> commandeRepository)
        {
            _commandeRepository = commandeRepository;
        }

        /// <summary>
        /// Créé une commande
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="commande">La commande à ajouter</param>
        /// <response code="200">La commande à été créée</response>
        /// <response code="400">La commande n'est pas valide</response>
        /// <response code="401">Un des paramètres n'est pas rempi (JWT?)</response>
        /// <response code="403">Le JWT ne correspond pas</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        // POST: api/Commande
        [HttpPost]
        [Authorize()]
        public async Task<ActionResult<Commande>> PostCommande([FromBody] CommandeDTO commande)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newcommande = new Commande
            {
                Idclient = commande.Idclient,
                IdadresseLivr = commande.IdadresseLivr,
                IdadresseFact = commande.IdadresseFact,
                Idcodepromo = commande.Idcodepromo,
                Idstatut = commande.Idstatut,
                Idtransporteur = commande.Idtransporteur,
                Datecommande = commande.Datecommande,
                Avecassurance = commande.Avecassurance,
                Aveclivraisonexpress = commande.Aveclivraisonexpress,
                Instructionlivraison = commande.Instructionlivraison
            };

            var identity = HttpContext.User.Identity as ClaimsIdentity; // #claims2#
            if (identity == null || identity.FindFirst("id").Value != newcommande.Idclient.ToString()) // #if#
            {
                return Forbid(); // #forbid#
            }

            await _commandeRepository.AddCommandeAsync(newcommande);

            return Ok(newcommande);
        }

        /// <summary>
        /// Obtiens une commande
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="idcommande">L'ID de la commande</param>
        /// <response code="401">Un des paramètres n'est pas rempi (JWT?)</response>
        /// <response code="403">Le JWT ne correspond pas</response>
        /// <response code="404">La commande n'est pas trouvée</response>
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        // GET: api/Commande/5
        [HttpGet("{idcommande}")]
        [Authorize()]
        public async Task<ActionResult<Commande>> GetCommandeById(int idcommande)
        {
            var commande = await _commandeRepository.GetCommandeByIdAsync(idcommande);

            if (commande.Value == null)
            {
                return NotFound();
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity; // #claims2#
            if (identity == null || identity.FindFirst("id").Value != commande.Value.Idclient.ToString()) // #if#
            {
                return Forbid(); // #forbid#
            }

            return commande;
        }
    }
}
