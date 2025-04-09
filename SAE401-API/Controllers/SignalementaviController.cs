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

        [HttpPost]
        public async Task<ActionResult<Signalementavi?>> PostSignalementavi([FromBody] SignalementaviDTO signalementaviDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var signalementavi = new Signalementavi
            {
                Idavis = signalementaviDTO.Idavis,
                Idtypesignalement = signalementaviDTO.Idtypesignalement,
                Emailsignalement = signalementaviDTO.Emailsignalement,
                Datesignalement = signalementaviDTO.Datesignalement,
                Contenusignalement = signalementaviDTO.Contenusignalement
            };

            await dataRepository.AddSignalementaviAsync(signalementavi);
            return Ok(signalementavi);
        }
    }
}
