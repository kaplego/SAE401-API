using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace SAE401_API.Models.DataManager
{
    public class DetailcommandeManager<TEntity> : IDetailcommandeRepository<TEntity> where TEntity : class
    {
        private readonly _DBMilibooContext _milibooContext;

        public DetailcommandeManager(_DBMilibooContext context)
        {
            _milibooContext = context;
        }

        public async Task<ActionResult<TEntity?>> GetDetailcommandeByIdsAsync(int idproduit, int idcouleur, int idcommande)
        {
            var detailcommande = await _milibooContext.Detailcommandes
                .Include(dc => dc.ColorationNavigation)  // Inclure les informations sur la coloration
                .Include(dc => dc.CommandeNavigation)  // Inclure les informations sur la commande
                .FirstOrDefaultAsync(dc => dc.Idproduit == idproduit && dc.Idcouleur == idcouleur && dc.Idcommande == idcommande);

            return detailcommande != null ? new ActionResult<TEntity>((TEntity)(object)detailcommande) : new NotFoundResult();
        }

        public async Task AddDetailcommandeAsync(TEntity entity)
        {
            if (entity is Detailcommande detailcommande)
            {
                _milibooContext.Detailcommandes.Add(detailcommande);
                await _milibooContext.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Entité de type incorrect.");
            }
        }

        public async Task UpdateDetailcommandeAsync(Detailcommande detailcommande, TEntity entity)
        {
            if (entity is Detailcommande updatedDetailcommande)
            {
                detailcommande.Quantitecommande = updatedDetailcommande.Quantitecommande;
            }

            _milibooContext.Entry(detailcommande).State = EntityState.Modified;
            await _milibooContext.SaveChangesAsync();
        }

        public async Task DeleteDetailcommandeAsync(Detailcommande detailcommande)
        {
            _milibooContext.Detailcommandes.Remove(detailcommande);
            await _milibooContext.SaveChangesAsync();
        }
    }
}
