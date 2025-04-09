using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartementController : ControllerBase
    {
        private readonly IDepartementRepository<Departement> dataRepository;

        public DepartementController(IDepartementRepository<Departement> datarepo)
        {
            dataRepository = datarepo;
        }

        // GET: api/Departement/GetAllDepartement
        [HttpGet]
        [Route("[action]")]
        [ActionName("GetAllDepartement")]
        public async Task<ActionResult<IEnumerable<Departement>>> GetAllDepartement()
        {
            return await dataRepository.GetAllDepartementAsync();
        }
    }
}
