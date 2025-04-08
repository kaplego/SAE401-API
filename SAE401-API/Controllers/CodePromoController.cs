using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodePromoController : ControllerBase
    {

        private readonly ICodePromoRepository<Codepromo> dataRepository;

        public CodePromoController(ICodePromoRepository<Codepromo> datarepo)
        {
            dataRepository = datarepo;
        }

        [HttpGet]
        [Route("[action]")]
        [ActionName("GetAllCodePromo")]
        public async Task<ActionResult<IEnumerable<Codepromo>>> GetAllCodePromo()
        {
            return await dataRepository.GetAllCodePromoAsync();
        }
    
    }
}
