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

        // POST: api/Commande
        [HttpPost]
        [Authorize()]
        public async Task<ActionResult<Commande>> PostCommande([FromBody] CommandeDTO commandeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var commande = new Commande
            {
                Idclient = commandeDTO.Idclient,
                IdadresseLivr = commandeDTO.IdadresseLivr,
                IdadresseFact = commandeDTO.IdadresseFact,
                Idcodepromo = commandeDTO.Idcodepromo,
                Idstatut = commandeDTO.Idstatut,
                Idtransporteur = commandeDTO.Idtransporteur,
                Datecommande = commandeDTO.Datecommande,
                Avecassurance = commandeDTO.Avecassurance,
                Aveclivraisonexpress = commandeDTO.Aveclivraisonexpress,
                Instructionlivraison = commandeDTO.Instructionlivraison
            };

            var identity = HttpContext.User.Identity as ClaimsIdentity; // #claims2#
            if (identity == null || identity.FindFirst("id").Value != commande.Idclient.ToString()) // #if#
            {
                return Forbid(); // #forbid#
            }

            await _commandeRepository.AddCommandeAsync(commande);

            return Ok(commande);
        }

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
