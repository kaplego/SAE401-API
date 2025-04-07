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


        // GET: api/Cartebancaire/GetAllCartebancaireByClient/idclient
        [HttpGet("[action]/{idclient}")]
        [Authorize()]
        public async Task<ActionResult<IEnumerable<Cartebancaire>>> GetAllCartebancaireByClient(int idclient)
        {
            var client = await dataRepository.GetClientByIdAsync(idclient);
            if (client.Value == null)
            {
                return NotFound();
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null || identity.FindFirst("id").Value != idclient.ToString())
            {
                return Forbid();
            }

           return await dataRepository.GetAllCartebancaireByClientAsync(idclient); 
        }
        

        // POST: api/Cartebancaire
        [HttpPost]
        [Authorize()]
        public async Task<ActionResult<Cartebancaire?>> PostCartebancaire([FromBody] CartebancaireDTO cartebancaireDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null || identity.FindFirst("id").Value != cartebancaireDTO.Idclient.ToString())
            {
                return Forbid();
            }

            var cartebancaire = new Cartebancaire
            {
                Idclient = cartebancaireDTO.Idclient,
                Titulairecartebancaire = cartebancaireDTO.Titulairecartebancaire,
                Nomcartebancaire = cartebancaireDTO.Nomcartebancaire,
                Dateenregistement = cartebancaireDTO.Dateenregistement,
                Numcartebancaire = cartebancaireDTO.Numcartebancaire,
                Dateexpirationcarte = cartebancaireDTO.Dateexpirationcarte
            };

            Cartebancaire cb = await dataRepository.AddCartebancaireAsync(cartebancaire);
            return Ok(cb);
        }

        // PUT: api/Cartebancaire/id
        [HttpPut("{idcartebancaire}")]
        [Authorize()]
        public async Task<ActionResult<Cartebancaire?>> PutCartebancaire(int idcartebancaire, [FromBody] CartebancaireDTO cartebancaireDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null || identity.FindFirst("id").Value != cartebancaireDTO.Idclient.ToString())
            {
                return Forbid();
            }

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
                Idclient = cartebancaireDTO.Idclient,
                Titulairecartebancaire = cartebancaireDTO.Titulairecartebancaire,
                Nomcartebancaire = cartebancaireDTO.Nomcartebancaire,
                Dateenregistement = cartebancaireDTO.Dateenregistement,
                Numcartebancaire = cartebancaireDTO.Numcartebancaire,
                Dateexpirationcarte = cartebancaireDTO.Dateexpirationcarte
            };

            Cartebancaire cb = await dataRepository.UpdateCartebancaireAsync(existingCartebancaire.Value, updatedCartebancaire);
            return Ok(cb);
        }

        // DELETE: api/Cartebancaire/id
        [HttpDelete("{idcartebancaire}")]
        [Authorize()]
        public async Task<IActionResult> DeleteCartebancaire(int idcartebancaire)
        {
            var cartebancaire = await dataRepository.GetCartebancaireByIdAsync(idcartebancaire);
            if (cartebancaire.Value == null)
            {
                return NotFound();
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null || identity.FindFirst("id").Value != cartebancaire.Value.Idclient.ToString())
            {
                return Forbid();
            }

            await dataRepository.DeleteCartebancaireAsync(cartebancaire.Value);
            return Ok();
        }
    }
}
