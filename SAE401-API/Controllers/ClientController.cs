using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.DataMethods;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;
using System.Security.Claims;

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


        public class Login
        {
            public string email { get; set; }
            public string password { get; set; }
            public Login() { }
        }

        /// <summary>
        /// Authentifie un client
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="login">Les identifiants sous forme {email:"", password:""}</param>
        /// <response code="200">Le client est authentifié</response>
        /// <response code="403">Email ou mot de passe invalide</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(403)]
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

        /// <summary>
        /// Obtiens un client
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">L'id du client</param>
        /// <response code="401">Un des paramètres n'est pas rempi (JWT?)</response>
        /// <response code="403">Le JWT ne correspond pas</response>
        /// <response code="404">Le client n'est pas trouvé</response>
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
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

            var client = await dataRepository.GetClientByIdAsync(id);

            if (client.Value == null)
            {
                return NotFound();
            }

            return client;
        }

        /// <summary>
        /// Modifie un client
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">L'ID du client à modifier</param>
        /// <param name="client">Le client mis à jour</param>
        /// <response code="200">Le client à été modifié</response>
        /// <response code="400">Le client n'est pas valide</response>
        /// <response code="401">Un des paramètres n'est pas rempi (JWT?)</response>
        /// <response code="403">Le JWT ne correspond pas</response>
        /// <response code="404">Le client n'est pas trouvé</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
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
                Client c = await dataRepository.UpdateClientAsync(clientToUpdate.Value, client);
                return Ok(c);
            }
        }

        /// <summary>
        /// Créé un client
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="client">Le client à ajouter</param>
        /// <response code="200">Le client à été créé</response>
        /// <response code="400">Le client n'est pas valide</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        // POST: api/Client
        [HttpPost]
        public async Task<ActionResult<Client?>> PostClient([FromBody] ClientDTO client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Client newclient = new Client()
            {
                Nomclient = client.Nomclient,
                Prenomclient = client.Prenomclient,
                Civiliteclient = client.Civiliteclient,
                Emailclient = client.Emailclient,
                Telfixeclient = client.Telfixeclient,
                Telportableclient = client.Telportableclient,
                Datecreationcompte = client.Datecreationcompte ?? DateTime.UtcNow,
                Hashmdp = client.Hashmdp,
                Pointfideliteclient = client.Pointfideliteclient,
                Newslettermiliboo = client.Newslettermiliboo,
                Newsletterpartenaires = client.Newsletterpartenaires,
            };

            await dataRepository.AddClientAsync(newclient);

            return Ok(newclient);
        }
    }
}
