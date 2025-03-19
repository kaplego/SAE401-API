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

        // GET: api/Produits        
        [HttpGet]
        [Route("[action]")]
        [ActionName("GetAllProduit")]
        public async Task<ActionResult<IEnumerable<Produit>>> GetProduits()
        {
            return await dataRepository.GetAllProduitAsync();
        }

        [HttpGet]
        [Route("[action]/{recherche}")]
        [ActionName("GetProduitByRecherche")]
        public async Task<ActionResult<IEnumerable<Produit>>> GetUtilisateurByRecherche(string recherche)
        {
            return await dataRepository.GetAllProduitByRechercheAsync(recherche,2);

        }

        // GET: api/Produits/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetProduitById")]

        public async Task<ActionResult<Produit>> GetProduit(int id)
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
