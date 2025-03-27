using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Models.DataManager;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;
using System.Security.Claims;

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
        public async Task<ActionResult<Photo>> PostPhoto(PhotoDTO photoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var photo = new Photo
            {
                Idphoto = photoDTO.Idphoto,
                Sourcephoto = photoDTO.Sourcephoto,
                Descriptionphoto = photoDTO.Descriptionphoto
            };


            await dataRepository.AddPhotoAsync(photo);

            return NoContent();
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
            return NoContent();
        }
    }
}
