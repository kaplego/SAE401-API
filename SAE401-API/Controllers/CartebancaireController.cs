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
    public class CartebancaireController : ControllerBase
    {
        private readonly ICartebancaireRepository<Cartebancaire> dataRepository;

        public CartebancaireController(ICartebancaireRepository<Cartebancaire> datarepo)
        {
            dataRepository = datarepo;
        }

        /// <summary>
        /// Récupère les cartes du client
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="idclient">L'ID du client</param>
        /// <response code="401">Un des paramètres n'est pas rempi (JWT?)</response>
        /// <response code="403">Le JWT ne correspond pas</response>
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        // GET: api/Cartebancaire/GetAllCartebancaireByClient/idclient
        [HttpGet("[action]/{idclient}")]
        [Authorize()]
        public async Task<ActionResult<IEnumerable<Cartebancaire>>> GetAllCartebancaireByClient(int idclient)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null || identity.FindFirst("id").Value != idclient.ToString())
            {
                return Forbid();
            }

            return await dataRepository.GetAllCartebancaireByClientAsync(idclient);
        }


        /// <summary>
        /// Créé une carte bancaire
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="cb">La carte à ajouter</param>
        /// <response code="200">La carte à été ajoutée</response>
        /// <response code="400">La carte n'est pas valide</response>
        /// <response code="401">Un des paramètres n'est pas rempi (JWT?)</response>
        /// <response code="403">Le JWT ne correspond pas</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        // POST: api/Cartebancaire
        [HttpPost]
        [Authorize()]
        public async Task<ActionResult<Cartebancaire?>> PostCartebancaire([FromBody] CartebancaireDTO cb)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null || identity.FindFirst("id").Value != cb.Idclient.ToString())
            {
                return Forbid();
            }

            var cartebancaire = new Cartebancaire
            {
                Idclient = cb.Idclient,
                Titulairecartebancaire = cb.Titulairecartebancaire,
                Nomcartebancaire = cb.Nomcartebancaire,
                Dateenregistement = cb.Dateenregistement,
                Numcartebancaire = cb.Numcartebancaire,
                Dateexpirationcarte = cb.Dateexpirationcarte
            };

            await dataRepository.AddCartebancaireAsync(cartebancaire);
            return Ok(cartebancaire);
        }

        /// <summary>
        /// Modifie une carte bancaire
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="idcartebancaire">L'ID de la carte à modifier</param>
        /// <param name="cartebancaire">La carte mise à jour</param>
        /// <response code="200">La carte à été modifiée</response>
        /// <response code="400">La carte n'est pas valide</response>
        /// <response code="401">Un des paramètres n'est pas rempi (JWT?)</response>
        /// <response code="403">Le JWT ne correspond pas</response>
        /// <response code="404">La carte n'est pas trouvée</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        // PUT: api/Cartebancaire/id
        [HttpPut("{idcartebancaire}")]
        [Authorize()]
        public async Task<ActionResult<Cartebancaire?>> PutCartebancaire(int idcartebancaire, [FromBody] CartebancaireDTO cartebancaire)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null || identity.FindFirst("id").Value != cartebancaire.Idclient.ToString())
            {
                return Forbid();
            }

            if (idcartebancaire != cartebancaire.Idcartebancaire)
            {
                return BadRequest("Les paramètres ne correspondent pas.");
            }

            var existingCartebancaire = await dataRepository.GetCartebancaireByIdAsync(idcartebancaire);
            if (existingCartebancaire.Value == null)
            {
                return NotFound();
            }

            Cartebancaire cb = await dataRepository.UpdateCartebancaireAsync(existingCartebancaire.Value, cartebancaire);
            return Ok(cb);
        }

        /// <summary>
        /// Supprime une carte bancaire
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="idcartebancaire">L'ID de la carte à supprimer</param>
        /// <response code="200">La carte à été supprimée</response>
        /// <response code="401">Un des paramètres n'est pas rempi (JWT?)</response>
        /// <response code="403">Le JWT ne correspond pas</response>
        /// <response code="404">La carte n'est pas trouvée</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        // DELETE: api/Cartebancaire/id
        [HttpDelete("{idcartebancaire}")]
        [Authorize()]
        public async Task<IActionResult> DeleteCartebancaire(int idcartebancaire)
        {
            var cartebancaire = await dataRepository.GetCartebancaireByIdAsync(idcartebancaire);
            if (cartebancaire.Value == null)
            {
                return NotFound();
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null || identity.FindFirst("id").Value != cartebancaire.Value.Idclient.ToString())
            {
                return Forbid();
            }

            await dataRepository.DeleteCartebancaireAsync(cartebancaire.Value);
            return Ok();
        }
    }
}
