using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;
using System.Security.Claims;

namespace SAE401_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessionelController : ControllerBase
    {
        private readonly IProfessionelRepository<Professionel> dataRepository;

        public ProfessionelController(IProfessionelRepository<Professionel> datarepo)
        {
            dataRepository = datarepo;
        }

        /// <summary>
        /// Créé un professionel
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="professionel">Le professionel à ajouter</param>
        /// <response code="200">Le professionel à été créé</response>
        /// <response code="400">Le professionel n'est pas valide</response>
        /// <response code="401">Un des paramètres n'est pas rempi (JWT?)</response>
        /// <response code="403">Le JWT ne correspond pas</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        // POST: api/Professionel
        [HttpPost]
        [Authorize()]
        public async Task<ActionResult<Professionel?>> PostProfessionel([FromBody] ProfessionelDTO professionel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null || identity.FindFirst("id").Value != professionel.Idclient.ToString())
            {
                return Forbid();
            }

            Professionel newpro = new Professionel()
            {
                Idclient = professionel.Idclient,
                Idactivitepro = professionel.Idactivitepro,
                Nomsociete = professionel.Nomsociete,
                Numtva = professionel.Numtva
            };

            await dataRepository.AddProfessionelAsync(newpro);

            return Ok(newpro);
        }

        /// <summary>
        /// Modifie un professionel
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">L'IDclient à modifier</param>
        /// <param name="professionel">Le professionel mis à jour</param>
        /// <response code="200">Le professionel à été modifié</response>
        /// <response code="400">Le professionel n'est pas valide</response>
        /// <response code="401">Un des paramètres n'est pas rempi (JWT?)</response>
        /// <response code="403">Le JWT ne correspond pas</response>
        /// <response code="404">Le professionel n'est pas trouvé</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        // PUT: api/Professionel/{id}
        [HttpPut("{id}")]
        [Authorize()]
        public async Task<ActionResult<Professionel?>> PutProfessionel(int id, ProfessionelDTO professionel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (id != professionel.Idclient)
            {
                return BadRequest();
            }

            var proToUpdate = await dataRepository.GetProfessionelByIdAsync(id);

            if (proToUpdate.Value == null)
            {
                return NotFound();
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null || identity.FindFirst("id").Value != id.ToString())
            {
                return Forbid();
            }


            else
            {
                Professionel newpro = await dataRepository.UpdateProfessionelAsync(proToUpdate.Value, professionel);
                return Ok(newpro);
            }
        }
    }
}
