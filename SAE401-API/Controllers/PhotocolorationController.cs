using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

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

        /// <summary>
        /// Créé une relation PhCo
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="photocoloration">La relation à ajouter</param>
        /// <response code="200">La relation à été créée</response>
        /// <response code="400">La relation n'est pas valide</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        [HttpPost]
        public async Task<ActionResult<Photocoloration?>> PostPhotocoloration([FromBody] PhotocolorationDTO photocoloration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pc = new Photocoloration
            {
                Idproduit = photocoloration.Idproduit,
                Idcouleur = photocoloration.Idcouleur,
                Idphoto = photocoloration.Idphoto
            };

            await dataRepository.AddPhotocolorationAsync(pc);
            return Ok(pc);
        }
    }
}
