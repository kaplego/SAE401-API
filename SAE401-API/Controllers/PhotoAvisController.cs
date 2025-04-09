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

        // POST: api/PhotoAvis
        [HttpPost]
        [Authorize()]
        public async Task<ActionResult<Photoavi?>> PostPhotoAvis([FromBody] PhotoaviDTO paDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var avis = await dataRepository.GetAvisByIdAsync(paDTO.Idavis);
            if (avis == null) NotFound();

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null || identity.FindFirst("id").Value != avis.Value.Idclient.ToString())
            {
                return Forbid();
            }

            var pa = new Photoavi
            {
                Idavis = paDTO.Idavis,
                Idphoto = paDTO.Idphoto
            };


            await dataRepository.AddPhotoAvisAsync(pa);

            return Ok(pa);
        }
    }
}
