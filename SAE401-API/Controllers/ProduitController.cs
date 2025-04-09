using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProduitController : ControllerBase
    {
        private readonly IProduitRepository<Produit> dataRepository;

        public ProduitController(IProduitRepository<Produit> datarepo)
        {
            dataRepository = datarepo;
        }

        /// <summary>
        /// Obtiens les produits
        /// </summary>
        /// <returns>Http response</returns>
        // GET: api/Produit/GetAllProduit
        [HttpGet]
        [Route("[action]")]
        [ActionName("GetAllProduit")]
        public async Task<ActionResult<IEnumerable<Produit>>> GetAllProduit()
        {
            return await dataRepository.GetAllProduitAsync();
        }

        /// <summary>
        /// Obtiens les produits selon une recherche
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="recherche">La recherche à effectuer</param>
        //GET: api/Produit/GetAllProduitByRecherche/{recherhe}
        [HttpGet]
        [Route("[action]/{recherche}")]
        [ActionName("GetAllProduitByRecherche")]
        public async Task<ActionResult<IEnumerable<Produit>>> GetAllProduitByRecherche(string recherche)
        {
            return await dataRepository.GetAllProduitByRechercheAsync(recherche, 2);

        }

        /// <summary>
        /// Obtiens les produits selon leur regroupement
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="idregroupement">Le regroupement</param>
        //GET: api/Produit/GetAllProduitByRegroupement/{idregroupement}
        [HttpGet]
        [Route("[action]/{idregroupement}")]
        [ActionName("GetAllProduitByRegroupement")]
        public async Task<ActionResult<IEnumerable<Produit>>> GetAllProduitByRegroupement(int idregroupement)
        {
            return await dataRepository.GetAllProduitByRegroupementAsync(idregroupement);

        }

        /// <summary>
        /// Obtiens les produits selon leur catégorie
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="idcategorie">La catégorie</param>
        //GET: api/Produit/GetAllProduitByCategorie/{idcategorie}
        [HttpGet]
        [Route("[action]/{idcategorie}")]
        [ActionName("GetAllProduitByCategorie")]
        public async Task<ActionResult<IEnumerable<Produit>>> GetAllProduitByCategorie(int idcategorie)
        {
            return await dataRepository.GetAllProduitByCategorieAsync(idcategorie);

        }

        /// <summary>
        /// Obtiens les produits selon leur type
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="idtype">Le type</param>
        //GET: api/Produit/GetAllProduitByType/{idtype}
        [HttpGet]
        [Route("[action]/{idtype}")]
        [ActionName("GetAllProduitByType")]
        public async Task<ActionResult<IEnumerable<Produit>>> GetAllProduitByType(int idtype)
        {
            return await dataRepository.GetAllProduitByTypeAsync(idtype);

        }

        /// <summary>
        /// Obtiens un produit
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="404">Le produit n'est pas trouvé</response>
        [ProducesResponseType(404)]
        // GET: api/Produit/GetProduitById/{id}
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetProduitById")]

        public async Task<ActionResult<Produit>> GetProduitById(int id)
        {
            var produit = await dataRepository.GetProduitByIdAsync(id);

            if (produit.Value == null)
            {
                return NotFound();
            }

            return produit;
        }


        /// <summary>
        /// Modifie un produit
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">L'IDproduit à modifier</param>
        /// <param name="produit">Le produit mis à jour</param>
        /// <response code="200">Le produit à été modifié</response>
        /// <response code="400">Le produit n'est pas valide</response>
        /// <response code="404">Le produit n'est pas trouvé</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        // PUT: api/Produit/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Produit?>> PutProduit(int id, ProduitDTO produit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != produit.Idproduit)
            {
                return BadRequest();
            }

            var produitToUpdate = await dataRepository.GetProduitByIdAsync(id);

            if (produitToUpdate.Value == null)
            {
                return NotFound();
            }

            else
            {
                Produit p = await dataRepository.UpdateProduitAsync(produitToUpdate.Value, produit);
                return Ok(p);
            }
        }

        /// <summary>
        /// Créé un produit
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="produit">Le produit à ajouter</param>
        /// <response code="200">Le produit à été créé</response>
        /// <response code="400">Le produit n'est pas valide</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        // POST: api/Produit
        [HttpPost]
        public async Task<ActionResult<Produit?>> PostProduit([FromBody] ProduitDTO produit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Produit newProduit = new Produit()
            {
                Idtypeproduit = produit.Idtypeproduit,
                Idpays = produit.Idpays,
                Nomproduit = produit.Nomproduit,
                Notice = produit.Sourcenotice,
                Aspecttechnique = produit.Sourceaspecttechnique,
                Delailivraison = produit.Delailivraison,
                Coutlivraison = produit.Coutlivraison,
                Nbpaiementmax = produit.Nbpaiementmax
            };

            await dataRepository.AddProduitAsync(newProduit);

            return Ok(newProduit);
        }

        // DELETE: api/Produit/{id} 
        /*[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduit(int id)
        {
            var produit =  await dataRepository.GetProduitByIdAsync(id);
            if (produit.Value == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteProduitAsync(produit.Value);
            return NoContent();
        }*/


    }
}
