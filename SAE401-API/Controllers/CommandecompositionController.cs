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
    public class CommandecompositionController : ControllerBase
    {
        private readonly ICommandecompositionRepository<Commandecomposition> dataRepository;

        public CommandecompositionController(ICommandecompositionRepository<Commandecomposition> datarepo)
        {
            dataRepository = datarepo;
        }

        [HttpPost]
        public async Task<ActionResult> PostCommandecomposition([FromBody] CommandecompositionDTO commandecompositionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var commandecomposition = new Commandecomposition
            {
                Idcomposition = commandecompositionDTO.Idcomposition,
                Idcommande = commandecompositionDTO.Idcommande,
                Quantitecompositioncommande = commandecompositionDTO.Quantitecompositioncommande
            };

         

            await dataRepository.AddCommandecompositionAsync(commandecomposition);
            return NoContent();
        }
    }
}
