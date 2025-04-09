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
    public class HistoriqueconsultationController : ControllerBase
    {
        private readonly IHistoriqueconsultationRepository<Historiqueconsultation> dataRepository;

        public HistoriqueconsultationController(IHistoriqueconsultationRepository<Historiqueconsultation> datarepo)
        {
            dataRepository = datarepo;
        }

        /*
        [HttpGet("{idproduit}/{idclient}")]
        public async Task<ActionResult<Historiqueconsultation>> GetHistoriqueconsultationByIdAsync(int idproduit, int idclient)
        {
            var historiqueConsultation = await dataRepository.GetHistoriqueconsultationByIdAsync(idproduit, idclient);

            if (historiqueConsultation.Value == null)
            {
                return NotFound();
            }

            return historiqueConsultation;
        }
        */

        /// <summary>
        /// Créé un historique
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="historique">L'historique à ajouter</param>
        /// <response code="200">L'historique à été créé</response>
        /// <response code="400">L'historique n'est pas valide</response>
        /// <response code="401">Un des paramètres n'est pas rempi (JWT?)</response>
        /// <response code="403">Le JWT ne correspond pas</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        // POST: api/Historiqueconsultation
        [HttpPost]
        [Authorize()]
        public async Task<ActionResult<Historiqueconsultation?>> PostHistoriqueconsultation(HistoriqueconsultationDTO historique)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null || identity.FindFirst("id").Value != historique.Idclient.ToString())
            {
                return Forbid();
            }

            var newHist = new Historiqueconsultation
            {
                Idproduit = historique.Idproduit,
                Idclient = historique.Idclient,
                Dateconsultation = historique.Dateconsultation
            };

            await dataRepository.AddHistoriqueconsultationAsync(newHist);

            return Ok(newHist);
        }


        /// <summary>
        /// Supprime un historique
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="idproduit">L'IDproduit à supprimer</param>
        /// <param name="idclient">L'IDclient à supprimer</param>
        /// <response code="200">L'historique à été supprimé</response>
        /// <response code="401">Un des paramètres n'est pas rempi (JWT?)</response>
        /// <response code="403">Le JWT ne correspond pas</response>
        /// <response code="404">L'historique n'est pas trouvé</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        // DELETE: api/Historiqueconsultation/idproduit/idclient
        [HttpDelete("{idproduit}/{idclient}")]
        [Authorize()]
        public async Task<IActionResult> DeleteHistoriqueconsultation(int idproduit, int idclient)
        {

            var historiqueConsultation = await dataRepository.GetHistoriqueconsultationByIdAsync(idproduit, idclient);

            if (historiqueConsultation.Value == null)
            {
                return NotFound();
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null || identity.FindFirst("id").Value != idclient.ToString())
            {
                return Forbid();
            }



            await dataRepository.DeleteHistoriqueconsultationAsync(historiqueConsultation.Value);
            return Ok();
        }
    }
}
