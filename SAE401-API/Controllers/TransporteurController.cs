using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransporteurController : ControllerBase
    {

        private readonly ITransporteurRepository<Transporteur> dataRepository;

        public TransporteurController(ITransporteurRepository<Transporteur> datarepo)
        {
            dataRepository = datarepo;
        }

        [HttpGet]
        [Route("[action]")]
        [ActionName("GetAllTransporteur")]
        public async Task<ActionResult<IEnumerable<Transporteur>>> GetAllTransporteur()
        {
            return await dataRepository.GetAllTransporteurAsync();
        }
    


    }
}
