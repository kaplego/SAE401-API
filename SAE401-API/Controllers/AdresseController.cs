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
    public class AdresseController : ControllerBase
    {
        private readonly IAdresseRepository<Adresse> dataRepository;

        public AdresseController(IAdresseRepository<Adresse> datarepo)
        {
            dataRepository = datarepo;
        }

        /*
        // GET: api/Adresse/5
        [HttpGet("{idadresse}")]
        [Authorize()]
        public async Task<ActionResult<AdresseDTO>> GetAdresseByIdAsync(int idadresse)
        {
            var adresse = await dataRepository.GetAdresseByIdAsync(idadresse);

            if (adresse.Value == null)
            {
                return NotFound();
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity; // #claims2#
            if (identity == null || identity.FindFirst("id").Value != adresse.Value.Idclient.ToString()) // #if#
            {
                return Forbid(); // #forbid#
            }

            return adresseDTO;
        }
        */

        // Ajouter une nouvelle adresse
        [HttpPost]
        [Authorize()]
        public async Task<ActionResult<Adresse?>> PostAdresse(AdresseDTO adresseDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity; // #claims2#
            if (identity == null || identity.FindFirst("id").Value != adresseDTO.Idclient.ToString()) // #if#
            {
                return Forbid(); // #forbid#
            }

            var adresse = new Adresse
            {
                Idpays = adresseDTO.Idpays,
                Codeinsee = adresseDTO.Codeinsee,
                Idclient = adresseDTO.Idclient,
                Iddepartement = adresseDTO.Iddepartement,
                Nomadresse = adresseDTO.Nomadresse,
                Numerorue = adresseDTO.Numerorue,
                Nomrue = adresseDTO.Nomrue,
                Codepostaladresse = adresseDTO.Codepostaladresse
            };

            await dataRepository.AddAdresseAsync(adresse);
            return Ok(adresse);
        }

        // Mettre à jour une adresse existante
        [HttpPut("{idadresse}")]
        [Authorize()]

        public async Task<ActionResult<Adresse?>> PutAdresse(int idadresse, [FromBody] AdresseDTO adresseDTO)
        {
            if (idadresse != adresseDTO.Idadresse)
            {
                return BadRequest("Les paramètres ne correspondent pas.");
            }

            var existingAdresse = await dataRepository.GetAdresseByIdAsync(idadresse);
            if (existingAdresse.Value == null)
            {
                return NotFound();
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity; // #claims2#
            if (identity == null || identity.FindFirst("id").Value != existingAdresse.Value.Idclient.ToString()) // #if#
            {
                return Forbid(); // #forbid#
            }

            await dataRepository.UpdateAdresseAsync(existingAdresse.Value, adresseDTO);

            return Ok(existingAdresse.Value);
        }

        // Supprimer une adresse
        [HttpDelete("{idadresse}")]
        [Authorize()]

        public async Task<IActionResult> DeleteAdresse(int idadresse)
        {
            var adresse = await dataRepository.GetAdresseByIdAsync(idadresse);
            if (adresse.Value == null)
            {
                return NotFound();
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity; // #claims2#
            if (identity == null || identity.FindFirst("id").Value != adresse.Value.Idclient.ToString()) // #if#
            {
                return Forbid(); // #forbid#
            }

            await dataRepository.DeleteAdresseAsync(adresse.Value);
            return Ok();
        }
    }
}
