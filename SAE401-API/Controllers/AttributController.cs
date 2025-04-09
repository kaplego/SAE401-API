using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttributController : ControllerBase
    {
        private readonly IAttributRepository<Attributproduit> dataRepository;

        public AttributController(IAttributRepository<Attributproduit> datarepo)
        {
            dataRepository = datarepo;
        }

        /// <summary>
        /// Récupère les attributs par type
        /// </summary>
        /// <returns>Http response</returns>
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetAllAttributByType")]
        public async Task<ActionResult<IEnumerable<Attributproduit>>> GetAllAttributByType(int id)
        {
            return await dataRepository.GetAllAttributByTypeAsync(id);

        }

    }
}
