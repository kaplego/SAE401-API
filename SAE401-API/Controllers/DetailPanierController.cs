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
    public class DetailPanierController : ControllerBase
    {
        private readonly IDetailPanierRepository<Detailpanier> dataRepository;

        // Le constructeur doit uniquement accepter l'interface IDetailPanierRepository<Detailpanier>
        public DetailPanierController(IDetailPanierRepository<Detailpanier> datarepo)
        {
            dataRepository = datarepo;
        }

        /*
        [HttpGet("{idproduit}/{idcouleur}/{idclient}")]
        [Authorize()]
        public async Task<ActionResult<Detailpanier>> GetDetailPanierByIdAsync(int idproduit, int idcouleur, int idclient)
        {
            var detailpanier = await dataRepository.GetDetailPanierByIdAsync(idproduit, idcouleur, idclient);

            if (detailpanier.Value == null)
            {
                return NotFound();
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity; // #claims2#
            if (identity == null || identity.FindFirst("id").Value != detailpanier.Value.Idclient.ToString()) // #if#
            {
                return Forbid(); // #forbid#
            }

            return detailpanier;
        }
        */

        /// <summary>
        /// Modifie une relation DtPn
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="idproduit">L'IDproduit à modifier</param>
        /// <param name="idcouleur">L'IDcouleur à modifier</param>
        /// <param name="idclient">L'IDclient à modifier</param>
        /// <param name="detailpanier">La relation mise à jour</param>
        /// <response code="200">La relation à été modifiée</response>
        /// <response code="400">La relation n'est pas valide</response>
        /// <response code="401">Un des paramètres n'est pas rempi (JWT?)</response>
        /// <response code="403">Le JWT ne correspond pas</response>
        /// <response code="404">La relation n'est pas trouvée</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [HttpPut("{idproduit}/{idcouleur}/{idclient}")]
        [Authorize()]
        public async Task<ActionResult<Detailpanier?>> PutDetailPanier(int idproduit, int idcouleur, int idclient, DetailpanierDTO detailpanier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Vérification de l'intégrité des paramètres
            if (idproduit != detailpanier.Idproduit || idcouleur != detailpanier.Idcouleur || idclient != detailpanier.Idclient)
            {
                return BadRequest("Les paramètres ne correspondent pas.");
            }

            // Récupérer l'entité existante
            var produitToUpdate = await dataRepository.GetDetailPanierByIdAsync(idproduit, idcouleur, idclient);

            if (produitToUpdate.Value == null)
            {
                return NotFound();
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity; // #claims2#
            if (identity == null || identity.FindFirst("id").Value != detailpanier.Idclient.ToString()) // #if#
            {
                return Forbid(); // #forbid#
            }

            // Appeler la méthode de mise à jour dans le repository
            Detailpanier dp = await dataRepository.UpdateDetailPanierAsync(produitToUpdate.Value, detailpanier);

            return Ok(dp);
        }


        /// <summary>
        /// Créé une relation DtPn
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="detailpanier">La relation à ajouter</param>
        /// <response code="200">La relation à été créée</response>
        /// <response code="400">La relation n'est pas valide</response>
        /// <response code="401">Un des paramètres n'est pas rempi (JWT?)</response>
        /// <response code="403">Le JWT ne correspond pas</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [HttpPost]
        [Authorize()]
        public async Task<ActionResult<Detailpanier?>> PostDetailPanier(DetailpanierDTO detailpanier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var detailpanierfinal = new Detailpanier
            {
                Idproduit = detailpanier.Idproduit,
                Idcouleur = detailpanier.Idcouleur,
                Idclient = detailpanier.Idclient,
                Quantitepanier = detailpanier.Quantitepanier
            };

            var identity = HttpContext.User.Identity as ClaimsIdentity; // #claims2#
            if (identity == null || identity.FindFirst("id").Value != detailpanier.Idclient.ToString()) // #if#
            {
                return Forbid(); // #forbid#
            }

            await dataRepository.AddDetailPanierAsync(detailpanierfinal);

            return Ok(detailpanierfinal);
        }


        /// <summary>
        /// Supprime une relation DtPn
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="idproduit">L'IDproduit à supprimer</param>
        /// <param name="idcouleur">L'IDcouleur à supprimer</param>
        /// <param name="idclient">L'IDclient à supprimer</param>
        /// <response code="200">La relation à été supprimée</response>
        /// <response code="401">Un des paramètres n'est pas rempi (JWT?)</response>
        /// <response code="403">Le JWT ne correspond pas</response>
        /// <response code="404">La relation n'est pas trouvée</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [HttpDelete("{idproduit}/{idcouleur}/{idclient}")]
        [Authorize()]
        public async Task<IActionResult> DeleteDetailPanier(int idproduit, int idcouleur, int idclient)
        {
            var detailpanier = await dataRepository.GetDetailPanierByIdAsync(idproduit, idcouleur, idclient);
            if (detailpanier.Value == null)
            {
                return NotFound();
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity; // #claims2#
            if (identity == null || identity.FindFirst("id").Value != detailpanier.Value.Idclient.ToString()) // #if#
            {
                return Forbid(); // #forbid#
            }

            await dataRepository.DeleteDetailPanierAsync(detailpanier.Value);
            return Ok();
        }
    }
}
