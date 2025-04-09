using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorationController : ControllerBase
    {
        private readonly IColorationRepository<Coloration> dataRepository;

        // Le constructeur accepte l'interface IColorationRepository
        public ColorationController(IColorationRepository<Coloration> datarepo)
        {
            dataRepository = datarepo;
        }

        /// <summary>
        /// Obtiens une coloration
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="idproduit">L'IDproduit à récupérer</param>
        /// <param name="idcouleur">L'IDcouleur à récupérer</param>
        /// <response code="404">La coloration n'est pas trouvée</response>
        [ProducesResponseType(404)]
        // Récupérer une coloration par produit et couleur
        [HttpGet("{idproduit}/{idcouleur}")]
        public async Task<ActionResult<Coloration?>> GetColorationByIdAsync(int idproduit, int idcouleur)
        {
            var coloration = await dataRepository.GetColorationByIdAsync(idproduit, idcouleur);

            if (coloration.Value == null)
            {
                return NotFound();
            }

            return coloration;
        }

        /// <summary>
        /// Créé une coloration
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="coloration">La coloration à ajouter</param>
        /// <response code="200">La coloration à été créée</response>
        /// <response code="400">La coloration n'est pas valide</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        // Ajouter une nouvelle coloration
        [HttpPost]
        public async Task<ActionResult<Coloration?>> PostColoration([FromBody] ColorationDTO coloration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newcoloration = new Coloration
            {
                Idproduit = coloration.Idproduit,
                Idcouleur = coloration.Idcouleur,
                Prixvente = coloration.Prixvente,
                Prixsolde = coloration.Prixsolde,
                Quantitestock = coloration.Quantitestock,
                Descriptioncoloration = coloration.Descriptioncoloration,
                Estvisible = coloration.Estvisible
            };

            await dataRepository.AddColorationAsync(newcoloration);
            return Ok(newcoloration);
        }

        /// <summary>
        /// Modifie une coloration
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="idproduit">L'IDproduit à modifier</param>
        /// <param name="idcouleur">L'IDcouleur à modifier</param>
        /// <param name="coloration">La coloration mise à jour</param>
        /// <response code="200">La coloration à été modifiée</response>
        /// <response code="400">La coloration n'est pas valide</response>
        /// <response code="404">La coloration n'est pas trouvée</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        // Mettre à jour une coloration
        [HttpPut("{idproduit}/{idcouleur}")]
        public async Task<ActionResult<Coloration?>> PutColoration(int idproduit, int idcouleur, [FromBody] ColorationDTO coloration)
        {
            if (idproduit != coloration.Idproduit || idcouleur != coloration.Idcouleur)
            {
                return BadRequest("Les paramètres ne correspondent pas.");
            }

            var existingColoration = await dataRepository.GetColorationByIdAsync(idproduit, idcouleur);
            if (existingColoration.Value == null)
            {
                return NotFound();
            }

            var updatedColoration = new Coloration
            {
                Idproduit = coloration.Idproduit,
                Idcouleur = coloration.Idcouleur,
                Prixvente = coloration.Prixvente,
                Prixsolde = coloration.Prixsolde,
                Quantitestock = coloration.Quantitestock,
                Descriptioncoloration = coloration.Descriptioncoloration,
                Estvisible = coloration.Estvisible
            };

            await dataRepository.UpdateColorationAsync(existingColoration.Value, updatedColoration);
            return Ok(existingColoration.Value);
        }
    }
}
