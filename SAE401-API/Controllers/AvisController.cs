using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Models.DataManager;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;
using System.Security.Claims;

namespace SAE401_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvisController : ControllerBase
    {
        private readonly IAvisRepository<Avisproduit> dataRepository;

        public AvisController(IAvisRepository<Avisproduit> datarepo)
        {
            dataRepository = datarepo;
        }

        // POST: api/Avis
        [HttpPost]
        [Authorize()]
        public async Task<ActionResult<Avisproduit?>> PostAvis(AvisproduitDTO avisDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null || identity.FindFirst("id").Value != avisDTO.Idclient.ToString())
            {
                return Forbid();
            }

            var avis = new Avisproduit
            {
                Idproduit = avisDTO.Idproduit,
                Idclient = avisDTO.Idclient,
                Noteavis = avisDTO.Noteavis,
                Dateavis = avisDTO.Dateavis,
                Commentaireavis = avisDTO.Commentaireavis,
                Reponsemiliboo = avisDTO.Reponsemiliboo
            };


            await dataRepository.AddAvisAsync(avis);

            return Ok(avis);
        }

        // DELETE: api/Avis/5
        [HttpDelete("{idavis}")]
        [Authorize()]
        public async Task<IActionResult> DeleteAvis(int idavis)
        {
            

            var avis = await dataRepository.GetAvisByIdAsync(idavis);

            if (avis.Value == null)
            {
                return NotFound();
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null || identity.FindFirst("id").Value != avis.Value.Idclient.ToString())
            {
                return Forbid();
            }


            await dataRepository.DeleteAvisAsync(avis.Value);
            return Ok();
        }
    }
}
