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

        // Le constructeur doit accepter l'interface IAdresseRepository<Adresse>
        public AdresseController(IAdresseRepository<Adresse> datarepo)
        {
            dataRepository = datarepo;
        }

        // GET: api/Adresse/5
        [HttpGet("{idadresse}")]
        [Authorize()]
        public async Task<ActionResult<Adresse>> GetAdresseByIdAsync(int idadresse)
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

            return adresse;
        }

        // PUT: api/Adresse/5
        [HttpPut("{idadresse}")]
        [Authorize()]
        public async Task<IActionResult> PutAdresse(int idadresse, AdresseDTO adresseDTO)
        {
            // Vérification de l'intégrité des paramètres
            if (idadresse != adresseDTO.Idadresse)
            {
                return BadRequest("L'Id dans les paramètres ne correspond pas à celui du DTO.");
            }

            // Récupérer l'entité existante
            var adresseToUpdate = await dataRepository.GetAdresseByIdAsync(idadresse);

            if (adresseToUpdate.Value == null)
            {
                return NotFound();
            }

            // Convertir le DTO en une instance d'Adresse
            var updatedAdresse = new Adresse
            {
                Idadresse = adresseDTO.Idadresse,
                Nomadresse = adresseDTO.Nomadresse,
                Nomrue = adresseDTO.Nomrue,
                Codepostaladresse = adresseDTO.Codepostaladresse,
                // Ajouter d'autres propriétés à mettre à jour
            };


            var identity = HttpContext.User.Identity as ClaimsIdentity; // #claims2#
            if (identity == null || identity.FindFirst("id").Value != adresseToUpdate.Value.Idclient.ToString()) // #if#
            {
                return Forbid(); // #forbid#
            }

            // Appeler la méthode de mise à jour dans le repository
            await dataRepository.UpdateAdresseAsync(adresseToUpdate.Value, updatedAdresse);

            return NoContent();
        }

        // POST: api/Adresse
        [HttpPost]
        [Authorize()]
        public async Task<ActionResult<Adresse>> PostAdresse(AdresseDTO adresseDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var adresse = new Adresse
            {
                Nomadresse = adresseDTO.Nomadresse,
                Nomrue = adresseDTO.Nomrue,
                Codepostaladresse = adresseDTO.Codepostaladresse,
                // Ajoutez d'autres propriétés à partir du DTO
            };


            var identity = HttpContext.User.Identity as ClaimsIdentity; // #claims2#
            if (identity == null || identity.FindFirst("id").Value != adresse.Idclient.ToString()) // #if#
            {
                return Forbid(); // #forbid#
            }

            await dataRepository.AddAdresseAsync(adresse);

            return CreatedAtAction(nameof(GetAdresseByIdAsync), new { idadresse = adresse.Idadresse }, adresse);
        }

        // DELETE: api/Adresse/5
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


            return NoContent();
        }
    }
}
