using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_API.Models.DataManager
{
    public class DetailpaniercompositionManager<TEntity> : IDetailPanierCompositionRepository<TEntity> where TEntity : class
    {
        private readonly _DBMilibooContext _milibooContext;

        public DetailpaniercompositionManager(_DBMilibooContext context)
        {
            _milibooContext = context;
        }

        public async Task<ActionResult<TEntity?>> GetDetailPanierCompositionByIdAsync(int idcomposition, int idclient)
        {
            var detailPaniercomposition = await _milibooContext.Detailpaniercompositions
                .Include(d => d.ClientNavigation)  // Inclure les informations sur le type de produit
                .FirstOrDefaultAsync(d => d.Idcomposition == idcomposition
                                       && d.Idclient == idclient);

            return detailPaniercomposition != null ? new ActionResult<TEntity>((TEntity)(object)detailPaniercomposition) : new NotFoundResult();
        }

        public async Task<Detailpaniercomposition> AddDetailPanierCompositionAsync(TEntity entity)
        {
            if (entity is Detailpaniercomposition detailpaniercomposition)
            {
                await _milibooContext.Detailpaniercompositions.AddAsync(detailpaniercomposition);
                await _milibooContext.SaveChangesAsync();
                return detailpaniercomposition;
            }
            else
            {
                throw new InvalidOperationException("Entité de type incorrect.");
            }
        }

        public async Task<Detailpaniercomposition> UpdateDetailPanierCompositionAsync(Detailpaniercomposition detailpaniercomposition, TEntity entity)
        {
            // Si l'entité est un Detailpanier, nous procédons à la mise à jour
            if (entity is Detailpaniercomposition dpc)
            {
                // Mise à jour de l'entité existante avec les valeurs du DTO
                detailpaniercomposition.Quantitepaniercomposition = dpc.Quantitepaniercomposition;
                // Ajoutez ici toutes les autres propriétés nécessaires à la mise à jour
            }

            _milibooContext.Entry(detailpaniercomposition).State = EntityState.Modified;
            await _milibooContext.SaveChangesAsync();
            return detailpaniercomposition;
        }

        public async Task DeleteDetailPanierCompositionAsync(Detailpaniercomposition detailpaniercomposition)
        {
            _milibooContext.Detailpaniercompositions.Remove(detailpaniercomposition);
            await _milibooContext.SaveChangesAsync();
        }
    }
}
