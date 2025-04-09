using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VilleController : ControllerBase
    {
        private readonly IVilleRepository<Ville> dataRepository;

        public VilleController(IVilleRepository<Ville> datarepo)
        {
            dataRepository = datarepo;
        }

        /// <summary>
        /// Obtiens les villes
        /// </summary>
        /// <returns>Http response</returns>
        // GET: api/Ville/GetAllVille
        [HttpGet]
        [Route("[action]")]
        [ActionName("GetAllVille")]
        public async Task<ActionResult<IEnumerable<Ville>>> GetAllVille()
        {
            return await dataRepository.GetAllVilleAsync();
        }
    }
}
