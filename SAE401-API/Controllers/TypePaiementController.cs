using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypePaiementController : ControllerBase
    {

        private readonly ITypePaiementRepository<Typepaiement> dataRepository;

        public TypePaiementController(ITypePaiementRepository<Typepaiement> datarepo)
        {
            dataRepository = datarepo;
        }

        /// <summary>
        /// Obtiens les types de paiement
        /// </summary>
        /// <returns>Http response</returns>
        [HttpGet]
        [Route("[action]")]
        [ActionName("GetAllTypePaiement")]
        public async Task<ActionResult<IEnumerable<Typepaiement>>> GetAllTypePaiement()
        {
            return await dataRepository.GetAllTypePaiementAsync();
        }



    }
}
