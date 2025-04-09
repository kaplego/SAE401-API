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
    public class PhotoAvisController : ControllerBase
    {
        private readonly IPhotoAvisRepository<Photoavi> dataRepository;

        public PhotoAvisController(IPhotoAvisRepository<Photoavi> datarepo)
        {
            dataRepository = datarepo;
        }

        /// <summary>
        /// Créé une relation PhAv
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="photoavis">La relation à ajouter</param>
        /// <response code="200">La relation à été créée</response>
        /// <response code="400">La relation n'est pas valide</response>
        /// <response code="401">Un des paramètres n'est pas rempi (JWT?)</response>
        /// <response code="403">Le JWT ne correspond pas</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        // POST: api/PhotoAvis
        [HttpPost]
        [Authorize()]
        public async Task<ActionResult<Photoavi?>> PostPhotoAvis([FromBody] PhotoaviDTO photoavis)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var avis = await dataRepository.GetAvisByIdAsync(photoavis.Idavis);
            if (avis == null) NotFound();

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null || identity.FindFirst("id").Value != avis.Value.Idclient.ToString())
            {
                return Forbid();
            }

            var pa = new Photoavi
            {
                Idavis = photoavis.Idavis,
                Idphoto = photoavis.Idphoto
            };


            await dataRepository.AddPhotoAvisAsync(pa);

            return Ok(pa);
        }
    }
}
