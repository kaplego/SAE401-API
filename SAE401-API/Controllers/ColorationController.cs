using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.DataManager;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

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

        // Récupérer une coloration par produit et couleur
        [HttpGet("{idproduit}/{idcouleur}")]
        public async Task<ActionResult<ColorationDTO>> GetColorationByIdAsync(int idproduit, int idcouleur)
        {
            var coloration = await dataRepository.GetColorationByIdAsync(idproduit, idcouleur);

            if (coloration.Value == null)
            {
                return NotFound();
            }

            var colorationDTO = new ColorationDTO
            {
                Idproduit = coloration.Value.Idproduit,
                Idcouleur = coloration.Value.Idcouleur,
                Prixvente = coloration.Value.Prixvente,
                Prixsolde = coloration.Value.Prixsolde,
                Quantitestock = coloration.Value.Quantitestock,
                Descriptioncoloration = coloration.Value.Descriptioncoloration,
                Estvisible = coloration.Value.Estvisible
            };

            return colorationDTO;
        }

        // Ajouter une nouvelle coloration
        [HttpPost]
        public async Task<ActionResult<Coloration?>> PostColoration([FromBody] ColorationDTO colorationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coloration = new Coloration
            {
                Idproduit = colorationDTO.Idproduit,
                Idcouleur = colorationDTO.Idcouleur,
                Prixvente = colorationDTO.Prixvente,
                Prixsolde = colorationDTO.Prixsolde,
                Quantitestock = colorationDTO.Quantitestock,
                Descriptioncoloration = colorationDTO.Descriptioncoloration,
                Estvisible = colorationDTO.Estvisible
            };

            await dataRepository.AddColorationAsync(coloration);
            return Ok(coloration);
        }

        // Mettre à jour une coloration
        [HttpPut("{idproduit}/{idcouleur}")]
        public async Task<ActionResult<Coloration?>> PutColoration(int idproduit, int idcouleur, [FromBody] ColorationDTO colorationDTO)
        {
            if (idproduit != colorationDTO.Idproduit || idcouleur != colorationDTO.Idcouleur)
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
                Idproduit = colorationDTO.Idproduit,
                Idcouleur = colorationDTO.Idcouleur,
                Prixvente = colorationDTO.Prixvente,
                Prixsolde = colorationDTO.Prixsolde,
                Quantitestock = colorationDTO.Quantitestock,
                Descriptioncoloration = colorationDTO.Descriptioncoloration,
                Estvisible = colorationDTO.Estvisible
            };

            await dataRepository.UpdateColorationAsync(existingColoration.Value, updatedColoration);
            return Ok(existingColoration.Value);
        }
    }
}
