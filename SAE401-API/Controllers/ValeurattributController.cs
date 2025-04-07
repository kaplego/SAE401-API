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

            
            var updatedValeurattribut = new Valeurattribut
            {
                Idattribut = valeurAttributDTO.Idattribut,
                Idproduit = valeurAttributDTO.Idproduit,
                Valeur = valeurAttributDTO.Valeur
            };

            await dataRepository.UpdateValeurattributAsync(existingValeurattribut.Value, updatedValeurattribut);
            return Ok(existingValeurattribut.Value);
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
