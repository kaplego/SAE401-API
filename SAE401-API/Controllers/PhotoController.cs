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

        // POST: api/Photo
        [HttpPost]
        public async Task<ActionResult<Photo?>> PostPhoto(PhotoDTO photoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var photo = new Photo
            {
                Sourcephoto = photoDTO.Sourcephoto,
                Descriptionphoto = photoDTO.Descriptionphoto
            };


            await dataRepository.AddPhotoAsync(photo);

            return Ok(photo);
        }

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
