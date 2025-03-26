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
    public class CartebancaireController : ControllerBase
    {
        private readonly ICartebancaireRepository<Cartebancaire> dataRepository;

        public CartebancaireController(ICartebancaireRepository<Cartebancaire> datarepo)
        {
            dataRepository = datarepo;
        }

        [HttpGet("{idcartebancaire}")]
        [Authorize()]

        public async Task<ActionResult<CartebancaireDTO>> GetCartebancaireByIdAsync(int idcartebancaire)
        {
            var cartebancaire = await dataRepository.GetCartebancaireByIdAsync(idcartebancaire);

            if (cartebancaire.Value == null)
            {
                return NotFound();
            }

            var cartebancaireDTO = new CartebancaireDTO
            {
                Idcartebancaire = cartebancaire.Value.Idcartebancaire,
                Idclient = cartebancaire.Value.Idclient,
                Nomcartebancaire = cartebancaire.Value.Nomcartebancaire,
                Dateenregistement = cartebancaire.Value.Dateenregistement,
                Numcartebancaire = cartebancaire.Value.Numcartebancaire,
                Dateexpirationcarte = cartebancaire.Value.Dateexpirationcarte
            };

            var identity = HttpContext.User.Identity as ClaimsIdentity; // #claims2#
            if (identity == null || identity.FindFirst("id").Value != cartebancaire.Value.Idclient.ToString()) // #if#
            {
                return Forbid(); // #forbid#
            }

            return cartebancaireDTO;
        }

        [HttpPost]
        [Authorize()]

        public async Task<ActionResult> PostCartebancaire([FromBody] CartebancaireDTO cartebancaireDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cartebancaire = new Cartebancaire
            {
                Idclient = cartebancaireDTO.Idclient,
                Nomcartebancaire = cartebancaireDTO.Nomcartebancaire,
                Dateenregistement = cartebancaireDTO.Dateenregistement,
                Numcartebancaire = cartebancaireDTO.Numcartebancaire,
                Dateexpirationcarte = cartebancaireDTO.Dateexpirationcarte
            };

            var identity = HttpContext.User.Identity as ClaimsIdentity; // #claims2#
            if (identity == null || identity.FindFirst("id").Value != cartebancaire.Idclient.ToString()) // #if#
            {
                return Forbid(); // #forbid#
            }

            await dataRepository.AddCartebancaireAsync(cartebancaire);
            return NoContent();
        }

        [HttpPut("{idcartebancaire}")]
        [Authorize()]
        public async Task<IActionResult> PutCartebancaire(int idcartebancaire, [FromBody] CartebancaireDTO cartebancaireDTO)
        {
            if (idcartebancaire != cartebancaireDTO.Idcartebancaire)
            {
                return BadRequest("Les paramètres ne correspondent pas.");
            }

            var existingCartebancaire = await dataRepository.GetCartebancaireByIdAsync(idcartebancaire);
            if (existingCartebancaire.Value == null)
            {
                return NotFound();
            }

            var updatedCartebancaire = new Cartebancaire
            {
                Idcartebancaire = cartebancaireDTO.Idcartebancaire,
                Idclient = cartebancaireDTO.Idclient,
                Nomcartebancaire = cartebancaireDTO.Nomcartebancaire,
                Dateenregistement = cartebancaireDTO.Dateenregistement,
                Numcartebancaire = cartebancaireDTO.Numcartebancaire,
                Dateexpirationcarte = cartebancaireDTO.Dateexpirationcarte
            };

            var identity = HttpContext.User.Identity as ClaimsIdentity; // #claims2#
            if (identity == null || identity.FindFirst("id").Value != updatedCartebancaire.Idclient.ToString()) // #if#
            {
                return Forbid(); // #forbid#
            }

            await dataRepository.UpdateCartebancaireAsync(existingCartebancaire.Value, updatedCartebancaire);
            return NoContent();
        }

        [HttpDelete("{idcartebancaire}")]
        [Authorize()]
        public async Task<IActionResult> DeleteCartebancaire(int idcartebancaire)
        {
            var cartebancaire = await dataRepository.GetCartebancaireByIdAsync(idcartebancaire);
            if (cartebancaire.Value == null)
            {
                return NotFound();
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity; // #claims2#
            if (identity == null || identity.FindFirst("id").Value != cartebancaire.Value.Idclient.ToString()) // #if#
            {
                return Forbid(); // #forbid#
            }

            await dataRepository.DeleteCartebancaireAsync(cartebancaire.Value);
            return NoContent();
        }
    }
}
