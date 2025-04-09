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
    public class CommandecompositionController : ControllerBase
    {
        private readonly ICommandecompositionRepository<Commandecomposition> dataRepository;

        public CommandecompositionController(ICommandecompositionRepository<Commandecomposition> datarepo)
        {
            dataRepository = datarepo;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Commandecomposition?>> PostCommandecomposition([FromBody] CommandecompositionDTO commandecompositionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Commande? comm = await dataRepository.GetCommandeByIdAsync(commandecompositionDTO.Idcommande);

            if (comm == null)
            {
                return NotFound();
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null || identity.FindFirst("id").Value != comm.Idclient.ToString())
            {
                return Forbid();
            }

            var commandecomposition = new Commandecomposition
            {
                Idcomposition = commandecompositionDTO.Idcomposition,
                Idcommande = commandecompositionDTO.Idcommande,
                Quantitecompositioncommande = commandecompositionDTO.Quantitecompositioncommande
            };

            await dataRepository.AddCommandecompositionAsync(commandecomposition);
            return Ok(commandecomposition);
        }
    }
}
