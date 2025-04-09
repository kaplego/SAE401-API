using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValeurattributController : ControllerBase
    {
        private readonly IValeurattributRepository<Valeurattribut> dataRepository;


        public ValeurattributController(IValeurattributRepository<Valeurattribut> datarepo)
        {
            dataRepository = datarepo;
        }


        /// <summary>
        /// Créé une relation VaAt
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="valeurattribut">La relation à ajouter</param>
        /// <response code="200">La relation à été créée</response>
        /// <response code="400">La relation n'est pas valide</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<ActionResult<Valeurattribut?>> PostValeurattribut([FromBody] ValeurattributDTO valeurattribut)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newVA = new Valeurattribut
            {
                Idattribut = valeurattribut.Idattribut,
                Idproduit = valeurattribut.Idproduit,
                Valeur = valeurattribut.Valeur
            };

            await dataRepository.AddValeurattributAsync(newVA);
            return Ok(newVA);
        }

        /// <summary>
        /// Modifie une relation VaAt
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="idproduit">L'IDproduit à modifier</param>
        /// <param name="idattribut">L'IDattribut à modifier</param>
        /// <param name="valeurattribut">La relation mise à jour</param>
        /// <response code="200">La relation à été modifiée</response>
        /// <response code="400">La relation n'est pas valide</response>
        /// <response code="404">La relation n'est pas trouvée</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        [HttpPut("{idattribut}/{idproduit}")]
        public async Task<ActionResult<Valeurattribut?>> PutValeurattribut(int idattribut, int idproduit, [FromBody] ValeurattributDTO valeurattribut)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (idattribut != valeurattribut.Idattribut || idproduit != valeurattribut.Idproduit)
            {
                return BadRequest("Les paramètres ne correspondent pas.");
            }

            var existingValeurattribut = await dataRepository.GetValeurattributByIdAsync(idattribut, idproduit);
            if (existingValeurattribut.Value == null)
            {
                return NotFound();
            }

            Valeurattribut va = await dataRepository.UpdateValeurattributAsync(existingValeurattribut.Value, valeurattribut);
            return Ok(va);
        }

        /// <summary>
        /// Supprime une relation VaAt
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="idproduit">L'IDproduit à supprimer</param>
        /// <param name="idattribut">L'IDattribut à supprimer</param>
        /// <response code="200">La relation à été supprimée</response>
        /// <response code="404">La relation n'est pas trouvée</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpDelete("{idattribut}/{idproduit}")]
        public async Task<IActionResult> DeleteValeurattribut(int idattribut, int idproduit)
        {
            var valeurAttribut = await dataRepository.GetValeurattributByIdAsync(idattribut, idproduit);
            if (valeurAttribut.Value == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteValeurattributAsync(valeurAttribut.Value);
            return Ok();
        }
    }
}
