using SAE401_API.Models.Repository;
using SAE401_API.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.DataManager
{
    public class DetailPanierManager<TEntity> : IDetailPanierRepository<TEntity> where TEntity : class
    {
        private readonly _DBMilibooContext _milibooContext;

        public DetailPanierManager(_DBMilibooContext context)
        {
            _milibooContext = context;
        }

        public async Task<ActionResult<TEntity?>> GetDetailPanierByIdAsync(int idproduit, int idcouleur, int idclient)
        {
            var detailPanier = await _milibooContext.Detailpaniers
                .Include(d => d.ClientNavigation)  // Inclure les informations sur le type de produit
                .Include(d => d.ColorationNavigation)  // Inclure les informations sur le type de produit
                .FirstOrDefaultAsync(d => d.Idproduit == idproduit
                                       && d.Idcouleur == idcouleur
                                       && d.Idclient == idclient);

            return detailPanier != null ? new ActionResult<TEntity>((TEntity)(object)detailPanier) : new NotFoundResult();
        }

        public async Task<Detailpanier> AddDetailPanierAsync(TEntity entity)
        {
            if (entity is Detailpanier detailpanier)
            {
                await _milibooContext.Detailpaniers.AddAsync(detailpanier);
                await _milibooContext.SaveChangesAsync();
                return detailpanier;
            }
            else
            {
                throw new InvalidOperationException("Entité de type incorrect.");
            }
        }

        public async Task<Detailpanier> UpdateDetailPanierAsync(Detailpanier detailpanier, TEntity entity)
        {
            // Si l'entité est un Detailpanier, nous procédons à la mise à jour
            if (entity is Detailpanier dp)
            {
                // Mise à jour de l'entité existante avec les valeurs du DTO
                detailpanier.Quantitepanier = dp.Quantitepanier;
                // Ajoutez ici toutes les autres propriétés nécessaires à la mise à jour
            }

            _milibooContext.Entry(detailpanier).State = EntityState.Modified;
            await _milibooContext.SaveChangesAsync();
            return detailpanier;
        }

        public async Task DeleteDetailPanierAsync(Detailpanier detailpanier)
        {
            _milibooContext.Detailpaniers.Remove(detailpanier);
            await _milibooContext.SaveChangesAsync();
        }
    }
}
