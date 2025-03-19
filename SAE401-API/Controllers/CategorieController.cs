using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorieController : ControllerBase
    {

        private readonly ICategorieRepository<Categorieproduit> dataRepository;

        public CategorieController(ICategorieRepository<Categorieproduit> datarepo)
        {
            dataRepository = datarepo;
        }

        [HttpGet]
        [Route("[action]")]
        [ActionName("GetAllCategorie")]
        public async Task<ActionResult<IEnumerable<Categorieproduit>>> GetAllCategorie()
        {
            return await dataRepository.GetAllCategorieAsync();
        }




    }
}
