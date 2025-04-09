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
    public class AvisController : ControllerBase
    {
        private readonly IAvisRepository<Avisproduit> dataRepository;

        public AvisController(IAvisRepository<Avisproduit> datarepo)
        {
            dataRepository = datarepo;
        }

        /// <summary>
        /// Créé un avis
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="avis">L'avis à ajouter</param>
        /// <response code="200">L'avis à été ajouté</response>
        /// <response code="400">L'avis n'est pas valide</response>
        /// <response code="401">Un des paramètres n'est pas rempi (JWT?)</response>
        /// <response code="403">Le JWT ne correspond pas</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        // POST: api/Avis
        [HttpPost]
        [Authorize()]
        public async Task<ActionResult<Avisproduit?>> PostAvis(AvisproduitDTO avis)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null || identity.FindFirst("id").Value != avis.Idclient.ToString())
            {
                return Forbid();
            }

            var newavis = new Avisproduit
            {
                Idproduit = avis.Idproduit,
                Idclient = avis.Idclient,
                Noteavis = avis.Noteavis,
                Dateavis = avis.Dateavis,
                Commentaireavis = avis.Commentaireavis,
                Reponsemiliboo = avis.Reponsemiliboo
            };


            await dataRepository.AddAvisAsync(newavis);

            return Ok(newavis);
        }

        /// <summary>
        /// Supprime un avis
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="idavis">L'ID de l'avis à supprimer</param>
        /// <response code="200">L'avis à été suprimé</response>
        /// <response code="401">Un des paramètres n'est pas rempi (JWT?)</response>
        /// <response code="403">Le JWT ne correspond pas</response>
        /// <response code="404">L'avis n'est pas trouvé/response>
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        // DELETE: api/Avis/5
        [HttpDelete("{idavis}")]
        [Authorize()]
        public async Task<IActionResult> DeleteAvis(int idavis)
        {


            var avis = await dataRepository.GetAvisByIdAsync(idavis);

            if (avis.Value == null)
            {
                return NotFound();
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null || identity.FindFirst("id").Value != avis.Value.Idclient.ToString())
            {
                return Forbid();
            }


            await dataRepository.DeleteAvisAsync(avis.Value);
            return Ok();
        }
    }
}
