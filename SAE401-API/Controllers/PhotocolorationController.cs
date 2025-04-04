using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.DataManager;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;
using System.Security.Claims;

namespace SAE401_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotocolorationController : ControllerBase
    {
        private readonly IPhotocolorationRepository<Photocoloration> dataRepository;

        public PhotocolorationController(IPhotocolorationRepository<Photocoloration> datarepo)
        {
            dataRepository = datarepo;
        }

        [HttpPost]
        public async Task<ActionResult<Photocoloration?>> PostPhotocoloration([FromBody] PhotocolorationDTO photocolorationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var photocoloration = new Photocoloration
            {
                Idproduit = photocolorationDTO.Idproduit,
                Idcouleur = photocolorationDTO.Idcouleur,
                Idphoto = photocolorationDTO.Idphoto
            };

            await dataRepository.AddPhotocolorationAsync(photocoloration);
            return Ok(photocoloration);
        }
    }
}
