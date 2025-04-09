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
    public class CommandecompositionController : ControllerBase
    {
        private readonly ICommandecompositionRepository<Commandecomposition> dataRepository;

        public CommandecompositionController(ICommandecompositionRepository<Commandecomposition> datarepo)
        {
            dataRepository = datarepo;
        }


        /// <summary>
        /// Créé une relation CmdCp
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="commandecomposition">La relation à ajouter</param>
        /// <response code="200">La relation à été modifiée</response>
        /// <response code="400">La relation n'est pas valide</response>
        /// <response code="401">Un des paramètres n'est pas rempi (JWT?)</response>
        /// <response code="403">Le JWT ne correspond pas</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Commandecomposition?>> PostCommandecomposition([FromBody] CommandecompositionDTO commandecomposition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Commande? comm = await dataRepository.GetCommandeByIdAsync(commandecomposition.Idcommande);

            if (comm == null)
            {
                return NotFound();
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null || identity.FindFirst("id").Value != comm.Idclient.ToString())
            {
                return Forbid();
            }

            var cmdcp = new Commandecomposition
            {
                Idcomposition = commandecomposition.Idcomposition,
                Idcommande = commandecomposition.Idcommande,
                Quantitecompositioncommande = commandecomposition.Quantitecompositioncommande
            };

            await dataRepository.AddCommandecompositionAsync(cmdcp);
            return Ok(cmdcp);
        }
    }
}
