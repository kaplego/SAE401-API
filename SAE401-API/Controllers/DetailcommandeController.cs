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
    public class DetailcommandeController : ControllerBase
    {
        private readonly IDetailcommandeRepository<Detailcommande> dataRepository;

        public DetailcommandeController(IDetailcommandeRepository<Detailcommande> datarepo)
        {
            dataRepository = datarepo;
        }

        /// <summary>
        /// Créé une relation DtCmd
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="detailcommande">La relation à ajouter</param>
        /// <response code="200">La relation à été ajoutée</response>
        /// <response code="400">La relation n'est pas valide</response>
        /// <response code="401">Un des paramètres n'est pas rempi (JWT?)</response>
        /// <response code="403">Le JWT ne correspond pas</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Detailcommande?>> PostDetailcommande([FromBody] DetailcommandeDTO detailcommande)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Commande? comm = await dataRepository.GetCommandeByIdAsync(detailcommande.Idcommande);

            if (comm == null)
            {
                return NotFound();
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null || identity.FindFirst("id").Value != comm.Idclient.ToString())
            {
                return Forbid();
            }

            var newdetailcommande = new Detailcommande
            {
                Idproduit = detailcommande.Idproduit,
                Idcouleur = detailcommande.Idcouleur,
                Idcommande = detailcommande.Idcommande,
                Quantitecommande = detailcommande.Quantitecommande
            };


            await dataRepository.AddDetailcommandeAsync(newdetailcommande);
            return Ok(newdetailcommande);
        }
    }
}
