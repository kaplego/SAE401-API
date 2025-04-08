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
using SAE401_API.Models.DataMethods;

namespace SAE401_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository<Client> dataRepository;

        public ClientController(IClientRepository<Client> datarepo)
        {
            dataRepository = datarepo;
        }
        

        public class Login {
            public string email { get; set; }
            public string password { get; set; }
            public Login() { }
        }

        //POST: api/Client/GetClientByLogin
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        [ActionName("GetClientByLogin")]
        public async Task<ActionResult> GetClientByLogin(Login login)
        {
            ActionResult response = Forbid();
            ActionResult<Client> client = await dataRepository.GetClientByLoginAsync(login.email, login.password);
            if (client.Value != null)
            {
                var tokenString = JwtManager.GenerateJwtToken(client.Value);
                response = Ok(new
                {
                    token = tokenString,
                    client = client.Value,
                });
            }
            return response;

        }

        // GET: api/Client/GetClientById/{id}
        [HttpGet]
        [Authorize()] // #Authorize#
        [Route("[action]/{id}")]
        [ActionName("GetClientById")]

        public async Task<ActionResult<Client>> GetClientById(int id)
        {
            /* #JWT#: Tuto JWT (utilisez CRTL+F pour trouver les sections avec #...#)
             * Utilisez #Authorize# pour demander un JWT valide (ne check pas ses données mais juste qu'il existe)
             * Program.cs s'occupe de vérifier les informations basiques (expiration, jwt_secret...)
             * Ici on récupère les données avec #claims2#, voir #claims1# pour l'endroit où on les écrit
             * Vous pouvez en ajouter selon les besoins, avec Claim(nom, valeur) (ps: tout toujours en string)
             * Ensuite, #if# vérifiez juste que identity est non null et faites un if
             * Si ça correspond pas, on retourne 403 Forbidden #forbid#
            */
            var identity = HttpContext.User.Identity as ClaimsIdentity; // #claims2#
            if (identity == null || identity.FindFirst("id").Value != id.ToString()) // #if#
            {
                return Forbid(); // #forbid#
            }

            var client =await dataRepository.GetClientByIdAsync(id);

            if (client.Value == null)
            {
                return NotFound();
            }

            return client;
        }

        // PUT: api/Client/{id}
        [HttpPut("{id}")]
        [Authorize()]
        public async Task<ActionResult<Client?>> PutClient(int id, ClientDTO client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null || identity.FindFirst("id").Value != id.ToString())
            {
                return Forbid();
            }

            if (id != client.Idclient)
            {
                return BadRequest();
            }

            var clientToUpdate = await dataRepository.GetClientByIdAsync(id);

            if (clientToUpdate.Value == null)
            {
                return NotFound();
            }

            else
            {
                await dataRepository.UpdateClientAsync(clientToUpdate.Value, client);
                return Ok(clientToUpdate.Value);
            }
        }

        // POST: api/Client
        [HttpPost]
        public async Task<ActionResult<Client?>> PostClient([FromBody] ClientDTO clientDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            Client newclient = new Client()
            {
                Nomclient = clientDTO.Nomclient,
                Prenomclient = clientDTO.Prenomclient,
                Civiliteclient = clientDTO.Civiliteclient,
                Emailclient = clientDTO.Emailclient,
                Telfixeclient = clientDTO.Telfixeclient,
                Telportableclient = clientDTO.Telportableclient,
                Datecreationcompte = clientDTO.Datecreationcompte ?? DateTime.UtcNow,
                Hashmdp = clientDTO.Hashmdp,
                Pointfideliteclient = clientDTO.Pointfideliteclient,
                Newslettermiliboo = clientDTO.Newslettermiliboo,
                Newsletterpartenaires = clientDTO.Newsletterpartenaires,
        };

            await dataRepository.AddClientAsync(newclient);

            return Ok(newclient);
        }
    }
}
