using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Models.DataManager;
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

        public ProduitController( IProduitRepository<Produit> datarepo)
        {
            dataRepository = datarepo;
        }

        // GET: api/Produit/GetAllProduit
        [HttpGet]
        [Route("[action]")]
        [ActionName("GetAllProduit")]
        public async Task<ActionResult<IEnumerable<Produit>>> GetAllProduit()
        {
            return await dataRepository.GetAllProduitAsync();
        }

        //GET: api/Produit/GetAllProduitByRecherche/{recherhe}
        [HttpGet]
        [Route("[action]/{recherche}")]
        [ActionName("GetAllProduitByRecherche")]
        public async Task<ActionResult<IEnumerable<Produit>>> GetAllProduitByRecherche(string recherche)
        {
            return await dataRepository.GetAllProduitByRechercheAsync(recherche,2);

        }

        //GET: api/Produit/GetAllProduitByRegroupement/{idregroupement}
        [HttpGet]
        [Route("[action]/{idregroupement}")]
        [ActionName("GetAllProduitByRegroupement")]
        public async Task<ActionResult<IEnumerable<Produit>>> GetAllProduitByRegroupement(int idregroupement)
        {
            return await dataRepository.GetAllProduitByRegroupementAsync(idregroupement);

        }

        //GET: api/Produit/GetAllProduitByCategorie/{idcategorie}
        [HttpGet]
        [Route("[action]/{idcategorie}")]
        [ActionName("GetAllProduitByCategorie")]
        public async Task<ActionResult<IEnumerable<Produit>>> GetAllProduitByCategorie(int idcategorie)
        {
            return await dataRepository.GetAllProduitByCategorieAsync(idcategorie);

        }

        //GET: api/Produit/GetAllProduitByType/{idtype}
        [HttpGet]
        [Route("[action]/{idtype}")]
        [ActionName("GetAllProduitByType")]
        public async Task<ActionResult<IEnumerable<Produit>>> GetAllProduitByType(int idtype)
        {
            return await dataRepository.GetAllProduitByTypeAsync(idtype);

        }

        // GET: api/Produit/GetProduitById/{id}
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetProduitById")]

        public async Task<ActionResult<Produit>> GetAllProduitById(int id)
        {
            var produit =await dataRepository.GetProduitByIdAsync(id);

            if (produit.Value == null)
            {
                return NotFound();
            }

            return produit;
        }

       

        // PUT: api/Produit/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduit(int id, ProduitDTO produit)
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
                await dataRepository.UpdateProduitAsync(produitToUpdate.Value, produit);
                return NoContent();
            }
        }

        // POST: api/Produit
        [HttpPost]
        public async Task<ActionResult<Produit>> PostProduit([FromBody] ProduitDTO produit)
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

            return NoContent();
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
