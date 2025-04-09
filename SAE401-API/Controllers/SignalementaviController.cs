using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignalementaviController : ControllerBase
    {
        private readonly ISignalementaviRepository<Signalementavi> dataRepository;

        public SignalementaviController(ISignalementaviRepository<Signalementavi> datarepo)
        {
            dataRepository = datarepo;
        }

        /// <summary>
        /// Créé un signalement
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="signalement">Le signalement à ajouter</param>
        /// <response code="200">Le signalement à été créé</response>
        /// <response code="400">Le signalement n'est pas valide</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<ActionResult<Signalementavi?>> PostSignalementavi([FromBody] SignalementaviDTO signalement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var signalementavi = new Signalementavi
            {
                Idavis = signalement.Idavis,
                Idtypesignalement = signalement.Idtypesignalement,
                Emailsignalement = signalement.Emailsignalement,
                Datesignalement = signalement.Datesignalement,
                Contenusignalement = signalement.Contenusignalement
            };

            await dataRepository.AddSignalementaviAsync(signalementavi);
            return Ok(signalementavi);
        }
    }
}
