using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoRepository<Photo> dataRepository;

        public PhotoController(IPhotoRepository<Photo> datarepo)
        {
            dataRepository = datarepo;
        }

        /// <summary>
        /// Créé une photo
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="photo">La photo à ajouter</param>
        /// <response code="200">La photo à été créée</response>
        /// <response code="400">La photo n'est pas valide</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        // POST: api/Photo
        [HttpPost]
        public async Task<ActionResult<Photo?>> PostPhoto(PhotoDTO photo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newphoto = new Photo
            {
                Sourcephoto = photo.Sourcephoto,
                Descriptionphoto = photo.Descriptionphoto
            };


            await dataRepository.AddPhotoAsync(newphoto);

            return Ok(newphoto);
        }

        /// <summary>
        /// Supprime une photo
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="idphoto">L'IDphoto à supprimer</param>
        /// <response code="200">La photo à été supprimée</response>
        /// <response code="404">La photo n'est pas trouvée</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        // DELETE: api/Photo/id
        [HttpDelete("{idphoto}")]
        public async Task<IActionResult> DeletePhoto(int idphoto)
        {


            var photo = await dataRepository.GetPhotoByIdAsync(idphoto);

            if (photo.Value == null)
            {
                return NotFound();
            }

            await dataRepository.DeletePhotoAsync(photo.Value);
            return Ok();
        }
    }
}
