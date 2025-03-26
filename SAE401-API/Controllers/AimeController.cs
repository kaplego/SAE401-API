using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.DataManager;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

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

        // Récupérer une relation "aime" par client et produit
        [HttpGet("{idclient}/{idproduit}")]
        public async Task<ActionResult<AimeDTO>> GetAimeByIdAsync(int idclient, int idproduit)
        {
            var aime = await dataRepository.GetAimeByIdAsync(idclient, idproduit);

            if (aime.Value == null)
            {
                return NotFound();
            }

            var aimeDTO = new AimeDTO
            {
                Idclient = aime.Value.Idclient,
                Idproduit = aime.Value.Idproduit
            };

            return aimeDTO;
        }

        // Ajouter une relation "aime"
        [HttpPost]
        public async Task<ActionResult> PostAime([FromBody] AimeDTO aimeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var aime = new Aime
            {
                Idclient = aimeDTO.Idclient,
                Idproduit = aimeDTO.Idproduit
            };

            await dataRepository.AddAimeAsync(aime);
            return NoContent();
        }

        // Supprimer une relation "aime"
        [HttpDelete("{idclient}/{idproduit}")]
        public async Task<IActionResult> DeleteAime(int idclient, int idproduit)
        {
            var aime = await dataRepository.GetAimeByIdAsync(idclient, idproduit);
            if (aime.Value == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAimeAsync(aime.Value);
            return NoContent();
        }
    }
}
