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
    public class ProduitsimilaireController : ControllerBase
    {
        private readonly IProduitsimilaireRepository<Produitsimilaire> _produitsimilaireRepository;

        public ProduitsimilaireController(IProduitsimilaireRepository<Produitsimilaire> produitsimilaireRepository)
        {
            _produitsimilaireRepository = produitsimilaireRepository;
        }

        /*
        // GET api/produitsimilaire/{idproduitRef}/{idproduitSim}
        [HttpGet("{idproduitRef}/{idproduitSim}")]
        
        public async Task<ActionResult<Produitsimilaire>> GetProduitsimilaireByIdAsync(int idproduitRef, int idproduitSim)
        {
            var produitSimilaire = await _produitsimilaireRepository.GetProduitsimilaireByIdAsync(idproduitRef, idproduitSim);

            if (produitSimilaire.Value == null)
            {
                return NotFound();
            }

            return produitSimilaire;
        }
        */

        // POST api/produitsimilaire
        [HttpPost]
        
        public async Task<ActionResult> PostProduitsimilaire([FromBody] ProduitsimilaireDTO produitsimilaireDTO)
        {
            var produitSimilaire = new Produitsimilaire
            {
                IdproduitRef = produitsimilaireDTO.IdproduitRef,
                IdproduitSim = produitsimilaireDTO.IdproduitSim
            };

            await _produitsimilaireRepository.AddProduitsimilaireAsync(produitSimilaire);

            return NoContent();
        }

        // DELETE api/produitsimilaire/{idproduitRef}/{idproduitSim}
        [HttpDelete("{idproduitRef}/{idproduitSim}")]
       
        public async Task<IActionResult> DeleteProduitsimilaire(int idproduitRef, int idproduitSim)
        {
            var produitSimilaire = await _produitsimilaireRepository.GetProduitsimilaireByIdAsync(idproduitRef, idproduitSim);

            if (produitSimilaire.Value == null)
            {
                return NotFound();
            }

            await _produitsimilaireRepository.DeleteProduitsimilaireAsync(produitSimilaire.Value);
            return NoContent();
        }
    }
}
