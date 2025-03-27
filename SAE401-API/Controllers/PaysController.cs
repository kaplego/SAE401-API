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
    public class PaysController : ControllerBase
    {
        private readonly IPaysRepository<Pay> dataRepository;

        public PaysController(IPaysRepository<Pay> datarepo)
        {
            dataRepository = datarepo;
        }

        // GET: api/Pays/GetAllPays
        [HttpGet]
        [Route("[action]")]
        [ActionName("GetAllPays")]
        public async Task<ActionResult<IEnumerable<Pay>>> GetAllPays()
        {
            return await dataRepository.GetAllPaysAsync();
        }
    }
}
