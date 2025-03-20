using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.DataManager;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailPanierController : ControllerBase
    {
        private readonly IDetailPanierRepository<Detailpanier> dataRepository;
        private readonly DetailPanierManager manager;

        public DetailPanierController(IDetailPanierRepository<Detailpanier> datarepo)
        {
            dataRepository = datarepo;
        }


        [HttpGet("api/Produits/{idproduit}/{idcouleur}/{idclient}")]
        public async Task<ActionResult<Detailpanier>> GetDetailPanierByIdAsync(int idproduit, int idcouleur, int idclient)
        {
            var detailpanier = await dataRepository.GetDetailPanierByIdAsync(idproduit,idcouleur,idclient);

            if (detailpanier.Value == null)
            {
                return NotFound();
            }

            return detailpanier;
        }


        // PUT: api/Produits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{idproduit}/{idcouleur}/{idclient}")]
        public async Task<IActionResult> PutDetailProduit(int idproduit,int idcouleur,int idclient, Detailpanier detailpanier)
        {
            if (idproduit != detailpanier.Idproduit && idcouleur != detailpanier.Idcouleur && idclient != detailpanier.Idclient)
            {
                return BadRequest();
            }

            var produitToUpdate = await dataRepository.GetDetailPanierByIdAsync(idproduit, idcouleur, idclient);

            if (produitToUpdate.Value == null)
            {
                return NotFound();
            }

            else
            {
                await dataRepository.UpdateDetailPanierAsync(produitToUpdate.Value, detailpanier);
                return NoContent();
            }
        }

        // POST: api/Produits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Detailpanier>> PostDetailPanier(DetailpanierDTO detailpanier )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (dataRepository is DetailPanierManager manager)
            {
                await new DetailPanierManager(manager.milibooContext).AddDetailPanierAsync(detailpanier);
            }

            return NoContent();//on ne renvoie rien si il y a bien une création
        }

        // DELETE: api/Produits/5
        [HttpDelete("{idproduit}/{idcouleur}/{idclient}")]
        public async Task<IActionResult> DeleteDetailPanier(int idproduit, int idcouleur, int idclient)
        {
            var detailpanier = await dataRepository.GetDetailPanierByIdAsync(idproduit, idcouleur, idclient);
            if (detailpanier.Value == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteDetailPanierAsync(detailpanier.Value);
            return NoContent();
        }




    }
}
