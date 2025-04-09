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

        // POST: api/Historiqueconsultation
        [HttpPost]
        [Authorize()]
        public async Task<ActionResult<Historiqueconsultation?>> PostHistoriqueconsultation(HistoriqueconsultationDTO historiqueconsultationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null || identity.FindFirst("id").Value != historiqueconsultationDTO.Idclient.ToString())
            {
                return Forbid();
            }

            var historiqueConsultation = new Historiqueconsultation
            {
                Idproduit = historiqueconsultationDTO.Idproduit,
                Idclient = historiqueconsultationDTO.Idclient,
                Dateconsultation = historiqueconsultationDTO.Dateconsultation
            };

            await dataRepository.AddHistoriqueconsultationAsync(historiqueConsultation);

            return Ok(historiqueConsultation);
        }

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
