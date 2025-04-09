using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using SAE401_API.Models;
using SAE401_API.Models.DataManager;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

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

        // POST: api/Professionel
        [HttpPost]
        [Authorize()]
        public async Task<ActionResult<Professionel?>> PostProfessionel([FromBody] ProfessionelDTO proDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null || identity.FindFirst("id").Value != proDTO.Idclient.ToString())
            {
                return Forbid();
            }

            Professionel newpro = new Professionel()
            {
                Idclient = proDTO.Idclient,
                Idactivitepro = proDTO.Idactivitepro,
                Nomsociete = proDTO.Nomsociete,
                Numtva = proDTO.Numtva
            };

            await dataRepository.AddProfessionelAsync(newpro);

            return Ok(newpro);
        }

        // PUT: api/Professionel/{id}
        [HttpPut("{id}")]
        [Authorize()]
        public async Task<ActionResult<Professionel?>> PutProfessionel(int id, ProfessionelDTO pro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            if (id != pro.Idclient)
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
                await dataRepository.UpdateProfessionelAsync(proToUpdate.Value, pro);
                return Ok(proToUpdate.Value);
            }
        }
    }
}
