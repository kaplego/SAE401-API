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
    public class AdresseController : ControllerBase
    {
        private readonly IAdresseRepository<Adresse> dataRepository;

        public AdresseController(IAdresseRepository<Adresse> datarepo)
        {
            dataRepository = datarepo;
        }

        /*
        // GET: api/Adresse/5
        [HttpGet("{idadresse}")]
        [Authorize()]
        public async Task<ActionResult<AdresseDTO>> GetAdresseByIdAsync(int idadresse)
        {
            var adresse = await dataRepository.GetAdresseByIdAsync(idadresse);

            if (adresse.Value == null)
            {
                return NotFound();
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity; // #claims2#
            if (identity == null || identity.FindFirst("id").Value != adresse.Value.Idclient.ToString()) // #if#
            {
                return Forbid(); // #forbid#
            }

            return adresseDTO;
        }
        */

        /// <summary>
        /// Créé une adresse
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="adresse">L'adresse à ajouter</param>
        /// <response code="200">L'adresse à été ajoutée</response>
        /// <response code="400">L'adresse n'est pas valide</response>
        /// <response code="401">Un des paramètres n'est pas rempi (JWT?)</response>
        /// <response code="403">Le JWT ne correspond pas</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        // Ajouter une nouvelle adresse
        [HttpPost]
        [Authorize()]
        public async Task<ActionResult<Adresse?>> PostAdresse(AdresseDTO adresse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity; // #claims2#
            if (identity == null || identity.FindFirst("id").Value != adresse.Idclient.ToString()) // #if#
            {
                return Forbid(); // #forbid#
            }

            var newAdresse = new Adresse()
            {
                Idpays = adresse.Idpays,
                Codeinsee = adresse.Codeinsee,
                Idclient = adresse.Idclient,
                Iddepartement = adresse.Iddepartement,
                Nomadresse = adresse.Nomadresse,
                Numerorue = adresse.Numerorue,
                Nomrue = adresse.Nomrue,
                Codepostaladresse = adresse.Codepostaladresse
            };

            await dataRepository.AddAdresseAsync(newAdresse);
            return Ok(newAdresse);
        }

        /// <summary>
        /// Modifie une adresse
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="idadresse">L'ID de l'adresse à modifier</param>
        /// <param name="adresse">Les nouvelles valeurs d'adresse</param>
        /// <response code="200">L'adresse à été modifiée</response>
        /// <response code="400">L'adresse n'est pas valide</response>
        /// <response code="401">Un des paramètres n'est pas rempi (JWT?)</response>
        /// <response code="403">Le JWT ne correspond pas</response>
        /// <response code="404">L'ID ne correspond pas à une adresse'</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        // Mettre à jour une adresse existante
        [HttpPut("{idadresse}")]
        [Authorize()]

        public async Task<ActionResult<Adresse?>> PutAdresse(int idadresse, [FromBody] AdresseDTO adresse)
        {
            if (idadresse != adresse.Idadresse)
            {
                return BadRequest("Les paramètres ne correspondent pas.");
            }

            var existingAdresse = await dataRepository.GetAdresseByIdAsync(idadresse);
            if (existingAdresse.Value == null)
            {
                return NotFound();
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity; // #claims2#
            if (identity == null || identity.FindFirst("id").Value != existingAdresse.Value.Idclient.ToString()) // #if#
            {
                return Forbid(); // #forbid#
            }

            Adresse updatedAdresse = await dataRepository.UpdateAdresseAsync(existingAdresse.Value, adresse);

            return Ok(updatedAdresse);
        }

        /// <summary>
        /// Supprime une adresse
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="idadresse">L'ID de l'adresse à supprimer</param>
        /// <response code="200">L'adresse à été supprimée</response>
        /// <response code="401">Un des paramètres n'est pas rempi (JWT?)</response>
        /// <response code="403">Le JWT ne correspond pas</response>
        /// <response code="404">L'ID ne correspond pas à une adresse'</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        // Supprimer une adresse
        [HttpDelete("{idadresse}")]
        [Authorize()]

        public async Task<IActionResult> DeleteAdresse(int idadresse)
        {
            var adresse = await dataRepository.GetAdresseByIdAsync(idadresse);
            if (adresse.Value == null)
            {
                return NotFound();
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity; // #claims2#
            if (identity == null || identity.FindFirst("id").Value != adresse.Value.Idclient.ToString()) // #if#
            {
                return Forbid(); // #forbid#
            }

            await dataRepository.DeleteAdresseAsync(adresse.Value);
            return Ok();
        }
    }
}
