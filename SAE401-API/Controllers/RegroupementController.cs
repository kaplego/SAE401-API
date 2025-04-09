using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegroupementController : ControllerBase
    {

        private readonly IRegroupementRepository<Regroupementproduit> dataRepository;

        public RegroupementController(IRegroupementRepository<Regroupementproduit> datarepo)
        {
            dataRepository = datarepo;
        }

        /// <summary>
        /// Obtiens les regroupements
        /// </summary>
        /// <returns>Http response</returns>
        [HttpGet]
        [Route("[action]")]
        [ActionName("GetAllRegroupement")]
        public async Task<ActionResult<IEnumerable<Regroupementproduit>>> GetAllRegroupement()
        {
            return await dataRepository.GetAllRegroupementAsync();
        }




    }
}
