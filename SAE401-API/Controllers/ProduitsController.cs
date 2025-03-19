using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Models.DataManager;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProduitsController : ControllerBase
    {
        private readonly  IProduitRepository<Produit> dataRepository;

        public ProduitsController( IProduitRepository<Produit> datarepo)
        {
            dataRepository = datarepo;
        }

        // GET: api/Produits/GetAllProduit
        [HttpGet]
        [Route("[action]")]
        [ActionName("GetAllProduit")]
        public async Task<ActionResult<IEnumerable<Produit>>> GetAllProduit()
        {
            return await dataRepository.GetAllProduitAsync();
        }

        //GET: api/Produits/GetAllProduitByRecherche/{id}
        [HttpGet]
        [Route("[action]/{recherche}")]
        [ActionName("GetAllProduitByRecherche")]
        public async Task<ActionResult<IEnumerable<Produit>>> GetAllProduitByRecherche(string recherche)
        {
            return await dataRepository.GetAllProduitByRechercheAsync(recherche,2);

        }

        //GET: api/Produits/GetAllProduitByRegroupement/{id}
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetAllProduitByRegroupement")]
        public async Task<ActionResult<IEnumerable<Produit>>> GetAllProduitByRegroupement(int id)
        {
            return await dataRepository.GetAllProduitByRegroupementAsync(id);

        }

        //GET: api/Produits/GetAllProduitByCategorie/{id}
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetAllProduitByCategorie")]
        public async Task<ActionResult<IEnumerable<Produit>>> GetAllProduitByCategorie(int id)
        {
            return await dataRepository.GetAllProduitByCategorieAsync(id);

        }

        //GET: api/Produits/GetAllProduitByType/{id}
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetAllProduitByType")]
        public async Task<ActionResult<IEnumerable<Produit>>> GetAllProduitByType(int id)
        {
            return await dataRepository.GetAllProduitByTypeAsync(id);

        }

        // GET: api/Produits/GetProduitById/{id}
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

       

        // PUT: api/Produits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduit(int id, Produit produit)
        {
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

        // POST: api/Produits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Produit>> PostProduit(Produit produit)
        {
            if(!ModelState.IsValid)
            {

            return BadRequest(ModelState); 
            }

            await dataRepository.AddProduitAsync(produit);

            return CreatedAtAction("GetProduit", new { id = produit.Idproduit }, produit);
        }

        // DELETE: api/Produits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduit(int id)
        {
            var produit =  await dataRepository.GetProduitByIdAsync(id);
            if (produit.Value == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteProduitAsync(produit.Value);
            return NoContent();
        }


    }
}
