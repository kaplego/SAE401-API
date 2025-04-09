using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

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


        /// <summary>
        /// Créé une relation PdSm
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="produitsimilaire">La relation à ajouter</param>
        /// <response code="200">La relation à été créée</response>
        /// <response code="400">La relation n'est pas valide</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        // POST api/produitsimilaire
        [HttpPost]

        public async Task<ActionResult<Produitsimilaire?>> PostProduitsimilaire([FromBody] ProduitsimilaireDTO produitsimilaire)
        {
            var ps = new Produitsimilaire
            {
                IdproduitRef = produitsimilaire.IdproduitRef,
                IdproduitSim = produitsimilaire.IdproduitSim
            };

            await _produitsimilaireRepository.AddProduitsimilaireAsync(ps);

            return Ok(ps);
        }

        /// <summary>
        /// Supprime une relation PdSm
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="idproduitRef">L'IDproduit parent/original</param>
        /// <param name="idproduitSim">L'IDproduit similaire/enfant</param>
        /// <response code="200">La relation à été supprimée</response>
        /// <response code="404">La relation n'est pas trouvée</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
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
            return Ok();
        }
    }
}
