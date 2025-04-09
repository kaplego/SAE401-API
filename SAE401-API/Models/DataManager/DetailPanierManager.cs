using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

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

        public async Task<Detailpanier> UpdateDetailPanierAsync(Detailpanier detailpanier, DetailpanierDTO entity)
        {
            // Mise à jour de l'entité existante avec les valeurs du DTO
            detailpanier.Quantitepanier = entity.Quantitepanier;

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
