using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;
using SAE401_API.Models.DataManager;

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
        public async Task<IActionResult> PostCommande([FromBody] CommandeDTO commandeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var commande = new Commande
            {
                Idcommande = commandeDTO.Idcommande,
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

            await _commandeRepository.AddCommandeAsync(commande);

            return CreatedAtAction(nameof(GetCommandeById), new { idcommande = commande.Idcommande }, commande);
        }

        // GET: api/Commande/5
        [HttpGet("{idcommande}")]
        public async Task<ActionResult<Commande>> GetCommandeById(int idcommande)
        {
            var commande = await _commandeRepository.GetCommandeByIdAsync(idcommande);

            if (commande.Value == null)
            {
                return NotFound();
            }

            return commande.Value;
        }
    }
}
