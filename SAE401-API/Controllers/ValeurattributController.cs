using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValeurattributController : ControllerBase
    {
        private readonly IValeurattributRepository<Valeurattribut> dataRepository;


        public ValeurattributController(IValeurattributRepository<Valeurattribut> datarepo)
        {
            dataRepository = datarepo;
        }



        [HttpPost]
        public async Task<ActionResult<Valeurattribut?>> PostValeurattribut([FromBody] ValeurattributDTO valeurAttributDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var valeurAttribut = new Valeurattribut
            {
                Idattribut = valeurAttributDTO.Idattribut,
                Idproduit = valeurAttributDTO.Idproduit,
                Valeur = valeurAttributDTO.Valeur
            };

            await dataRepository.AddValeurattributAsync(valeurAttribut);
            return Ok(valeurAttribut);
        }

        [HttpPut("{idattribut}/{idproduit}")]
        public async Task<ActionResult<Valeurattribut?>> PutValeurattribut(int idattribut, int idproduit, [FromBody] ValeurattributDTO valeurAttributDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (idattribut != valeurAttributDTO.Idattribut || idproduit != valeurAttributDTO.Idproduit)
            {
                return BadRequest("Les paramètres ne correspondent pas.");
            }

            var existingValeurattribut = await dataRepository.GetValeurattributByIdAsync(idattribut, idproduit);
            if (existingValeurattribut.Value == null)
            {
                return NotFound();
            }

            Valeurattribut va = await dataRepository.UpdateValeurattributAsync(existingValeurattribut.Value, valeurAttributDTO);
            return Ok(va);
        }

        [HttpDelete("{idattribut}/{idproduit}")]
        public async Task<IActionResult> DeleteValeurattribut(int idattribut, int idproduit)
        {
            var valeurAttribut = await dataRepository.GetValeurattributByIdAsync(idattribut, idproduit);
            if (valeurAttribut.Value == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteValeurattributAsync(valeurAttribut.Value);
            return Ok();
        }
    }
}
