using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_API.Models.DataManager
{
    public class AttributManager : IAttributRepository<Attributproduit>
    {
        private readonly _DBMilibooContext milibooContext;


        public AttributManager(_DBMilibooContext context)
        {
            milibooContext = context;
        }

        public async Task<ActionResult<IEnumerable<Attributproduit>>> GetAllAttributByTypeAsync(int id)
        {

            // Récupérer les attributs associés au type de produit avec les informations de Typeproduit
            var attributs = await milibooContext.Attributproduits
                .Where(a => a.Idtypeproduit == id)  // Filtrer par l'ID du type de produit
                .Include(a => a.TypeproduitNavigation)  // Inclure les informations sur le type de produit
                .Include(a => a.ValeursNavigation)
                .ToListAsync();  // Exécution de la requête en asynchrone

            if (attributs == null || attributs.Count == 0)
            {
                return new ActionResult<IEnumerable<Attributproduit>>(new List<Attributproduit>());  // Si aucun attribut trouvé, retourner une liste vide
            }

            return new ActionResult<IEnumerable<Attributproduit>>(attributs);  // Retourner les attributs trouvés

        }
    }
}
