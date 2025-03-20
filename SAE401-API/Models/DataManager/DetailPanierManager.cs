using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_API.Models.DataManager
{
    public class DetailPanierManager : IDetailPanierRepository<object>
    {
        public readonly _DBMilibooContext milibooContext;

        public DetailPanierManager() { }

        public DetailPanierManager(_DBMilibooContext context)
        {
            milibooContext = context;
        }

        public async Task<ActionResult<object?>> GetDetailPanierByIdAsync(int idproduit, int idcouleur, int idclient)
        {
            var detailPanier = await milibooContext.Detailpaniers
                .Include(d => d.ClientNavigation)
                .FirstOrDefaultAsync(d => d.Idproduit == idproduit
                                       && d.Idcouleur == idcouleur
                                       && d.Idclient == idclient);

            return detailPanier != null ?
                   new ActionResult<object?>(detailPanier) :
                   new NotFoundResult();
        }



        // Méthode qui accepte un Detailpanier
        public async Task AddDetailPanierAsync(Detailpanier entity)
        {
            // Logique pour ajouter un Detailpanier
            await milibooContext.Detailpaniers.AddAsync(entity);
            await milibooContext.SaveChangesAsync();
        }


        public async Task AddDetailPanierAsync(object entity)
        {
            if (entity is DetailpanierDTO dto)
            {
                // Logique pour ajouter un DetailpanierDTO
                var coloration = await milibooContext.Colorations
                    .FirstOrDefaultAsync(c => c.Idproduit == dto.Idproduit && c.Idcouleur == dto.Idcouleur);

                if (coloration == null)
                {
                    throw new Exception("La coloration spécifiée n'existe pas.");
                }

                var client = await milibooContext.Clients
                    .FirstOrDefaultAsync(c => c.Idclient == dto.Idclient);

                if (client == null)
                {
                    throw new Exception("Le client spécifié n'existe pas.");
                }

                Detailpanier detailpanier = new Detailpanier
                {
                    Idclient = dto.Idclient,
                    Idcouleur = dto.Idcouleur,
                    Idproduit = dto.Idproduit,
                    Quantitepanier = dto.Quantitepanier,
                    ClientNavigation = client,
                    ColorationNavigation = coloration
                };

                await milibooContext.Detailpaniers.AddAsync(detailpanier);
                await milibooContext.SaveChangesAsync();
            }
            else if (entity is Detailpanier dp)
            {
                // Logique pour ajouter un Detailpanier
                await milibooContext.Detailpaniers.AddAsync(dp);
                await milibooContext.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Type d'entité non supporté");
            }
        }



        public async Task UpdateDetailPanierAsync(Detailpanier detailpanier, object entity)
        {
            if (entity is DetailpanierDTO dto)
            {
                // Si l'entité est un DTO, on mappe et met à jour
                detailpanier.Idproduit = dto.Idproduit;
                detailpanier.Idcouleur = dto.Idcouleur;
                detailpanier.Idclient = dto.Idclient;
                detailpanier.Quantitepanier = dto.Quantitepanier;

                // Mettez à jour les propriétés de navigation ici si nécessaire
            }
            else if (entity is Detailpanier dp)
            {
                // Si c'est déjà un Detailpanier, on l'utilise directement
                detailpanier = dp;
            }

            milibooContext.Entry(detailpanier).State = EntityState.Modified;
            await milibooContext.SaveChangesAsync();
        }

        public async Task DeleteDetailPanierAsync(Detailpanier detailpanier)
        {
            milibooContext.Detailpaniers.Remove(detailpanier);
            await milibooContext.SaveChangesAsync();
        }

    }
}
