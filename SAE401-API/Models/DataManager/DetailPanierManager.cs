using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_API.Models.DataManager
{
    public class DetailPanierManager : IDetailPanierRepository<Detailpanier>
    {
        public readonly _DBMilibooContext milibooContext;

        public DetailPanierManager() { }

        public DetailPanierManager(_DBMilibooContext context)
        {
            milibooContext = context;
        }

        public async Task<ActionResult<Detailpanier?>> GetDetailPanierByIdAsync(int idproduit, int idcouleur, int idclient)
        {
            return await milibooContext.Detailpaniers
                .Include(d => d.ClientNavigation)
                .FirstOrDefaultAsync(d => d.Idproduit == idproduit
                                       && d.Idcouleur == idcouleur
                                       && d.Idclient == idclient);
        }





        public async Task AddDetailPanierAsync(DetailpanierDTO entity)
        {
            // Récupérer la Coloration en fonction des IDs produits et couleur
            var coloration = await milibooContext.Colorations
                .FirstOrDefaultAsync(c => c.Idproduit == entity.Idproduit && c.Idcouleur == entity.Idcouleur);

            if (coloration == null)
            {
                throw new Exception("La coloration spécifiée n'existe pas.");
            }

            // Récupérer le Client en fonction de l'ID client
            var client = await milibooContext.Clients
                .FirstOrDefaultAsync(c => c.Idclient == entity.Idclient);

            if (client == null)
            {
                throw new Exception("Le client spécifié n'existe pas.");
            }

            // Assigner les entités récupérées aux propriétés de navigation
            Detailpanier detailpanier = new Detailpanier()
            {
                Idclient = entity.Idclient,
                Idcouleur = entity.Idcouleur,
                Idproduit = entity.Idproduit,
                Quantitepanier = entity.Quantitepanier,
                ClientNavigation = client,
                ColorationNavigation = coloration
            };

            // Ajouter l'objet Detailpanier à la base de données
            await milibooContext.Detailpaniers.AddAsync(detailpanier);

            // Sauvegarder les changements dans la base de données
            await milibooContext.SaveChangesAsync();
        }


        public async Task UpdateDetailPanierAsync(Detailpanier detailpanier, Detailpanier entity)
        {
            milibooContext.Entry(detailpanier).State = EntityState.Modified;
            detailpanier.Idproduit = entity.Idproduit;
            detailpanier.Idcouleur = entity.Idcouleur;
            detailpanier.Idclient = entity.Idclient;
            detailpanier.Quantitepanier = entity.Quantitepanier;
            detailpanier.ColorationNavigation = entity.ColorationNavigation;
            detailpanier.ClientNavigation = entity.ClientNavigation;
            await milibooContext.SaveChangesAsync();
        }

        public async Task DeleteDetailPanierAsync(Detailpanier detailpanier)
        {
            milibooContext.Detailpaniers.Remove(detailpanier);
            await milibooContext.SaveChangesAsync();
        }

    }
}
