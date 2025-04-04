using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.DataManager;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;
using System.Security.Claims;

namespace SAE401_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailcommandeController : ControllerBase
    {
        private readonly IDetailcommandeRepository<Detailcommande> dataRepository;

        public DetailcommandeController(IDetailcommandeRepository<Detailcommande> datarepo)
        {
            dataRepository = datarepo;
        }

        [HttpPost]
        public async Task<ActionResult<Detailcommande?>> PostDetailcommande([FromBody] DetailcommandeDTO detailcommandeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var detailcommande = new Detailcommande
            {
                Idproduit = detailcommandeDTO.Idproduit,
                Idcouleur = detailcommandeDTO.Idcouleur,
                Idcommande = detailcommandeDTO.Idcommande,
                Quantitecommande = detailcommandeDTO.Quantitecommande
            };


            await dataRepository.AddDetailcommandeAsync(detailcommande);
            return Ok(detailcommande);
        }
    }
}
