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
    public class AimeController : ControllerBase
    {
        private readonly IAimeRepository<Aime> dataRepository;

        // Le constructeur accepte l'interface IAimeRepository
        public AimeController(IAimeRepository<Aime> datarepo)
        {
            dataRepository = datarepo;
        }

        /*/ Récupérer une relation "aime" par client et produit
        [HttpGet("{idclient}/{idproduit}")]
        [Authorize()]
        public async Task<ActionResult<Aime?>> GetAimeByIdAsync(int idclient, int idproduit)
        {
            var aime = await dataRepository.GetAimeByIdAsync(idclient, idproduit);

            if (aime.Value == null)
            {
                return NotFound();
            }

            
            var identity = HttpContext.User.Identity as ClaimsIdentity; // #claims2#
            if (identity == null || identity.FindFirst("id").Value != aime.Value.Idclient.ToString()) // #if#
            {
                return Forbid(); // #forbid#
            }

            return aime;
        }
        /*/

        /// <summary>
        /// Créé une relation Aime
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="aime">La relation à ajouter</param>
        /// <response code="200">La relation à été ajoutée</response>
        /// <response code="401">Un des paramètres n'est pas rempi (JWT?)</response>
        /// <response code="403">Le JWT ne correspond pas</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        // Ajouter une relation "aime"
        [HttpPost]
        [Authorize()]
        public async Task<ActionResult<Aime?>> PostAime([FromBody] AimeDTO aime)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newaime = new Aime
            {
                Idclient = aime.Idclient,
                Idproduit = aime.Idproduit
            };

            var identity = HttpContext.User.Identity as ClaimsIdentity; // #claims2#
            if (identity == null || identity.FindFirst("id").Value != newaime.Idclient.ToString()) // #if#
            {
                return Forbid(); // #forbid#
            }

            await dataRepository.AddAimeAsync(newaime);
            return Ok(newaime);
        }

        /// <summary>
        /// Supprime une relation Aime
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="idclient">L'IDclient identifiant</param>
        /// <param name="idproduit">L'IDproduit identifiant</param>
        /// <response code="200">La relation à été supprimée</response>
        /// <response code="401">Un des paramètres n'est pas rempi (JWT?)</response>
        /// <response code="403">Le JWT ne correspond pas</response>
        /// <response code="404">La relation n'est pas trouvée</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        // Supprimer une relation "aime"
        [HttpDelete("{idclient}/{idproduit}")]
        [Authorize()]
        public async Task<IActionResult> DeleteAime(int idclient, int idproduit)
        {
            var aime = await dataRepository.GetAimeByIdAsync(idclient, idproduit);
            if (aime.Value == null)
            {
                return NotFound();
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity; // #claims2#
            if (identity == null || identity.FindFirst("id").Value != aime.Value.Idclient.ToString()) // #if#
            {
                return Forbid(); // #forbid#
            }

            await dataRepository.DeleteAimeAsync(aime.Value);
            return Ok();
        }
    }
}
