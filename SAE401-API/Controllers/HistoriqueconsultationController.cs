using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Models.DataManager;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

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

        // Méthode supprimée pour ne pas avoir de PUT
        // [HttpPut("{idproduit}/{idclient}")]
        // public async Task<IActionResult> PutHistoriqueConsultation(int idproduit, int idclient, HistoriqueconsultationDTO historiqueconsultationDTO)
        // {
        //    // Code pour la mise à jour supprimé
        // }

        [HttpPost]
        public async Task<ActionResult<Historiqueconsultation>> PostHistoriqueconsultation(HistoriqueconsultationDTO historiqueconsultationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var historiqueConsultation = new Historiqueconsultation
            {
                Idproduit = historiqueconsultationDTO.Idproduit,
                Idclient = historiqueconsultationDTO.Idclient,
                Dateconsultation = historiqueconsultationDTO.Dateconsultation
            };

            await dataRepository.AddHistoriqueconsultationAsync(historiqueConsultation);

            return NoContent();
        }

        [HttpDelete("{idproduit}/{idclient}")]
        public async Task<IActionResult> DeleteHistoriqueconsultation(int idproduit, int idclient)
        {
            var historiqueConsultation = await dataRepository.GetHistoriqueconsultationByIdAsync(idproduit, idclient);

            if (historiqueConsultation.Value == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteHistoriqueconsultationAsync(historiqueConsultation.Value);
            return NoContent();
        }
    }
}
