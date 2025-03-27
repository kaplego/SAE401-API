using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Models.DataManager;
using SAE401_API.Models.DTO;
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
